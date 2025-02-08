using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal class NominateApproverCommand : ICommand
    {
        private Document document;
        private User user;
        public NominateApproverCommand(Document document, User user)
        {
            this.user = user;
        }
        public void execute()
        {
            // Add approver to the project
            document.nominateApprover(user);
        }
        public void undo()
        {
            // Remove approver from the project
        }
    }
}
