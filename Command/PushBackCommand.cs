using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Command
{
    public class PushBackCommand : Command
    {
        private Document document;
        private string comment;

        public PushBackCommand(Document document, string comment)
        {
            this.document = document;
            this.comment = comment;
        }

        public void execute()
        {
            DocumentState currentState = document.getCurrentState();
            currentState.pushBack(comment);
            Console.WriteLine("Document Pushed back.");
        }

        public void undo()
        {
            Console.WriteLine("Pushed back document cannot be changed.");
        }

    }
}
