namespace SDP_T01_Group06
{
	public abstract class Document
	{
		private DocumentState currentState;
		private User owner;
		private List<User> collaborators = new List<User>();
        private User approver;
		private bool previouslyrejected = false;
		private bool isedited = false;
		private string documentcontent;

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
			addHeader();
			addFooter();
			createBody();
			addCodeSnippet();
			addBudgetBreakdown();
		}

		public void addHeader()
		{
			Console.WriteLine("Header added to document");
		}
		public void addFooter()
		{
			Console.WriteLine("Footer added to document");
		}

		public abstract void createBody();

		public virtual void addCodeSnippet() { }

		public virtual void addBudgetBreakdown() { }
	}
}
