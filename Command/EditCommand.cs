using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal class EditCommand : Command
    {
        private Document document;
        private string content;


        public EditCommand(Document document, string content)
        {
            this.document = document;
            this.content = content;
        }

        public void execute()
        {
            //document.Edit(content);
        }

        public void undo()
        {
            //document.RevertEdit();
        }
    }
}
