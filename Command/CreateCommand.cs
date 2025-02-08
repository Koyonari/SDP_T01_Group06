using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal class CreateCommand : Command
    {
        private Document document;
        public CreateCommand(Document document)
        {
            this.document = document;
        }
        public void execute()
        {
            // Create the document
        }
        public void undo()
        {
            // Delete the document
        }
    }
}
