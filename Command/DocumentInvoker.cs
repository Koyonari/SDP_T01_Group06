using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    public class DocumentInvoker
    {

        private ICommand currentCommand;
        private ICommand[] hotkeys;
        private Stack<ICommand> commandHistory = new Stack<ICommand>();

        public DocumentInvoker()
        {
            this.hotkeys = new ICommand[4];
        }

        // Sets the current command
        public void setCommand(ICommand command)
        {
            this.currentCommand = command;
        }

        public void setHotkeys(ICommand command, int slot)
        {
            hotkeys[slot] = command;
        }
        // Executes the command and stores it in history
        public void executeCommand()
        {
            if (currentCommand != null)
            {
                currentCommand.execute();
                if (currentCommand.isUndoable())
                    commandHistory.Push(currentCommand);
                // Clear redo stack when a new command is executed
                //redoStack.Clear();
            }
        }

        public void executeHotKey(int slot)
        {
            hotkeys[slot].execute();
        }

        // Undo the last command
        public void undoCommand()
        {
            Console.WriteLine("Undo method called");
            if (commandHistory.Count > 0)
            {
                ICommand commandToUndo = commandHistory.Pop();
                commandToUndo.undo();
                //redoStack.Push(commandToUndo);
            }
            else
            {
                Console.WriteLine("No command to undo");
            }
        }

    }
}
