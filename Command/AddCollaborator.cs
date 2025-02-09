using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal class AddCollaborator : ICommand
    {
        private User collaborator;

        public AddCollaborator(User collaborator)
        {
            this.collaborator = collaborator;
        }

        public void execute()
        {
            // Add collaborator to the project
        }

        public void undo()
        {
            // Remove collaborator from the project
        }
    }
}
