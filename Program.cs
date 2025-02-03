using SDP_T01_Group06.Factory;
using Spectre.Console;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace SDP_T01_Group06
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Document> allDocuments = new List<Document>();
            List<User> allUsers = new List<User>();
            GrantProposalFactory grantProposalFactory = new GrantProposalFactory();
            TechnicalReportFactory tenicalReportFactory = new TechnicalReportFactory();

            // Create users
            User user1 = new User("John");
            User user2 = new User("Alice");
            User user3 = new User("Bob");
            User user4 = new User("Charles");

            allUsers.Add(user1);
            allUsers.Add(user2);
            allUsers.Add(user3);
            allUsers.Add(user4);
            
            // Store original console input before setting simulated input
            TextReader originalConsoleIn = Console.In;

            // Create documents
            // Simulate user input (this will replace Console.ReadLine())
            Console.SetIn(new StringReader("GP1\n"));
            Document doc1 = grantProposalFactory.CreateDocument(user1);
            Console.SetIn(new StringReader("TR1\n"));
            Document doc2 = tenicalReportFactory.CreateDocument(user2);
            Console.SetIn(new StringReader("GP2\n"));
            Document doc3 = grantProposalFactory.CreateDocument(user3);

            // 🔹 Restore standard input for manual entry
            Console.SetIn(originalConsoleIn);

            // Add collaborators
            doc1.addCollaborator(user2);
            doc1.addCollaborator(user3);
            //doc1.addCollaborator(user4);

            //doc2.addCollaborator(user2);
            //doc2.addCollaborator(user2);
            doc2.addCollaborator(user4);

            doc3.addCollaborator(user1);
            doc3.addCollaborator(user2);
            doc3.addCollaborator(user3);

            //Add Documents to List
            allDocuments.Add(doc1);
            allDocuments.Add(doc2);
            allDocuments.Add(doc3);

            

            Console.Clear();

            AnsiConsole.Write(
               new FigletText("Document Workflow System")
                   .LeftJustified()
                   .Color(Color.Blue));
            while (true)
            {
                User currentUser = null;
                var selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Document Workflow System")
                        .PageSize(4)
                        .AddChoices(
                            "Create new User",
                            "Login as a User",
                            "List All User",
                            "List All Documents"
                            ));

                switch (selectedOption)
                {
                    case "Create new User":
                        CreateNewUser(allUsers);
                        break;
                    case "Login as a User":
                        currentUser = LoginAsUser(allUsers);
                        LoggedInMenu(currentUser, allDocuments);
                        break;
                    case "List All User":
                        ListAllUsers(allUsers);
                        break;
                    case "List All Documents":
                        ListAllDocs(allDocuments);
                        break;
                }
            }

        }

        static void LoggedInMenu(User currentUser, List<Document> allDocuments)
        {
            Console.Clear();
            AnsiConsole.Write(
               new FigletText("Document Workflow System")
                   .LeftJustified()
                   .Color(Color.Blue));
            while (true)
            {
                var selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Document Workflow System")
                        .PageSize(7)
                        .AddChoices(
                            "Create a new document",
                            "Edit existing document",
                            "View Owned documents",
                            "View Associated documents",
                            "Submit existing document for approval",
                            "View existing document status",
                            "Manage document review",
                            "Log Out"
                            ));

                switch (selectedOption)
                {
                    case "Create a new document":
                        CreateNewDocument(currentUser, allDocuments); 
                        break;
                    case "Edit existing document":                        
                        EditExistingDocument(currentUser, allDocuments);                        
                        break;
                    case "View Owned documents":
                        ViewOwnedDocuments(currentUser, allDocuments);
                        break;
                    case "View Associated documents":
                        ViewAssociatedDocuments(currentUser, allDocuments);                        
                        break;
                    case "Submit existing document for approval":
                        SubmitDocumentForApproval(currentUser, allDocuments);                        
                        break;
                    case "View existing document status":
                        ViewDocumentStatus(currentUser, allDocuments);                        
                        break;
                    case "Manage document review":                   
                        ManageDocumentReview(currentUser, allDocuments);                      
                        break;
                    case "Log Out":
                        currentUser = null;
                        return;
                }
            }
        }
        static void CreateNewUser(List<User> users)
        {
            Console.Write("Enter new User's name: ");
            string name = Console.ReadLine();
            while (name == null || name == "")
            {
                Console.WriteLine("Enter in a Name.");
                Console.Write("Enter new User's name: ");
                name = Console.ReadLine();
            }
            User newUser = new User(name);
            users.Add(newUser);
        }

        static User LoginAsUser(List<User> users)
        {
            string name;
            Console.Write("Enter a User's Name: ");
            name = Console.ReadLine();

            // Ensure name is not empty
            while (string.IsNullOrWhiteSpace(name))
            {
                AnsiConsole.MarkupLine("[red]Enter a Name....[/]");
                Console.Write("Enter a User's Name: ");
                name = Console.ReadLine();
            }

            // Search for the user
            foreach (User user in users)
            {
                if (user.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    AnsiConsole.MarkupLine($"[green]Logged in as {user.Name}[/]");
                    return user;
                }
            }

            // If user is not found, ask again
            AnsiConsole.MarkupLine($"[red]User not found. Try again.[/]");
            return LoginAsUser(users); // Recursive call until a valid user is found

        }

        static void ListAllUsers(List<User> users)
        {
            foreach (var user in users)
            {
                AnsiConsole.MarkupLine($"[blue]{user}[/]");
            }            
            return;
        }
        static void ListAllDocs(List<Document> docs)
        {
            foreach (var doc in docs){
                AnsiConsole.MarkupLine($"[red]{doc.Documentname} - {doc.Owner.Name} [/]"); 
            }
        }
        static void CreateNewDocument(User user, List<Document> docs)
        {
            Console.WriteLine("Document Types");
            Console.WriteLine("1. Grant Proposal");
            Console.WriteLine("2. Technical Report");
			int doctype = 0;
			bool isValidInput = false;

			while (!isValidInput)
			{
				Console.Write("Enter document type: ");
				string input = Console.ReadLine();

				// check for valid int
				if (int.TryParse(input, out doctype))
				{
					// if the integer is either 1 or 2, valid
					if (doctype == 1 || doctype == 2)
					{
						isValidInput = true;
					}
					else
					{
						Console.WriteLine("Please enter either 1 or 2.");
					}
				}
				else
				{
					Console.WriteLine("Invalid input. Please enter a valid number.");
				}
			}
			Document doc1;
            if (doctype == 1)
            {
				GrantProposalFactory grantProposalFactory = new GrantProposalFactory();
				doc1 = grantProposalFactory.CreateDocument(user);
			}
			else
			{
				TechnicalReportFactory tenicalReportFactory = new TechnicalReportFactory();
                doc1 = tenicalReportFactory.CreateDocument(user);
			}

		}

        static void EditExistingDocument(User user, List<Document> docs)
        {
            // Implement the logic to edit an existing document
            AnsiConsole.MarkupLine("[green]Editing an existing document...[/]");
        }

        static void ViewOwnedDocuments(User user, List<Document> docs)
        {
            // Implement the logic to view the user's documents
            AnsiConsole.MarkupLine("[green]Viewing Owned documents...[/]");
            user.ListOwnedDocuments(docs);
            
        }

        static void ViewAssociatedDocuments(User user, List<Document> docs)
        {
            // Implement the logic to view the user's documents
            AnsiConsole.MarkupLine("[green]Viewing your documents...[/]");
            user.ListRelatedDocuments(docs);
            
        }

        static void SubmitDocumentForApproval(User user, List<Document> docs)
        {
            // Implement the logic to submit a document for approval
            AnsiConsole.MarkupLine("[green]Submitting a document for approval...[/]");
        }

        static void ViewDocumentStatus(User user, List<Document> docs)
        {
            // Implement the logic to view the status of a document
            AnsiConsole.MarkupLine("[green]Viewing the status of a document...[/]");            
        }

        static void ManageDocumentReview(User user, List<Document> docs)
        {
            // Implement the logic to manage the review of a document
            AnsiConsole.MarkupLine("[green]Managing the review of a document...[/]");
        }
    }
}