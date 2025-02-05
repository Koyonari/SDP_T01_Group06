using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    public class RejectCommand : ApprovalCommand
    {
        public RejectCommand(Document document, User approver)
        : base(document, approver)
        {
        }

        public override void execute()
        {
            PreviousState = Document.getCurrentState();
            Document.reject();


            //document.State = Document.DocumentState.Rejected;
            //document.NotifyCollaborators("Document rejected.");
        }

        public override void undo()
        {
            Document.setCurrentState(PreviousState);


            //document.State = Document.DocumentState.Draft;
            //document.NotifyCollaborators("Rejection undone.");
        }
    }
}
