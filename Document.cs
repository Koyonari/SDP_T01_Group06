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
        // Option 1: Define a History field to persist throughout the editing session.
        protected History history;
        //protected History history = new History();
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

            history = new History();
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
            //Console.WriteLine("\n=== Adding Paragraph ===");
            //Console.WriteLine($"Current section before adding: {currentSection?.SectionName ?? "null"}");
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
            //Console.WriteLine($"Section children count after adding: {currentSection.children.Count}");
            //Console.WriteLine("========================\n");
        }

        //public void selectSection(DocumentSection section, int level)
        //{
        //	Console.WriteLine($"\nYou are in section: {section.SectionName}");

        //	if (section.children.Count == 0)
        //	{
        //		if (!section.IsEditable)
        //		{
        //			Console.WriteLine("This section is not editable. Cannot select it.");
        //			return;
        //		}
        //		currentSection = section;
        //		Console.WriteLine("No sub-sections available. Selected current section: " + section.SectionName);
        //		return;
        //	}

        //	Console.WriteLine("Available components:");
        //	for (int i = 0; i < section.children.Count; i++)
        //	{
        //		DocumentComponent comp = section.children[i];
        //		if (comp is DocumentSection ds)
        //		{
        //			// Show whether the section is editable.
        //			string editableMark = ds.IsEditable ? "" : " (Not Editable)";
        //			Console.WriteLine($"{i + 1}. Section: {ds.SectionName}{editableMark}");
        //		}
        //		else if (comp is DocumentItem di)
        //		{
        //			// Even if an item is editable, it cannot be selected as a section.
        //			string editableMark = di.IsEditable ? "" : " (Not Editable)";
        //			Console.WriteLine($"{i + 1}. [Leaf] Item: {di.Content}{editableMark} (Cannot be selected as a section)");
        //		}
        //	}

        //	while (true)
        //	{
        //		Console.Write($"Enter the number of the component to select (0 to confirm current section, 1-{section.children.Count} to navigate): ");
        //		string input = Console.ReadLine();

        //		// validation
        //		if (!int.TryParse(input, out int choice))
        //		{
        //			Console.WriteLine("Invalid input. Please enter a numeric value.");
        //			continue;
        //		}

        //		// current section
        //		if (choice == 0)
        //		{
        //			if (!section.IsEditable)
        //			{
        //				Console.WriteLine($"Error: Section '{section.SectionName}' is not editable, cannot confirm selection.");
        //				continue;
        //			}
        //			currentSection = section;
        //			Console.WriteLine($"Confirmed current section: {section.SectionName}");
        //			return;
        //		}

        //		// Validate choice range
        //		if (choice < 1 || choice > section.children.Count)
        //		{
        //			Console.WriteLine($"Invalid selection. Please enter a number between 0 and {section.children.Count}.");
        //			continue;
        //		}

        //		DocumentComponent selected = section.children[choice - 1];

        //		// Prevent selecting a leaf node (DocumentItem) as a section.
        //		if (selected is DocumentItem item)
        //		{
        //			Console.WriteLine($"Error: '{item.Content}' is an item and cannot be selected as a section.");
        //			continue;
        //		}

        //		// Handle section selection (only DocumentSections can be navigated into)
        //		if (selected is DocumentSection childSection)
        //		{
        //			if (!childSection.IsEditable)
        //			{
        //				Console.WriteLine($"Error: Section '{childSection.SectionName}' is not editable.");
        //				continue;
        //			}
        //			// Recursively navigate into the editable section.
        //			selectSection(childSection, level + 1);
        //			return;
        //		}

        //		Console.WriteLine("Selected component type is not supported."); // Fallback error
        //		return;
        //	}
        //}

        //     public void selectSection(DocumentSection section, int level)
        //     {
        //Console.WriteLine("STUPID ASS" + rootsection.SectionName);
        //         Console.WriteLine($"\nYou are in section: {section.SectionName}");

        //         if (section.children.Count == 0)
        //         {
        //             if (!section.IsEditable)
        //             {
        //                 Console.WriteLine("This section is not editable. Cannot select it.");
        //                 return;
        //             }
        //             currentSection = section;
        //             Console.WriteLine("No sub-sections available. Selected current section: " + section.SectionName);
        //             return;
        //         }

        //         Console.WriteLine("Available components:");
        //         for (int i = 0; i < section.children.Count; i++)
        //         {
        //             DocumentComponent comp = section.children[i];
        //             if (comp is DocumentSection ds)
        //             {
        //                 string editableMark = ds.IsEditable ? "" : " (Not Editable)";
        //                 Console.WriteLine($"{i + 1}. Section: {ds.SectionName}{editableMark}");
        //             }
        //             else if (comp is DocumentItem di)
        //             {
        //                 string editableMark = di.IsEditable ? "" : " (Not Editable)";
        //                 Console.WriteLine($"{i + 1}. [Leaf] Item: {di.Content}{editableMark} (Cannot be selected as a section)");
        //             }
        //         }

        //         // Show the undo option in the prompt.
        //         Console.WriteLine("Type 'u' to undo the last change,");
        //         while (true)
        //         {
        //             Console.Write($"Enter the number of the component to select (0 to confirm current section, 1-{section.children.Count} to navigate) or 'u' to undo: ");
        //             string input = Console.ReadLine().Trim();

        //             // Check for the undo command.
        //             if (input.Equals("u", StringComparison.OrdinalIgnoreCase))
        //             {
        //                 // Call your undo logic here. For example:
        //                 // history.Undo(this);  // if your Document has a History caretaker method.
        //                 // Or if you want to simply signal that undo is requested:
        //                 Console.WriteLine("Undo command received. Reverting to the previous state...");
        //                 // If your Document class has a method like "UndoLastEdit()":
        //                 // In your undo command:
        //                 DocumentMemento memento = history.Undo(); // Retrieves the last snapshot
        //                 //Console.WriteLine("IN doc.cs" + );
        //                 if (memento != null)
        //                 {
        //                     restoreMemento(memento);
        //                     Console.WriteLine("Undo successful.");
        //                 }
        //                 else
        //                 {
        //                     Console.WriteLine("No mementos to undo.");
        //                 }
        //                 // After undoing, re-read the restored state and re-display the selection prompt.
        //                 // Use the restored state from the Document instance.
        //                 DocumentSection updatedSection = (level == 1) ? this.rootsection : this.currentSection;
        //                 Console.WriteLine($"\n[Updated] You are in section: {updatedSection.SectionName}");
        //                 // Recurse to re-display the available components.
        //                 selectSection(updatedSection, level);
        //                 return;

        //                 // After undoing, re-display the same section selection prompt.
        //                 //selectSection(section, level);
        //                 //return;
        //                 // Optionally, after undoing, you might want to display the (restored) section name.
        //                 //if (currentSection == null) // If the current section is null, default to rootsection.
        //                 //               {
        //                 //                   currentSection = rootsection;
        //                 //               }
        //                 //               Console.WriteLine($"Current section after undo: {currentSection?.SectionName ?? "None"}");
        //                 //               return;
        //             }

        //             // Try parsing the input as a number.
        //             if (!int.TryParse(input, out int choice))
        //             {
        //                 Console.WriteLine("Invalid input. Please enter a numeric value or 'u' to undo.");
        //                 continue;
        //             }

        //             // If choice is 0: confirm the current section.
        //             if (choice == 0)
        //             {
        //                 if (!section.IsEditable)
        //                 {
        //                     Console.WriteLine($"Error: Section '{section.SectionName}' is not editable, cannot confirm selection.");
        //                     continue;
        //                 }
        //                 currentSection = section;
        //                 Console.WriteLine($"Confirmed current section: {section.SectionName}");
        //                 return;
        //             }

        //             // Validate choice range.
        //             if (choice < 1 || choice > section.children.Count)
        //             {
        //                 Console.WriteLine($"Invalid selection. Please enter a number between 0 and {section.children.Count}.");
        //                 continue;
        //             }

        //             DocumentComponent selected = section.children[choice - 1];

        //             // Prevent selecting a leaf node (DocumentItem) as a section.
        //             if (selected is DocumentItem item)
        //             {
        //                 Console.WriteLine($"Error: '{item.Content}' is an item and cannot be selected as a section.");
        //                 continue;
        //             }

        //             // Handle section selection (only DocumentSections can be navigated into)
        //             if (selected is DocumentSection childSection)
        //             {
        //                 if (!childSection.IsEditable)
        //                 {
        //                     Console.WriteLine($"Error: Section '{childSection.SectionName}' is not editable.");
        //                     continue;
        //                 }
        //                 // Recursively navigate into the editable section.
        //                 selectSection(childSection, level + 1);
        //                 return;
        //             }

        //             Console.WriteLine("Selected component type is not supported."); // Fallback error.
        //             return;
        //         }
        //     }

        //public bool selectSection(DocumentSection section, int level)
        //{
        //    //Console.WriteLine("\n=== Select Section Debug ===");
        //    //Console.WriteLine($"Attempting to select section: {section.SectionName}");
        //    //Console.WriteLine($"Current level: {level}");
        //    //Console.WriteLine("Section children:");
        //    //foreach (var child in section.children)
        //    //{
        //    //    if (child is DocumentSection ds)
        //    //        Console.WriteLine($"- Section: {ds.SectionName}");
        //    //    else if (child is DocumentItem di)
        //    //        Console.WriteLine($"- Item: {di.Content}");
        //    //}

        //    // If we're at the top level, update the section from the restored document.
        //    if (level == 1)
        //    {
        //        section = this.rootsection;
        //    }

        //    Console.WriteLine($"\nYou are in section: {section.SectionName}");

        //    if (section.children.Count == 0)
        //    {
        //        if (!section.IsEditable)
        //        {
        //            Console.WriteLine("This section is not editable. Cannot select it.");
        //            return false;
        //        }
        //        currentSection = section;
        //        Console.WriteLine("No sub-sections available. Selected current section: " + section.SectionName);
        //        return false;
        //    }

        //    // Display available components.
        //    Console.WriteLine("Available components:");
        //    for (int i = 0; i < section.children.Count; i++)
        //    {
        //        DocumentComponent comp = section.children[i];
        //        if (comp is DocumentSection ds)
        //        {
        //            string editableMark = ds.IsEditable ? "" : " (Not Editable)";
        //            Console.WriteLine($"{i + 1}. Section: {ds.SectionName}{editableMark}");
        //        }
        //        else if (comp is DocumentItem di)
        //        {
        //            string editableMark = di.IsEditable ? "" : " (Not Editable)";
        //            Console.WriteLine($"{i + 1}. [Leaf] Item: {di.Content}{editableMark} (Cannot be selected as a section)");
        //        }
        //    }

        //    // Show the undo and save options.
        //    Console.WriteLine("Type 'u' to undo the last change, or 's' to save changes (which clears undo history).");

        //    while (true)
        //    {
        //        Console.Write($"Enter the number of the component to select (0 to confirm current section, 1-{section.children.Count} to navigate): ");
        //        string input = Console.ReadLine().Trim();

        //        // Check for the undo command.
        //        if (input.Equals("u", StringComparison.OrdinalIgnoreCase))
        //        {
        //            Console.WriteLine("Undo command received. Reverting to the previous state...");
        //            DocumentMemento memento = history.Undo(); // Retrieves the last snapshot.
        //            if (memento != null)
        //            {
        //                restoreMemento(memento);
        //                Console.WriteLine("Undo successful.");
        //            }
        //            else
        //            {
        //                Console.WriteLine("No mementos to undo.");
        //            }
        //            // After undoing, re-read the restored state and re-display the selection prompt.
        //            // Use the restored state from the Document instance.
        //            DocumentSection updatedSection = (level == 1) ? this.rootsection : this.currentSection;

        //            //Console.WriteLine($"\n[Updated] You are in section: {updatedSection.SectionName}");
        //            // Recurse to re-display the available components.
        //            selectSection(rootsection, level);
        //            return false;
        //        }

        //        // Check for the save command.
        //        if (input.Equals("s", StringComparison.OrdinalIgnoreCase))
        //        {
        //            Console.WriteLine("Save command received. Saving current state and clearing undo history...");
        //            // Clear the undo history.
        //            history.Clear();
        //            return true;
        //        }

        //        // Try parsing the input as a number.
        //        if (!int.TryParse(input, out int choice))
        //        {
        //            Console.WriteLine("Invalid input. Please enter a numeric value or 'u' to undo.");
        //            continue;
        //        }

        //        // Confirm the current section.
        //        if (choice == 0)
        //        {
        //            if (!section.IsEditable)
        //            {
        //                Console.WriteLine($"Error: Section '{section.SectionName}' is not editable, cannot confirm selection.");
        //                continue;
        //            }
        //            currentSection = section;
        //            Console.WriteLine($"Confirmed current section: {section.SectionName}");
        //            return false;
        //        }

        //        // Validate choice range.
        //        if (choice < 1 || choice > section.children.Count)
        //        {
        //            Console.WriteLine($"Invalid selection. Please enter a number between 0 and {section.children.Count}.");
        //            continue;
        //        }

        //        DocumentComponent selected = section.children[choice - 1];

        //        // Prevent selecting a leaf node as a section.
        //        if (selected is DocumentItem item)
        //        {
        //            Console.WriteLine($"Error: '{item.Content}' is an item and cannot be selected as a section.");
        //            continue;
        //        }

        //        // Handle section selection.
        //        if (selected is DocumentSection childSection)
        //        {
        //            if (!childSection.IsEditable)
        //            {
        //                Console.WriteLine($"Error: Section '{childSection.SectionName}' is not editable.");
        //                continue;
        //            }
        //            // Recursively navigate into the editable sub-section.
        //            selectSection(childSection, level + 1);
        //            return false;
        //        }

        //        Console.WriteLine("Selected component type is not supported."); // Fallback error.
        //        return false;
        //    }
        //}

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
