using SDP_T01_Group06.Converter;
using SDP_T01_Group06.Factory;
using SDP_T01_Group06.Iterator;
using SDP_T01_Group06.Strategy;
using SDP_T01_Group06.Command;
using Spectre.Console;

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
            // Test Docs with Name manually set            

            Console.SetIn(new StringReader("GP1\n"));
            Document doc1 = grantProposalFactory.CreateDocument(user1);
            doc1.DocumentName = "GP1";
            user1.DocumentList.Add(doc1);
            Console.SetIn(new StringReader("TR1\n"));
            Document doc2 = tenicalReportFactory.CreateDocument(user2);
            doc2.DocumentName = "TR1";
            user2.DocumentList.Add(doc2);
            Console.SetIn(new StringReader("GP2\n"));
            Document doc3 = grantProposalFactory.CreateDocument(user3);
            doc3.DocumentName = "GP2";
            user3.DocumentList.Add(doc3);

            // 🔹 Restore standard input for manual entry
            Console.SetIn(originalConsoleIn);

            // Add collaborators
            doc1.addCollaborator(user2);
            doc1.addCollaborator(user3);
            //doc1.addCollaborator(user4);

            //doc2.addCollaborator(user2);
            //doc2.addCollaborator(user2);
            doc2.addCollaborator(user4);
            doc2.nominateApprover(user1);
            doc2.submitForApproval(user4);

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
                        LoggedInMenu(currentUser, allDocuments, allUsers);
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

        static void LoggedInMenu(User currentUser, List<Document> allDocuments, List<User> allUsers)
        {
            DocumentInvoker documentInvoker = new DocumentInvoker();
            ViewCommand viewOwnedCommand = new ViewCommand(currentUser, "Owned");
            ViewCommand viewAssociatedCommand = new ViewCommand(currentUser, "Associated");
            ViewCommand viewPendingCommand = new ViewCommand(currentUser, "Pending");
            ViewCommand viewStatusCommand = new ViewCommand(currentUser, "Status");

            documentInvoker.setHotkeys(viewOwnedCommand, 0);
            documentInvoker.setHotkeys(viewAssociatedCommand, 1);
            documentInvoker.setHotkeys(viewPendingCommand, 2);
            documentInvoker.setHotkeys(viewStatusCommand, 3);

            // CreateCommand Done
            //EditCommand editCommand = new EditCommand(currentUser);
            //ViewCommand viewCommand = new ViewCommand(currentUser);
            //NominateApproverCommand nominateApproverCommand = new NominateApproverCommand(currentUser, allUsers);
            //SubmitForApprovalCommand submitForApprovalCommand = new SubmitForApprovalCommand(currentUser);


            Console.Clear();
            AnsiConsole.Write(
               new FigletText("Document Workflow System")
                   .LeftJustified()
                   .Color(Color.Blue));

            // Show unread notifications count if any exist
            var unreadNotifications = currentUser.GetNotifications(unreadOnly: true);
            if (unreadNotifications.Any())
            {
                AnsiConsole.MarkupLine($"[yellow]You have {unreadNotifications.Count} unread notifications![/]");
            }

            while (true)
            {
                var selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Document Workflow System")
                        .PageSize(7)
                        .AddChoices(
                            "View Owned documents",
                            "View Associated documents",
                             "View Notifications",
                            "Create a new document",
                            "Edit existing document",
                            "Add Collaborator to a document",
                            "Nominate Approver for a document",
                            "Submit existing document for approval",
                            "View existing document status",
                            "View Documents Pending Your Approval",
                            "Review & Approve Document",
                            "Convert document",
                            "Undo",
                            "Log Out"
                            ));

                switch (selectedOption)
                {
                    case "View Owned documents":
                        ViewOwnedDocuments(currentUser);
                        break;
                    case "View Associated documents":
                        ViewAssociatedDocuments(currentUser);
                        break;
                    case "Create a new document":
                        CreateNewDocument(currentUser, allDocuments, documentInvoker);
                        break;
                    case "Edit existing document":
                        EditExistingDocument(currentUser, documentInvoker);
                        break;
                    case "Add Collaborator to a document":
                        AddCollaborator(currentUser, allUsers);
                        break;
                    case "View Documents Pending Your Approval":
                        ViewDocumentsAwaitingForApproval(currentUser);
                    case "View Owned documents":
                        ViewOwnedDocuments(currentUser, documentInvoker);
                        break;
                    case "View Associated documents":
                        ViewAssociatedDocuments(currentUser, documentInvoker);
                        break;
                    case "View Documents Awaiting For Your Review & Approval":
                        ViewDocumentsAwaitingForReview(currentUser, documentInvoker);
                        break;
                    case "Nominate Approver for a document":
                        NominateApproverForDocument(currentUser, allUsers, documentInvoker);
                        break;
                    case "Submit existing document for approval":
                        SubmitDocumentForApproval(currentUser, documentInvoker);
                        break;
                    case "View existing document status":
                        ViewDocumentStatus(currentUser, documentInvoker);
                        break;
                    case "Review & Approve Document":
                        ReviewDocument(currentUser, documentInvoker);
                        break;
                    case "Convert document":
                        ConvertDocument(currentUser, allDocuments, documentInvoker);
                        break;
                    case "Undo":
                        documentInvoker.undoCommand();
                        break;
                    case "View Notifications":
                        ViewNotifications(currentUser);
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

        static void ViewNotifications(User user)
        {
            var notifications = user.GetNotifications();

            if (!notifications.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No notifications to display.[/]");
                return;
            }

            var table = new Table();
            table.AddColumn("Status");
            table.AddColumn("Time");
            table.AddColumn("Message");

            foreach (var notification in notifications)
            {
                table.AddRow(
                    notification.IsRead ? "[green]Read[/]" : "[red]Unread[/]",
                    notification.Timestamp.ToString("g"),
                    notification.Message
                );
            }

            AnsiConsole.Write(table);

            if (user.GetNotifications(unreadOnly: true).Any())
            {
                if (AnsiConsole.Confirm("Mark all notifications as read?"))
                {
                    user.MarkAllNotificationsAsRead();
                    AnsiConsole.MarkupLine("[green]All notifications marked as read.[/]");
                }
            }
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
            foreach (var doc in docs)
            {
                AnsiConsole.MarkupLine($"[red]{doc.DocumentName} - {doc.Owner.Name} [/]");
            }
        }
        static void CreateNewDocument(User user, List<Document> allDocuments, DocumentInvoker documentInvoker)
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
            DocumentFactory factory;

            if (doctype == 1)
            {
                //GrantProposalFactory grantProposalFactory = new GrantProposalFactory();
                //doc1 = user.CreateDocument(grantProposalFactory);

                factory = new GrantProposalFactory();

            }
            else
            {
                //TechnicalReportFactory tenicalReportFactory = new TechnicalReportFactory();
                //doc1 = user.CreateDocument(tenicalReportFactory);

                factory = new TechnicalReportFactory();

            }
            // Create and execute the command using the chosen factory.
            CreateCommand createCommand = new CreateCommand(factory, user, allDocuments);
            documentInvoker.setCommand(createCommand);
            documentInvoker.executeCommand();

            // Retrieve the result from the command.
            doc1 = createCommand.getResult();

            allDocuments.Add(doc1);
        }

        static void EditExistingDocument(User user, DocumentInvoker documentInvoker)
        {
            Console.WriteLine("Available documents:");
            user.ListRelatedDocuments();

            if (user.DocumentList.Count == 0)
            {
                Console.WriteLine("No documents available for editing.");
                return;
            }

            int choice;
            bool isValid;
            do
            {
                Console.Write("Enter the index of the document to edit: ");
                isValid = int.TryParse(Console.ReadLine(), out choice);
                isValid = isValid && choice >= 1 && choice <= user.getNoOfRelatedDocuments();

                if (!isValid)
                {
                    Console.WriteLine($"Invalid input. Please enter a number between 1 and {user.getNoOfRelatedDocuments()}.");
                }
            } while (!isValid);

            Document selectedDoc = user.getRelatedDocument(choice-1);
            EditCommand editCommand = new EditCommand(selectedDoc);
            documentInvoker.setCommand(editCommand);
            documentInvoker.executeCommand();
            //selectedDoc.edit();
        }

        static void ViewOwnedDocuments(User user, DocumentInvoker documentInvoker)
        {
            // Implement the logic to view the user's documents
            AnsiConsole.MarkupLine("[green]Viewing Owned documents...[/]");
            documentInvoker.executeHotKey(0);
            Console.WriteLine();
            Console.WriteLine();
            //user.ListOwnedDocuments();
        }

        static void ViewAssociatedDocuments(User user, DocumentInvoker documentInvoker)
        {
            // Implement the logic to view the user's documents
            AnsiConsole.MarkupLine("[green]Viewing your documents...[/]");
            user.ListRelatedDocuments();
            Console.WriteLine();
            Console.WriteLine();
        }

        static void ViewDocumentsAwaitingForApproval(User user)
        {
            // Implement the logic to view documents awaiting review
            AnsiConsole.MarkupLine("[green]Viewing Documents Awaiting Approval...[/]");
            user.ListPendingDocsForReview();
            Console.WriteLine();
            Console.WriteLine();
        }

        static void AddCollaborator(User user, List<User> allUsers)
        {
            AnsiConsole.MarkupLine("[green]Add a Collaborator to a Document...[/]");

            Console.WriteLine("Available documents:");
            user.ListRelatedDocuments();

            if (user.DocumentList.Count == 0)
            {
                Console.WriteLine("No documents available for editing.");
                return;
            }

            int choice;
            bool isValid;
            do
            {
                Console.Write("Enter the index of the document to edit: ");
                isValid = int.TryParse(Console.ReadLine(), out choice);
                isValid = isValid && choice >= 1 && choice <= user.getNoOfRelatedDocuments();

                if (!isValid)
                {
                    Console.WriteLine($"Invalid input. Please enter a number between 1 and {user.getNoOfRelatedDocuments()}.");
                }
            } while (!isValid);

            Document selectedDoc = user.getRelatedDocument(choice - 1);

            // Display Users
            int position = 1;
            Console.WriteLine("Available Users:");
            for (int i = 0; i < allUsers.Count; i++)
            {
                Console.WriteLine($"{position}. {allUsers[i].Name}");
                position++;
            }
            Console.WriteLine();

            do
            {
                Console.Write("Enter the user index to be your Collaborator: ");
                isValid = int.TryParse(Console.ReadLine(), out choice);
                isValid = isValid && choice >= 1 && choice <= allUsers.Count;

                if (!isValid)
                {
                    Console.WriteLine($"Invalid input. Please enter a number between 1 and {allUsers.Count}.");
                }
            } while (!isValid);

            User selectedUser = allUsers[choice - 1];
            selectedDoc.addCollaborator(selectedUser);
            Console.WriteLine();
            Console.WriteLine();
//             documentInvoker.executeHotKey(1);
//             //user.ListRelatedDocuments();
        }

        static void ViewDocumentsAwaitingForReview(User user, DocumentInvoker documentInvoker)
        {
            // Implement the logic to view documents awaiting review
            AnsiConsole.MarkupLine("[green]Viewing documents awaiting review...[/]");
            documentInvoker.executeHotKey(2);
            //user.ListPendingDocsForReview();
        }

        static void NominateApproverForDocument(User user, List<User> allUsers, DocumentInvoker documentInvoker)
        {
            AnsiConsole.MarkupLine("[green]Nominate An Approver For a Document...[/]");

            Console.WriteLine("Available documents:");
            documentInvoker.executeHotKey(1);
            //user.ListRelatedDocuments();

            if (user.DocumentList.Count == 0)
            {
                Console.WriteLine("No documents available for editing.");
                return;
            }

            int choice;
            bool isValid;
            do
            {
                Console.Write("Enter the index of the document to edit: ");
                isValid = int.TryParse(Console.ReadLine(), out choice);
                isValid = isValid && choice >= 1 && choice <= user.getNoOfRelatedDocuments();

                if (!isValid)
                {
                    Console.WriteLine($"Invalid input. Please enter a number between 1 and {user.getNoOfRelatedDocuments()}.");
                }
            } while (!isValid);

            Document selectedDoc = user.getRelatedDocument(choice - 1);

            // Display Users
            int position = 1;
            Console.WriteLine("Available approvers:");
            for (int i = 0; i < allUsers.Count; i++)
            {
                Console.WriteLine($"{position}. {allUsers[i].Name}");
                position++;
            }
            Console.WriteLine();

            do
            {
                Console.Write("Enter the user index to be your Approver: ");
                isValid = int.TryParse(Console.ReadLine(), out choice);
                isValid = isValid && choice >= 1 && choice <= allUsers.Count;

                if (!isValid)
                {
                    Console.WriteLine($"Invalid input. Please enter a number between 1 and {allUsers.Count}.");
                }
            } while (!isValid);

            User selectedUser = allUsers[choice - 1];

            NominateApproverCommand nominateApproverCommand = new NominateApproverCommand(selectedDoc, selectedUser);
            documentInvoker.setCommand(nominateApproverCommand);
            documentInvoker.executeCommand();
            //selectedDoc.nominateApprover(selectedUser);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        static void SubmitDocumentForApproval(User user, DocumentInvoker documentInvoker)
        {
            // Implement the logic to submit a document for approval
            AnsiConsole.MarkupLine("[green]Submitting a document for approval...[/]");

            Console.WriteLine("Available documents:");
            documentInvoker.executeHotKey(1);
            //user.ListRelatedDocuments();

            int choice;
            bool isValid;
            do
            {
                Console.Write("Enter the index of the document to edit: ");
                isValid = int.TryParse(Console.ReadLine(), out choice);
                isValid = isValid && choice >= 1 && choice <= user.getNoOfRelatedDocuments();

                if (!isValid)
                {
                    Console.WriteLine($"Invalid input. Please enter a number between 1 and {user.getNoOfRelatedDocuments()}.");
                }
            } while (!isValid);

            Document selectedDoc = user.getRelatedDocument(choice - 1);

            SubmitForApprovalCommand submitForApprovalCommand = new SubmitForApprovalCommand(selectedDoc, user);
            documentInvoker.setCommand(submitForApprovalCommand);
            documentInvoker.executeCommand();
            //selectedDoc.submitForApproval(user);
        }

        static void ViewDocumentStatus(User user, DocumentInvoker documentInvoker)
        {
            // Implement the logic to view the status of a document
            AnsiConsole.MarkupLine("[green]Viewing the status of a document...[/]");
            documentInvoker.executeHotKey(3);
            //user.ListRelatedDocumentStatus();
        }

        static void ReviewDocument(User user, DocumentInvoker documentInvoker)
        {
            // Display Header
            AnsiConsole.MarkupLine("[green]Managing the review of a document...[/]");

            // Show Pending Documents
            Console.WriteLine("\nDocuments To Review:");
            documentInvoker.executeHotKey(2);
            //user.ListPendingDocsForReview();

            // Get the user’s choice of document
            int choice;
            do
            {
                if (user.getNoOfPendingDocuments() == 0) { Console.WriteLine(""); return; }
                choice = AnsiConsole.Ask<int>($"Enter the index of the document to edit (1-{user.getNoOfPendingDocuments()}): ");

                if (choice < 1 || choice > user.getNoOfPendingDocuments())
                {
                    Console.WriteLine("Invalid selection. Please choose a valid index.");
                }

            } while (choice < 1 || choice > user.getNoOfPendingDocuments());

            // Retrieve the selected document
            Document selectedDoc = user.getPendingDocument(choice - 1);
            Console.WriteLine($"\nSelected Document: {selectedDoc.DocumentName}");
            selectedDoc.displayDocumentContent();

            // Ask for action (Approve, Pushback with Comment, Reject)
            var action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do with this document?")
                    .AddChoices("Approve", "Pushback with Comment", "Reject"));

            // Handle the selected action
            switch (action)
            {
                case "Approve":
                    ApproveCommand approveCommand = new ApproveCommand(selectedDoc);
                    documentInvoker.setCommand(approveCommand);
                    documentInvoker.executeCommand();
                    //selectedDoc.approve();
                    break;

                case "Pushback with Comment":
                    string comment = AnsiConsole.Ask<string>("Enter your comment for pushback: ");
                    PushBackCommand pushBackCommand = new PushBackCommand(selectedDoc, comment);
                    documentInvoker.setCommand(pushBackCommand);
                    documentInvoker.executeCommand();
                    //selectedDoc.pushBack(comment);
                    break;

                case "Reject":
                    RejectCommand rejectCommand = new RejectCommand(selectedDoc);
                    documentInvoker.setCommand(rejectCommand);
                    documentInvoker.executeCommand();
                    //selectedDoc.reject();
                    break;
            }

            Console.WriteLine("\nReview process completed successfully.");
        }

        static void ConvertDocument(User user, List<Document> allDocuments, DocumentInvoker documentInvoker)
        {

            documentInvoker.executeHotKey(1);
            //user.ListRelatedDocuments();

            // Display available documents
            //DocumentIterator iterator = new AssociatedDocumentsIterator(user);
            //List<Document> availableDocs = new List<Document>();

            // Get only the documents the user is associated with
            List<Document> associatedDocs = new List<Document>();
            foreach (Document doc in user.DocumentList)
            {
                if (doc.Owner == user || doc.Collaborators.Contains(user))
                {
                    associatedDocs.Add(doc);
                }
            }

            if (associatedDocs.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No documents available for conversion.[/]");
                return;
            }

            AnsiConsole.MarkupLine("\n[blue]Available documents for conversion:[/]");

            var table = new Table();
            table.AddColumn(new TableColumn("Number").Centered());
            table.AddColumn(new TableColumn("Document Name"));

            // Add only associated documents to the table
            for (int i = 0; i < associatedDocs.Count; i++)
            {
                table.AddRow((i + 1).ToString(), associatedDocs[i].DocumentName);
            }

            AnsiConsole.Write(table);

            // Select document using AnsiConsole prompt
            var docChoice = AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("Select a document to convert:")
                    .AddChoices(Enumerable.Range(1, associatedDocs.Count))
                    .UseConverter(i => $"{i}. {associatedDocs[i - 1].DocumentName}"));

            // Select conversion format using AnsiConsole prompt
            var formatChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select conversion format:")
                    .AddChoices(new[] { "Word", "PDF" }));

            var converter = new DocumentConverter(); // Will use PDF by default

