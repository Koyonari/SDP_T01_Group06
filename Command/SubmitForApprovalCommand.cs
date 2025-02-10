using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.States;
using SDP_T01_Group06.Factory;

namespace SDP_T01_Group06.Command
{
    public class SubmitForApprovalCommand : ICommand
    {
        private Document document;
        private User user;
        //private DocumentState previousState;

        public SubmitForApprovalCommand(Document document, User user)
        {
            this.document = document;
            this.user = user;
        }

        public void execute()
        {
            document.submitForApproval(user);
            // Save the current state
            //previousState = Document.getCurrentState();
            //Document.submitForApproval(User);



            // Validate approver and update state.
            //document.submitForApproval();


            //Notify collaborators???


            //document.State = Document.DocumentState.UnderReview;
            //document.NotifyCollaborators("Document submitted for approval.");
        }

        public void undo()
        {
            document.undoSubmission(user);
            // Undo
            // Restore the previous state.
            //Document.undoSubmission(User);

            // do i need to check if undoing submission is successful? (idts)
            //Document.setCurrentState(previousState);




            // Revert state change.
            //document.State = Document.DocumentState.Draft;
            //document.NotifyCollaborators("Submission for approval undone.");
        }
        public bool isUndoable()
        {
            return true;
        }
    }
}
