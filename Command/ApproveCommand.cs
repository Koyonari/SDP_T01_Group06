using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06;

namespace SDP_T01_Group06.Command
{
    public class ApproveCommand : ApprovalCommand
    {
        public ApproveCommand(Document document, User approver)
        : base(document, approver)
        {
        }

        public override void execute()
        {
            PreviousState = Document.getCurrentState();
            Document.approve();

            //document.State = Document.DocumentState.Approved;
            //document.NotifyCollaborators("Document approved.");
        }

        public override void undo()
        {
            Document.setCurrentState(PreviousState);


            //document.State = Document.DocumentState.UnderReview;
            //document.NotifyCollaborators("Approval undone.");
        }
    }
}
