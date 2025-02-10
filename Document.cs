using SDP_T01_Group06.Composite;
using SDP_T01_Group06.Memento;
using SDP_T01_Group06.Observer;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06
{
	public abstract class Document
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
		protected DocumentObservable documentSubject;

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

            // Initialize document subject with name and state
            getDocumentName();
            this.documentSubject = new DocumentObservable(this.documentName, this.currentState);

            // Register owner as an observer
            Listener ownerObserver = new Listener(owner);
            ownerObserver.AddDocument(this.documentSubject);
        }

		public bool hasApprover()
		{
			return approver != null;
		}

        public void setState(DocumentState state)
        {
            this.currentState = state;
            this.documentSubject.setState(state);
        }

        public void edit()
		{
			currentState.edit();
		}

		public void addCollaborator(User collaborator)
        {
            // Check if the collaborator exists by comparing names since that's the unique identifier
            bool isExistingCollaborator = collaborators.Any(c => c.Name.Equals(collaborator.Name, StringComparison.OrdinalIgnoreCase));

            if (!isExistingCollaborator && this.owner.Name != collaborator.Name)
            {
                collaborators.Add(collaborator);
                if (!collaborator.DocumentList.Contains(this))
                {
                    collaborator.DocumentList.Add(this);
                }

                // Register the collaborator as an observer
                Listener collaboratorObserver = new Listener(collaborator);
                collaboratorObserver.AddDocument(this.documentSubject);

                // Notify current state
                currentState.addCollaborator(collaborator);
                Console.WriteLine($"{collaborator.Name} has been added as a collaborator.");
            }
            else if (isExistingCollaborator)
            {
                Console.WriteLine($"{collaborator.Name} is already a collaborator.");
            }
            else
            {
                Console.WriteLine($"{collaborator.Name} cannot be added as a collaborator (document owner).");
            }
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
			this.currentState = currentState;
            this.documentSubject.setState(currentState);
        }

		public DocumentState getCurrentState()
		{
			return this.currentState;
		}

		public void assembleDocument()
		{
			addHeader(); // concrete operation
			createBody(); // primitive operation
			addFooter(); // concrete operation
		}

		public void getDocumentName()
		{
			Console.Write("Please enter the name of the document: ");
			string docname = Console.ReadLine();

            Console.WriteLine(docname);
            while (docname == "")
			{

				Console.Write("Invlid Input. Please enter the name of the document: ");
				docname = Console.ReadLine();
			}
            documentName = docname;
		}
		public void addHeader()
		{
			DocumentSection header = new DocumentSection("Header", false); // header is not editable
			header.add(new DocumentItem($"{owner.Name} - {documentName}", "Title", false));
			rootsection.add(header);
			Console.WriteLine("\nHeader added to document");
		}

		public void addFooter()
		{
			DocumentSection footer = new DocumentSection("Footer", false); // footer is not editable
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
					// Show whether the section is editable.
					string editableMark = ds.IsEditable ? "" : " (Not Editable)";
					Console.WriteLine($"{i + 1}. Section: {ds.SectionName}{editableMark}");
				}
				else if (comp is DocumentItem di)
				{
					// Even if an item is editable, it cannot be selected as a section.
					string editableMark = di.IsEditable ? "" : " (Not Editable)";
					Console.WriteLine($"{i + 1}. [Leaf] Item: {di.Content}{editableMark} (Cannot be selected as a section)");
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

				// Validate choice range
				if (choice < 1 || choice > section.children.Count)
				{
					Console.WriteLine($"Invalid selection. Please enter a number between 0 and {section.children.Count}.");
					continue;
				}

				DocumentComponent selected = section.children[choice - 1];

				// Prevent selecting a leaf node (DocumentItem) as a section.
				if (selected is DocumentItem item)
				{
					Console.WriteLine($"Error: '{item.Content}' is an item and cannot be selected as a section.");
					continue;
				}

				// Handle section selection (only DocumentSections can be navigated into)
				if (selected is DocumentSection childSection)
				{
					if (!childSection.IsEditable)
					{
						Console.WriteLine($"Error: Section '{childSection.SectionName}' is not editable.");
						continue;
					}
					// Recursively navigate into the editable section.
					selectSection(childSection, level + 1);
					return;
				}

				Console.WriteLine("Selected component type is not supported."); // Fallback error
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

		public Document clone()
		{
			Document clonedDoc = (Document)this.MemberwiseClone();

			// Deep copy lists and complex objects
            clonedDoc.collaborators = new List<User>(this.collaborators);
			clonedDoc.rootsection = cloneSection(this.rootsection);
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

		public DocumentMemento createMemento()
		{
            return new DocumentMemento(this.clone());
        }

		public void restoreFromMemento(DocumentMemento memento)
        {
            Document restoredDoc = memento.getSavedState();
            this.documentID = restoredDoc.documentID;
            this.documentName = restoredDoc.documentName;
            this.owner = restoredDoc.owner;
            this.collaborators = restoredDoc.collaborators;
            this.approver = restoredDoc.approver;
            this.submitter = restoredDoc.submitter;
            this.previouslyrejected = restoredDoc.previouslyrejected;
            this.isedited = restoredDoc.isedited;
            this.rootsection = restoredDoc.rootsection;
            this.currentSection = restoredDoc.currentSection;
            this.currentState = restoredDoc.currentState;
            this.documentSubject = restoredDoc.documentSubject;
        }
}
