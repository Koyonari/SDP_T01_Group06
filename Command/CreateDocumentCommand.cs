using SDP_T01_Group06.Factory;
using SDP_T01_Group06.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal class CreateDocumentCommand : DocumentCommand
    {
        private DocumentState previousState;

        public CreateDocumentCommand(User user, List<Document> document)
        : base(user, document)
        {
        }

        public override void execute()
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
                doc1 = grantProposalFactory.CreateDocument(User);
            }
            else
            {
                TechnicalReportFactory tenicalReportFactory = new TechnicalReportFactory();
                doc1 = tenicalReportFactory.CreateDocument(User);
            }

            DraftState state = new DraftState(doc1);
            doc1.setCurrentState(state);

        }

        public override void undo()
        {

        }
    }
}
