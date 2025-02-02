namespace SDP_T01_Group06
{
	public abstract class Document
	{
		protected DocumentState currentState;
		protected User owner;
		protected List<User> collaborators = new List<User>();
		protected User approver;
		protected bool previouslyrejected = false;
		protected bool isedited = false;
		protected string documentcontent;
		protected string documentname;
		protected DocumentSection rootsection;
		protected DocumentSection currentSection;

		public User Owner { get => owner; set => owner = value; }
        public List<User> Collaborators { get => collaborators; set => collaborators = value; }
        public User Approver { get => approver; set => approver = value; }
		public string documentContent { get => documentcontent; set => documentcontent = value; }

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

		public void submitForApproval()
		{
			currentState.submitForApproval();
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
			while (docname == null)
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
			Console.WriteLine("Header added to document");
		}

		public void addFooter()
		{
			DocumentSection footer = new DocumentSection("Footer", false); // footer is not editable
			footer.add(new DocumentItem($"Copyright {new DateTime().Year} - {owner.Name}", "Copyright", false));
			rootsection.add(footer);
			Console.WriteLine("Footer added to document");
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

		public void selectSection()
		{
			Console.WriteLine("Available sections:");
			DisplaySections(rootsection, 0);
			Console.Write("Enter section number to edit: ");
			if (int.TryParse(Console.ReadLine(), out int sectionIndex)) // if section number is correct, put it in sectionIndex variable
			{
				try
				{
					currentSection = (DocumentSection)rootsection.getChild(sectionIndex);
					Console.WriteLine($"Selected section: {currentSection.SectionName}");
				}
				catch (Exception)
				{
					Console.WriteLine("Invalid section number.");
				}
			}
		}

		protected void DisplaySections(DocumentSection section, int level)
		{
			Console.WriteLine($"{new string(' ', level * 2)}{level}. {section.SectionName}");
			foreach (var child in section.children)
			{
				if (child is DocumentSection childSection)
				{
					DisplaySections(childSection, level + 1);
				}
			}
		}

		public abstract void editDocument();

		public abstract void createBody();

		public virtual void addCodeSnippet() { } // hook

		public virtual void addBudgetBreakdown() { } // hook
	}
}
