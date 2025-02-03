using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.States
{
    public interface DocumentState
    {
        public void edit();
        public void addCollaborator(User collaborator);
        public void nominateApprover(User approver);
        public void submitForApproval(User submitter);
        public void pushBack(string comment);
        public void approve();
        public void reject();
        public void resumeEditing();
        public void undoSubmission(User undoer);
    }
}
