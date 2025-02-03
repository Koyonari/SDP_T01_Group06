using Spectre.Console;

namespace SDP_T01_Group06
{
    class Program
    {
        static void Main(string[] args)
        {
            //User user = new User("John Doe");
            //User owner = new User("Alice");
            //Document techReport = new TechnicalReport(owner);
            //Document grantProposal = new GrantProposal(owner);
            //techReport.assembleDocument();

            //techReport.edit(); // Calls DraftState.edit(), then techReport.editContent()

            GrantProposalFactory grantProposalFactory = new GrantProposalFactory();
            TechnicalReportFactory tenicalReportFactory = new TechnicalReportFactory();

            //Document newDoc = owner.CreateDocument(grantProposalFactory);

            // Create users
            User alice = new User("Alice");
            User bob = new User("Bob");
            User charlie = new User("Charlie");

            // Create documents
            Document doc1 = grantProposalFactory.CreateDocument(alice);
            Document doc2 = grantProposalFactory.CreateDocument(bob);
            Document doc3 = grantProposalFactory.CreateDocument(alice);

            // Add collaborators
            doc1.addCollaborator(bob);
            doc2.addCollaborator(alice);
            doc3.addCollaborator(charlie);

            List<Document> allDocuments = new List<Document> { doc1, doc2, doc3 };

            User user = alice;
            // List Alice's associated and owned documents
            user.ListRelatedDocuments(allDocuments);

            user.ListOwnedDocuments(allDocuments);


            //grantProposal.edit(); // Calls DraftState.edit(), then grantProposal.editContent()
            //AnsiConsole.Write(
            //   new FigletText("Document Workflow System")
            //       .LeftJustified()
            //       .Color(Color.Blue));
            //while (true)
            //{
            //    var selectedOption = AnsiConsole.Prompt(
            //        new SelectionPrompt<string>()
            //            .Title("Document Workflow System")
            //            .PageSize(6)
            //            .AddChoices(
            //                "Create a new document",
            //                "Edit existing document",
            //                "View your documents",
            //                "Submit existing document for approval",
            //                "View existing document status",
            //                "Manage document review"));

            //    switch (selectedOption)
            //    {
            //        case "Create a new document":
            //            CreateNewDocument();
            //            break;
            //        case "Edit existing document":
            //            EditExistingDocument();
            //            break;
            //        case "View your documents":
            //            ViewDocuments();
            //            break;
            //        case "Submit existing document for approval":
            //            SubmitDocumentForApproval();
            //            break;
            //        case "View existing document status":
            //            ViewDocumentStatus();
            //            break;
            //        case "Manage document review":
            //            ManageDocumentReview();
            //            break;
            //    }
            //}
        }

        static void CreateNewDocument()
        {
            // Implement the logic to create a new document
            AnsiConsole.MarkupLine("[green]Creating a new document...[/]");
        }

        static void EditExistingDocument()
        {
            // Implement the logic to edit an existing document
            AnsiConsole.MarkupLine("[green]Editing an existing document...[/]");
        }

        static void ViewDocuments()
        {
            // Implement the logic to view the user's documents
            AnsiConsole.MarkupLine("[green]Viewing your documents...[/]");
        }

        static void SubmitDocumentForApproval()
        {
            // Implement the logic to submit a document for approval
            AnsiConsole.MarkupLine("[green]Submitting a document for approval...[/]");
        }

        static void ViewDocumentStatus()
        {
            // Implement the logic to view the status of a document
            AnsiConsole.MarkupLine("[green]Viewing the status of a document...[/]");
        }

        static void ManageDocumentReview()
        {
            // Implement the logic to manage the review of a document
            AnsiConsole.MarkupLine("[green]Managing the review of a document...[/]");
        }
    }
}