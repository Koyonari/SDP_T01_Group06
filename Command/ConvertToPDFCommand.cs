using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal class ConvertToPDFCommand : ConversionCommand
    {

        public ConvertToPDFCommand(Document document)
             : base(document)
        {
        }
        public override void execute()
        {
            // Call the conversion operation in Document.
            //document.ConvertTo("PDF");


            // Conversion logic: convert document content to PDF format
            //document.convertTo("PDF");
            // You might store the previous format if needed for undo
        }

        public override void undo()
        {
            // Undo conversion – implementation may vary.
            //document.RevertConversion();


            // Undo conversion: this might revert to a default or previous format
            // Undo might be as simple as removing the generated PDF or setting the document format back
            //document.revertConversion();
        }
    }
}
