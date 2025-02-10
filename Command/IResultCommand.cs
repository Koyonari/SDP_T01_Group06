using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal interface IResultCommand : ICommand
    {
        Document getResult();
    }
}
