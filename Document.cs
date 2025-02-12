using SDP_T01_Group06.Composite;
using SDP_T01_Group06.Converter;
using SDP_T01_Group06.Observer;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06
{
    public abstract class Document : ISubject
    {
        protected DocumentState currentState;
        protected Guid documentID;
        protected string documentName;
        protected User owner;
        protected List<User> collaborators = new List<User>();
        protected User approver;
        protected User submitter;
        protected bool previouslyrejected = false;
        protected bool isedited = false;
        protected DocumentSection rootsection;
        protected DocumentSection currentSection;
        private List<IObserver> observers;
        private IDocumentConverter conversionStrategy; 

        public Guid DocumentID
        {
            get { return documentID; }
            set { documentID = value; }
        }
        public string DocumentName
        {
            get { return documentName; }
            set { documentName = value; }
        }
        public User Owner { get => owner; set => owner = value; }
        public List<User> Collaborators { get => collaborators; set => collaborators = value; }
        public User Approver { get => approver; set => approver = value; }
        public User Submitter { get => submitter; set => submitter = value; }
        public bool previouslyRejected { get => previouslyrejected; set => previouslyrejected = value; }
        public bool isEdited { get => isedited; set => isedited = value; }

        public Document(User owner)
        {
            this.documentID = Guid.NewGuid();
            this.owner = owner;
            this.currentState = new DraftState(this);
            this.isedited = false;
            this.previouslyrejected = false;
            this.collaborators = new List<User>();
            this.rootsection = new DocumentSection("Document Root");
            this.observers = new List<IObserver>();

            // Get document name and register owner as observer
            getDocumentName();
            registerObserver(owner);
        }

        public bool hasApprover()
        {
            return approver != null;
        }

        // ------------Observer Design Pattern------------
        public void registerObserver(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void removeObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void notifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.update(documentName, currentState);
            }
        }


        public void setState(DocumentState state)
        {
            var oldState = currentState;
            this.currentState = state;

            // Only notify if the state has actually changed
            if (oldState.GetType() != state.GetType())
            {
                notifyObservers();
            }
        }

        // -----------------------------------------

        public void addCollaborator(User collaborator)
        {
            // Let the state handle the actual collaborator management
            currentState.addCollaborator(collaborator);

            // Only set up the observer if the collaborator was successfully added
            if (collaborators.Any(c => c.Name.Equals(collaborator.Name, StringComparison.OrdinalIgnoreCase)))
            {
                registerObserver(collaborator);
            }
        }

        public void edit()
        {
            currentState.edit();
        }

        public void nominateApprover(User approver)
        {
            currentState.nominateApprover(approver);
        }

        public void submitForApproval(User submitter)
        {
            currentState.submitForApproval(submitter);
            this.submitter = submitter;
        }

        public void pushBack(string comment)
        {
            currentState.pushBack(comment);
        }

        public void approve()
        {
            currentState.approve();
        }

        public void reject()
        {
            currentState.reject();
        }

        public void resumeEditing()
        {
            currentState.resumeEditing();
        }

        public void undoSubmission(User undoer)
        {
            currentState.undoSubmission(undoer);
        }

        public void setCurrentState(DocumentState currentState)
        {
            setState(currentState);
        }

        public DocumentState getCurrentState()
        {
            return this.currentState;
        }

        public void assembleDocument()
        {
            addHeader();
            createBody();
            addFooter();
        }

        public void getDocumentName()
        {
            Console.Write("Please enter the name of the document: ");
            string docname = Console.ReadLine();

            Console.WriteLine(docname);
            while (docname == "")
            {
                Console.Write("Invalid Input. Please enter the name of the document: ");
                docname = Console.ReadLine();
            }
            documentName = docname;
        }

        public void addHeader()
        {
            DocumentSection header = new DocumentSection("Header", false);
            header.add(new DocumentItem($"{owner.Name} - {documentName}", "Title", false));
            rootsection.add(header);
            Console.WriteLine("\nHeader added to document");
        }

        public void addFooter()
        {
            DocumentSection footer = new DocumentSection("Footer", false);
            footer.add(new DocumentItem($"Copyright {new DateTime().Year} - {owner.Name}", "Copyright", false));
            rootsection.add(footer);
            Console.WriteLine("Footer added to document\n");
        }

        public void addParagraph()
        {
            if (currentSection == null)
            {
				// default to body section
                currentSection = (DocumentSection)rootsection.getChild(1);
            }

            Console.Write("Enter text to put in paragraph: ");
            string text = Console.ReadLine();
            if (!string.IsNullOrEmpty(text))
            {
                currentSection.add(new DocumentItem(text, "Paragraph"));
                Console.WriteLine("Paragraph added to document");
                isedited = true;
            }
        }

        public void selectSection(DocumentSection section, int level)
        {
            Console.WriteLine($"\nYou are in section: {section.SectionName}");

            if (section.children.Count == 0)
            {
                if (!section.IsEditable)
                {
                    Console.WriteLine("This section is not editable. Cannot select it.");
                    return;
                }
                currentSection = section;
                Console.WriteLine("No sub-sections available. Selected current section: " + section.SectionName);
                return;
            }

            Console.WriteLine("Available components:");
            for (int i = 0; i < section.children.Count; i++)
            {
                DocumentComponent comp = section.children[i];
                if (comp is DocumentSection ds)
                {
					// show whether the section is editable to tell users if they can select it for editing or not
                    string editableMark = ds.IsEditable ? "" : " (Not Editable)";
                    Console.WriteLine($"{i + 1}. Section: {ds.SectionName}{editableMark}");
                }
                else if (comp is DocumentItem di)
                {
					// even if an item is editable, it cannot be selected as a section cos its a leaf
                    string editableMark = di.IsEditable ? "" : " (Not Editable)";
                    Console.WriteLine($"{i + 1}. [Leaf] {di.ElementType}: {di.Content}{editableMark} (Cannot be selected as a section)");
                }
            }

            while (true)
            {
                Console.Write($"Enter the number of the component to select (0 to confirm current section, 1-{section.children.Count} to navigate): ");
                string input = Console.ReadLine();

				// validation
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value.");
                    continue;
                }

				// current section
                if (choice == 0)
                {
                    if (!section.IsEditable)
                    {
                        Console.WriteLine($"Error: Section '{section.SectionName}' is not editable, cannot confirm selection.");
                        continue;
                    }
                    currentSection = section;
                    Console.WriteLine($"Confirmed current section: {section.SectionName}");
                    return;
                }

                if (choice < 1 || choice > section.children.Count)
                {
                    Console.WriteLine($"Invalid selection. Please enter a number between 0 and {section.children.Count}.");
                    continue;
                }

                DocumentComponent selected = section.children[choice - 1];

                if (selected is DocumentItem item)
                {
                    Console.WriteLine($"Error: '{item.Content}' is an item and cannot be selected as a section.");
                    continue;
                }

                if (selected is DocumentSection childSection)
                {
                    if (!childSection.IsEditable)
                    {
                        Console.WriteLine($"Error: Section '{childSection.SectionName}' is not editable.");
                        continue;
                    }
                    selectSection(childSection, level + 1);
                    return;
                }

                Console.WriteLine("Selected component type is not supported.");
                return;
            }
        }

        public void displayDocumentContent()
        {
            Console.WriteLine("Document Content:");
            rootsection.display();
        }

        protected void DisplaySections(DocumentSection section, int level)
        {
            Console.WriteLine($"{new string(' ', level * 2)}{level}. {section.SectionName}");
            foreach (DocumentComponent child in section.children)
            {
                if (child is DocumentSection childSection)
                {
                    DisplaySections(childSection, level + 1);
                }
            }
        }

        public abstract void editDocument();

        public abstract void createBody();

        // ----------------Strategy Design Pattern----------------
        public void setConversionStrategy(IDocumentConverter strategy)
        {
            conversionStrategy = strategy;
        }

        public Document convert()
        {
            if (conversionStrategy == null)
            {
                throw new InvalidOperationException("No conversion strategy set");
            }
            return conversionStrategy.convert(this);
        }

        public Document clone()
        {
            Document clonedDoc = (Document)this.MemberwiseClone();
            clonedDoc.collaborators = new List<User>(this.collaborators);
            clonedDoc.rootsection = cloneSection(this.rootsection);
            clonedDoc.observers = new List<IObserver>(this.observers);
            return clonedDoc;
        }

        private DocumentSection cloneSection(DocumentSection section)
        {
            DocumentSection newSection = new DocumentSection(section.SectionName, section.IsEditable);
            foreach (var child in section.children)
            {
                if (child is DocumentSection childSection)
                {
                    newSection.add(cloneSection(childSection));
                }
                else if (child is DocumentItem childItem)
                {
                    newSection.add(new DocumentItem(childItem.Content, "Item", childItem.IsEditable));
                }
            }
            return newSection;
        }
        // -----------------------------------------
    }
}