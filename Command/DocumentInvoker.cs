using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    public class DocumentInvoker
    {

        private Command currentCommand;
        private Stack<Command> commandHistory = new Stack<Command>();

        public DocumentInvoker()
        {

        }
        // Sets the current command
        public void setCommand(Command command)
        {
            this.currentCommand = command;
        }

        // Executes the command and stores it in history
        public void executeCommand()
        {
            if (currentCommand != null)
            {
                currentCommand.execute();
                commandHistory.Push(currentCommand);
                // Clear redo stack when a new command is executed
                //redoStack.Clear();
            }
        }

        // Undo the last command
        public void undoCommand()
        {
            if (commandHistory.Count > 0)
            {
                Command commandToUndo = commandHistory.Pop();
                commandToUndo.undo();
                //redoStack.Push(commandToUndo);
            }
        }

    }
}
