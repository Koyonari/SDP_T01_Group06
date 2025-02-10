using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal class AddCollaboratorCommand : ICommand
    {
        private Document document;
        private User collaborator;

        public AddCollaboratorCommand(Document document, User collaborator)
        {
            this.document = document;
            this.collaborator = collaborator;
        }

        public void execute()
        {
            // Add collaborator to the project
            document.addCollaborator(collaborator);
        }

        public void undo()
        {
            // Remove collaborator from the project
        }
        public bool isUndoable()
        {
            return false;
        }
    }
}
