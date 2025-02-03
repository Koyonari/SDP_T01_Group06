using SDP_T01_Group06.Composite;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06
{
    public abstract class Document
	{
		protected DocumentState currentState;
		protected User owner;
		protected List<User> collaborators = new List<User>();
		protected User approver;
		protected User submitter;
		protected bool previouslyrejected = false;
		protected bool isedited = false;
		protected string documentcontent;
		protected string documentname;
		protected DocumentSection rootsection;
		protected DocumentSection currentSection;

		public User Owner { get => owner; set => owner = value; }
        public List<User> Collaborators { get => collaborators; set => collaborators = value; }
        public User Approver { get => approver; set => approver = value; }
		public User Submitter { get => submitter; set => submitter = value; }
		public string documentContent { get => documentcontent; set => documentcontent = value; }
        public string Documentname { get => documentname; set => documentname = value; }
        public bool previouslyRejected { get => previouslyrejected; set => previouslyrejected = value; }
        public bool isEdited { get => isedited; set => isedited = value; }
		
        public Document(User owner)
		{
			this.owner = owner;
			this.currentState = new DraftState(this);
			this.isedited = false;
			this.previouslyrejected = false;
            this.collaborators = new List<User>();
			this.rootsection = new DocumentSection("Document Root");
        }

		public bool hasApprover()
		{
			return approver != null;
        }

        public void setState(DocumentState state)
		{
			this.setState(state);
        }

		public void edit()
		{
            currentState.edit();
        }

		public void addCollaborator(User collaborator)
		{
			currentState.addCollaborator(collaborator);
		}

		public void nominateApprover(User approver)
		{
			currentState.nominateApprover(approver);
        }

		public void submitForApproval(User submitter)
		{
			currentState.submitForApproval(submitter);
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
        }

        public DocumentState getCurrentState()
        {
            return this.currentState;
        }

		public void displayContent()
		{
			Console.WriteLine("Document Content: \n" + documentcontent);
        }

        public void assembleDocument()
		{
			getDocumentName(); // concrete operation
			addHeader(); // concrete operation
			createBody(); // primitive operation
			addFooter(); // concrete operation
		}

		public void getDocumentName()
		{
			Console.Write("Please enter the name of the document: ");
			string docname = Console.ReadLine();
			while (docname == "")
			{
				Console.Write("Please enter the name of the document: ");
				docname = Console.ReadLine();
			}
			documentname = docname;
		}
		public void addHeader()
		{
			DocumentSection header = new DocumentSection("Header", false); // header is not editable
			header.add(new DocumentItem($"{owner.Name} - {documentname}", "Title", false));
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
			// current section
			Console.WriteLine($"\nYou are in section: {section.SectionName}");

			// if there are no more children, just select the current one
			if (section.children.Count == 0)
			{
				currentSection = section;
				Console.WriteLine("No sub-sections or items available. Selected current section: " + section.SectionName);
				return;
			}

			// or else display the current section children
			Console.WriteLine("Available components:");
			for (int i = 0; i < section.children.Count; i++)
			{
				DocumentComponent comp = section.children[i];
				if (comp is DocumentSection ds)
				{
					Console.WriteLine($"{i + 1}. Section: {ds.SectionName}");
				}
				else if (comp is DocumentItem di)
				{
					Console.WriteLine($"{i + 1}. Item: {di.Content}");
				}
			}

			Console.Write("Enter the number of the component to select (or 0 to select the current section): ");
			string input = Console.ReadLine();
			if (int.TryParse(input, out int choice))
			{
				// If 0 is entered, select the current section and stop drilling down.
				if (choice == 0)
				{
					currentSection = section;
					Console.WriteLine("Selected section: " + section.SectionName);
					return;
				}

				// Validate selection is within range.
				if (choice < 1 || choice > section.children.Count)
				{
					Console.WriteLine("Invalid selection. Please try again.");
					return;
				}

				// Get the selected component (adjusting for zero-based indexing)
				DocumentComponent selected = section.children[choice - 1];

				// If the selected component is a section, call selectSection recursively.
				if (selected is DocumentSection ds)
				{
					selectSection(ds, level + 1);
				}
				// If the selected component is a DocumentItem, print a message (or call an edit routine).
				else if (selected is DocumentItem di)
				{
					Console.WriteLine("Selected a document item: " + di.Content);
					// Optionally call an edit routine:
					// EditDocumentItem(di);
				}
			}
			else
			{
				Console.WriteLine("Invalid input. Please enter a valid number.");
			}
		}



		protected void DisplaySections(DocumentSection section, int level)
		{
			Console.WriteLine($"{new string(' ', level * 2)}{level}. {section.SectionName}");
			//foreach (DocumentComponent child in section.children)
			//{
			//	if (child is DocumentSection childSection)
			//	{
			//		DisplaySections(childSection, level + 1);
			//	}
			//}
		}

		public abstract void editDocument();

		public abstract void createBody();

		public virtual void addCodeSnippet() { } // hook

		public virtual void addBudgetBreakdown() { } // hook
    }
}
