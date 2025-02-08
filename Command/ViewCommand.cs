using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal class ViewCommand : Command
    {
        private Document document;
        public ViewCommand(Document document)
        {
            this.document = document;
        }
        public void execute()
        {
            // View the document
        }
        public void undo()
        {
            // Close the document
        }
    }
}
