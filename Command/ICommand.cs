using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    public interface ICommand
    {
        void execute();
        void undo();
        bool isUndoable();
    }
}
