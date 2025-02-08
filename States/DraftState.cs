using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.States
{
    public class DraftState : DocumentState
    {
        private Document document;

        public DraftState(Document document)
        {
            this.document = document;
        }

        public void edit()
        {
            document.isEdited = true;
            document.editDocument();
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
            if (document.Collaborators.Contains(approver) || document.Owner == approver)
            {
                Console.WriteLine("Approver cannot be a collaborator or the owner.");
                return;
            }

            document.Approver?.removeDocument(document);

            document.Approver = approver;
            Console.WriteLine("Approver nominated.");
        }

        public void submitForApproval(User submitter)
        {
            if (document.hasApprover())
            {
                Console.WriteLine("Document submitted for approval.");
                document.setCurrentState(new UnderReviewState(document));
                document.Approver.AddDocument(document);
                document.Submitter = submitter;
            }
            else
            {
                Console.WriteLine("Document cannot be submitted without an approver.");
            }
        }

        public void pushBack(string comment)
        {
            Console.WriteLine("Cannot push back a document that is not under review.");
        }

        public void approve()
        {
            Console.WriteLine("Cannot approve a document that is not under review.");
        }

        public void reject()
        {
            Console.WriteLine("Cannot reject a document that is not under review.");
        }

        public void resumeEditing()
        {
            Console.WriteLine("Cannot resume editing for a draft document.");
        }

        public void undoSubmission(User undoer)
        {
            Console.WriteLine("Cannot undo submission for a draft document.");
        }
    }
}
