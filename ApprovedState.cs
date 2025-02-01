using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06
{
    public class ApprovedState : DocumentState
    {
        private Document document;

        public ApprovedState(Document document)
        {
            this.document = document;
        }

        public void edit()
        {
            Console.WriteLine("Document is approved. Cannot be edited.");
        }

        public void addCollaborator(User collaborator)
        {
            Console.WriteLine("Cannot add a collaborator to an approved document.");
        }

        public void nominateApprover(User approver)
        {
            Console.WriteLine("Cannot nominate an approver for an approved document.");
        }

        public void submitForApproval()
        {
            Console.WriteLine("Cannot submit an approved document for approval.");
        }

        public void pushBack(string comment)
        {
            Console.WriteLine("Cannot push back an approved document.");
        }

        public void approve()
        {
            Console.WriteLine("Document is already approved.");
        }

        public void reject()
        {
            Console.WriteLine("Cannot reject an approved document.");
        }
    }
}
