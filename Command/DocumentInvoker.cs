using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Memento;

namespace SDP_T01_Group06.Command
{
    public class DocumentInvoker
    {

        private ICommand currentCommand;
        private ICommand[] hotkeys;
        private Stack<ICommand> commandHistory = new Stack<ICommand>();
        //History history = new History();

        //public History History
        //{
        //    get { return history; }
        //}
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
                {
                    commandHistory.Push(currentCommand);
                }
            }
        }

        public void executeHotKey(int slot)
        {
            hotkeys[slot].execute();
        }

        // Undo the last command
        public void undoCommand()
        {
            if (commandHistory.Count > 0)
            {
                ICommand lastCommand = commandHistory.Pop();
                lastCommand.undo();
            }
            else
            {
                Console.WriteLine("No command to undo");
            }
        }

    }
}
