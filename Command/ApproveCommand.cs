using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Command
{
    public class ApproveCommand : ICommand
    {
        public Document document;
        //public DocumentState previousState;

        public ApproveCommand(Document document)
        {
            this.document = document;
        }

        public void execute()
        {
            //Method 1
            //DocumentState currentState = document.getCurrentState();
            //currentState.approve();

            //Method 2 (I thnik is this)
            document.approve();
            Console.WriteLine("Document approved.");
        }

        public void undo()
        {
            Console.WriteLine("Approved document cannot be changed.");
        }
    }
}
