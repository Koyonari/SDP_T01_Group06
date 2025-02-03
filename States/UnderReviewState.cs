using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.States
{
    public class UnderReviewState : DocumentState
    {
        private Document document;

        public UnderReviewState(Document document)
        {
            this.document = document;
        }

        public void edit()
        {
            Console.WriteLine("Document is under review. Cannot be edited.");
        }

        public void addCollaborator(User collaborator)
        {
            document.Collaborators.Add(collaborator);
            Console.WriteLine($"{collaborator.Name} has been added as a collaborator.");
        }

        public void nominateApprover(User approver)
        {
            Console.WriteLine("Cannot nominate an approver while the document is under review.");
        }

        public void submitForApproval()
        {
            Console.WriteLine("Document is already under review and cannot be submitted again.");
        }

        public void pushBack(string comment)
        {
            Console.WriteLine("Document pushed back with comment: " + comment);
            document.setCurrentState(new DraftState(document));
            document.previouslyRejected = false;
        }

        public void approve()
        {
            Console.WriteLine("Document approved.");
            document.setCurrentState(new ApprovedState(document));
            document.previouslyRejected = false;
        }

        public void reject()
        {
            Console.WriteLine("Document rejected.");
            document.setCurrentState(new DraftState(document));
            document.previouslyRejected = true;
            document.isEdited = false;
        }
    }
}
