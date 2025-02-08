using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    public class ConvertToWordCommand : Command
    {
        Document document;
        public ConvertToWordCommand(Document document)
        {
            this.document = document;
        }

        public void execute()
        {
            //document.ConvertTo("Word");
        }

        public void undo()
        {
            //document.RevertConversion();
        }
    }
}
