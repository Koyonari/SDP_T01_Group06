using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal class EditCommand : ICommand
    {
        private Document document;
        //private User user;

        public EditCommand(Document document)
        {
            this.document = document;
            //this.user = user;
        }

        public void execute()
        {
            document.edit();
        }

        public void undo()
        {
            //document.RevertEdit();
        }
        public bool isUndoable()
        {
            return false;
        }
    }
}
