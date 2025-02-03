using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.States
{
    public class RejectedState : DocumentState
    {
        private Document document;

        public RejectedState(Document document)
        {
            this.document = document;
        }

        public void edit()
        {
            Console.WriteLine("Document is approved. Cannot be edited.");
        }

        public void addCollaborator(User collaborator)
        {
            Console.WriteLine("Cannot add a collaborator to a rejected document.");
        }

        public void nominateApprover(User approver)
        {
            Console.WriteLine("Cannot nominate an approver in an rejected document.");
        }

        public void submitForApproval()
        {
            Console.WriteLine("Cannot submit an rejected document for approval.");
        }

        public void pushBack(string comment)
        {
            Console.WriteLine("Cannot push back a rejected document.");
        }

        public void approve()
        {
            Console.WriteLine("Cannot approve a document that is not under review.");
        }

        public void reject()
        {
            Console.WriteLine("Cannot reject an already rejected document.");
        }

        public void resumeEditing()
        {
            Console.WriteLine("Resuming editing of the document.");
            document.setCurrentState(new DraftState(document));
        }
    }
}
