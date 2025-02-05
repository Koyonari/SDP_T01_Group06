using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    public class ConvertToWordCommand : ConversionCommand
    {
        public ConvertToWordCommand(Document document)
        : base(document)
        {
        }

        public override void execute()
        {
            //document.ConvertTo("Word");
        }

        public override void undo()
        {
            //document.RevertConversion();
        }
    }
}
