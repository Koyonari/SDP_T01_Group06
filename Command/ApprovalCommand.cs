using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Command
{
    public abstract class ApprovalCommand : Command
    {
        private Document document;
        private User approver;  // The user that is acting as the approver
        //private string comment;

        // We store the previous state so we can restore it in Undo.
        private DocumentState previousState;

        public Document Document
        {
            get { return document; }
            set { document = value; }
        }
        public User Approver
        {
            get { return approver; }
            set { approver = value; }
        }
        public DocumentState PreviousState
        {
            get { return previousState; }
            set { previousState = value; }
        }
        // Common constructor
        public ApprovalCommand(Document document, User approver)
        {
            this.document = document;
            this.approver = approver;
        }

        // Implementations of execute() and undo() will be provided in subclasses
        public abstract void execute();
        public abstract void undo();
    }
}
