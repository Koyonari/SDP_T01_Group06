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
            if (!document.Collaborators.Contains(collaborator) && collaborator != document.Approver && collaborator != document.Owner)
            {
                document.Collaborators.Add(collaborator);
                collaborator.AddDocument(document);
                Console.WriteLine($"{collaborator.Name} has been added as a collaborator.");
            }
            else if (collaborator == document.Approver)
            {
                Console.WriteLine($"{collaborator.Name} cannot be a collaborator and an approver.");
            }
            else if (collaborator == document.Owner)
            {
                Console.WriteLine($"{collaborator.Name} cannot be a collaborator and the owner.");
            }
            else
            {
                Console.WriteLine($"{collaborator.Name} is already a collaborator.");
            }
        }

        public void nominateApprover(User approver)
        {
            Console.WriteLine("Cannot nominate an approver while the document is under review.");
        }

        public void submitForApproval(User submitter)
        {
            Console.WriteLine("Document is already under review and cannot be submitted again.");
        }

        public void pushBack(string comment)
        {
            Console.WriteLine("Document pushed back with comment: " + comment);
            document.setCurrentState(new DraftState(document));
            document.previouslyRejected = false;
            document.Approver.DocumentList.Remove(document);
        }

        public void approve()
        {
            Console.WriteLine("Document approved.");
            document.setCurrentState(new ApprovedState(document));
            document.previouslyRejected = false;
            document.Approver.removeDocument(document);
        }

        public void reject()
        {
            Console.WriteLine("Document rejected.");
            document.setCurrentState(new DraftState(document));
            document.previouslyRejected = true;
            document.isEdited = false;
            document.Approver.DocumentList.Remove(document);
        }

        public void resumeEditing()
        {
            Console.WriteLine("Cannot resume editing of a document that is under review.");
        }

        public void undoSubmission(User undoer)
        {
            Console.WriteLine($"Submission has been undone by {undoer.Name} for approval.");
            document.setCurrentState(new DraftState(document));
        }
    }
}
