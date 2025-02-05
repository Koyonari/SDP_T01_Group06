using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    public class PushBackCommand : ApprovalCommand
    {
        private string comment;

        public PushBackCommand(Document document, User approver, string comment)
        : base(document, approver)
        {
            this.comment = comment;
        }

        public override void execute()
        {
            PreviousState = Document.getCurrentState();
            Document.pushBack(comment);

            //document.State = Document.DocumentState.PushedBack;
            //document.NotifyCollaborators($"Document pushed back: {comment}");
        }

        public override void undo()
        {
            Document.setCurrentState(PreviousState);


            //document.State = Document.DocumentState.UnderReview;
            //document.NotifyCollaborators("Push back undone.");
        }

    }
}
