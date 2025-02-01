using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06
{
    public interface DocumentState
    {
        public void edit();
        public void addCollaborator(User Collaborator);
        public void nominateApprover(User Approver);
        public void submitForApproval();
        public void pushBack(string comment);
        public void approve();
        public void reject();
    }
}
