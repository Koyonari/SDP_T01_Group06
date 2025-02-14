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
        protected History history;
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
            
            history = new History();
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

        public bool selectSection(DocumentSection section, int level)
        {
            // At top level, always start at the root.
            if (level == 1)
            {
                section = this.rootsection;
            }

            Console.WriteLine($"\nYou are in section: {section.SectionName}");

            // If there are no children, let the user confirm this section,
            // but also offer the save option.
            if (section.children.Count == 0)
            {
                if (!section.IsEditable)
                {
                    Console.WriteLine("This section is not editable. Cannot select it.");
                    return false;
                }
                currentSection = section;
                Console.WriteLine("No sub-sections available. Selected current section: " + section.SectionName);

                // Even here, offer the save option.
                Console.WriteLine("Type 's' to save changes and exit to the main menu, or press Enter to continue editing in this section.");
                string inputNoChildren = Console.ReadLine().Trim();
                if (inputNoChildren.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    history.Clear();
                    return true; // signal save/exit
                }
                return false;
            }

            // Display available components.
            Console.WriteLine("Available components:");
            for (int i = 0; i < section.children.Count; i++)
            {
                DocumentComponent comp = section.children[i];
                if (comp is DocumentSection ds)
                {
                    string editableMark = ds.IsEditable ? "" : " (Not Editable)";
                    Console.WriteLine($"{i + 1}. Section: {ds.SectionName}{editableMark}");
                }
                else if (comp is DocumentItem di)
                {
                    string editableMark = di.IsEditable ? "" : " (Not Editable)";
                    Console.WriteLine($"{i + 1}. [Leaf] Item: {di.Content}{editableMark} (Cannot be selected as a section)");
                }
            }

            // Show the undo and save options.
            Console.WriteLine("Type 'u' to undo the last change, or 's' to save changes and exit to the main menu.");

            while (true)
            {
                Console.Write($"Enter the number of the component to select (0 to confirm current section, 1-{section.children.Count} to navigate): ");
                string input = Console.ReadLine().Trim();

                // Check for the undo command.
                if (input.Equals("u", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Undo command received. Reverting to the previous state...");
                    DocumentMemento memento = history.Undo(); // Retrieves the last snapshot.
                    if (memento != null)
                    {
                        restoreMemento(memento);
                        Console.WriteLine("Undo successful.");
                    }
                    else
                    {
                        Console.WriteLine("No mementos to undo.");
                    }
                    // After undoing, re-read the updated state.
                    DocumentSection updatedSection = (level == 1) ? this.rootsection : this.currentSection;
                    // Recurse to re-display the selection prompt.
                    return selectSection(rootsection, level);
                }

                // Check for the save command.
                if (input.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Save command received. Saving current state and clearing undo history. Exiting to main menu...");
                    history.Clear();
                    return true; // Signal to exit.
                }

                // Try parsing the input as a number.
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value, 'u' to undo, or 's' to save.");
                    continue;
                }

                // Confirm the current section.
                if (choice == 0)
                {
                    if (!section.IsEditable)
                    {
                        Console.WriteLine($"Error: Section '{section.SectionName}' is not editable, cannot confirm selection.");
                        continue;
                    }
                    currentSection = section;
                    Console.WriteLine($"Confirmed current section: {section.SectionName}");
                    return false;
                }

                // Validate the choice range.
                if (choice < 1 || choice > section.children.Count)
                {
                    Console.WriteLine($"Invalid selection. Please enter a number between 0 and {section.children.Count}.");
                    continue;
                }

                DocumentComponent selected = section.children[choice - 1];

                // Prevent selecting a leaf node as a section.
                if (selected is DocumentItem)
                {
                    Console.WriteLine($"Error: '{((DocumentItem)selected).Content}' is an item and cannot be selected as a section.");
                    continue;
                }

                // Handle section selection.
                if (selected is DocumentSection childSection)
                {
                    if (!childSection.IsEditable)
                    {
                        Console.WriteLine($"Error: Section '{childSection.SectionName}' is not editable.");
                        continue;
                    }
                    // Recursively navigate into the editable sub-section.
                    bool result = selectSection(childSection, level + 1);
                    // If the nested call returns true (user saved), propagate that upward.
                    if (result)
                    {
                        return true;
                    }
                    else
                    {
                        // Otherwise, continue from this level.
                        return false;
                    }
                }

                Console.WriteLine("Selected component type is not supported.");
                return false;
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
        
         // Memento
         public DocumentMemento createMemento()
         {
             return new DocumentMemento(
                 this.documentName,
                 this.rootsection,
                 this.currentSection,
                 this.currentState,
                 this.isedited
             );
         }

         // In Document class, modify restoreMemento:
         public void restoreMemento(DocumentMemento memento)
         {  
             //Console.WriteLine("\n=== Restoring Memento ===");
             //Console.WriteLine($"Before restore - Root children count: {rootsection?.children.Count ?? 0}");
             //Console.WriteLine($"Before restore - Current section: {currentSection?.SectionName ?? "null"}");

             this.documentName = memento.DocumentName;
             this.rootsection = memento.RootSectionClone;
             this.currentSection = memento.CurrentSectionClone;
             this.currentState = memento.CurrentState;
             this.isedited = memento.IsEdited;

             //Console.WriteLine($"After restore - Root children count: {rootsection.children.Count}");
             //Console.WriteLine($"After restore - Current section: {currentSection?.SectionName ?? "null"}");
             //Console.WriteLine("========================\n");
         }
    }
}