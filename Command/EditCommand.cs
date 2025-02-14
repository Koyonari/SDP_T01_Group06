using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Memento;

namespace SDP_T01_Group06.Command
{
    internal class EditCommand : ICommand
    {
        private Document document;
        private History history = new History(); // Reference to the shared History caretaker

        //private User user;

        public EditCommand(Document document)
        {
            this.document = document;
            //this.history = history;
            //this.user = user;
        }

        public void execute()
        {
            // Create a snapshot of the current document state
            DocumentMemento memento = document.save();

            // Save the snapshot into the History
            history.AddMemento(memento);

            document.edit();
        }

        public void undo()
        {
            // When undo is called for an EditCommand, retrieve the last snapshot
            DocumentMemento memento = history.Undo();
            if (memento != null)
            {
                document.restore(memento);
            }
            else
            {
                Console.WriteLine("No mementos to undo for the edit command.");
            }
            //document.RevertEdit();
        }
        public bool isUndoable()
        {
            return true;
        }
    }
}
