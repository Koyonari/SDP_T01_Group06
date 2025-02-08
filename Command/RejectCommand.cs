using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Command
{
    public class RejectCommand : ICommand
    {
        Document document;
        public RejectCommand(Document document)
        {
            this.document = document;
        }

        public void execute()
        {
            //DocumentState currentState = document.getCurrentState();
            //currentState.reject();
            document.reject();
            Console.WriteLine("Document Rejected.");
        }

        public void undo()
        {
            Console.WriteLine("Rejected document cannot be changed.");
        }
    }
}