//             //var converter = new DocumentConverter();
//             Document chosenDoc = user.DocumentList[docChoice - 1];

//             // Declare a variable to hold the conversion command
//             IResultCommand conversionCommand = null;

            // Set conversion strategy based on selection
            if (formatChoice == "Word")
            {
                converter.SetStrategy(new WordConverter());
            }
            else if (formatChoice == "PDF")
            {
                converter.SetStrategy(new PDFConverter());
//                 case "Word":
//                     WordConverter wordConverter = new WordConverter();
//                     conversionCommand = new ConvertToWordCommand(user, chosenDoc, wordConverter);
//                     documentInvoker.setCommand(conversionCommand);
//                     //converter.SetStrategy(new WordConverter());
//                     break;
//                 case "PDF":
//                     PDFConverter pdfConverter = new PDFConverter();
//                     conversionCommand = new ConvertToPDFCommand(user, chosenDoc, pdfConverter);
//                     documentInvoker.setCommand(conversionCommand);
//                     //converter.SetStrategy(new PDFConverter());
//                     break;
            }

            try
            {
                // Show spinner during conversion
                var convertedDoc = AnsiConsole.Status()
                    .Start("Converting document...", ctx =>
                    {
                        ctx.Spinner(Spinner.Known.Dots);
                        ctx.SpinnerStyle(Style.Parse("green"));
                        return converter.convert(associatedDocs[docChoice - 1]);
//                         documentInvoker.executeCommand();
//                         return conversionCommand.getResult();
//                         //return converter.convert(user.DocumentList[docChoice - 1]);
                    });

                user.AddDocument(convertedDoc);
                allDocuments.Add(convertedDoc);
                AnsiConsole.MarkupLine($"\n[green]Document converted successfully:[/] {convertedDoc.DocumentName}");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"\n[red]Error converting document:[/] {ex.Message}");
            }
        }
    }
}