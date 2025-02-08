using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Converter;
using SDP_T01_Group06.Strategy;

namespace SDP_T01_Group06.Command
{
    internal class ConvertToPDFCommand : ICommand, IResultCommand
    {
        private User user;
        private Document document;
        private IDocumentConverter strategy;
        private DocumentConverter documentConverter;

        public ConvertToPDFCommand(User user, Document document, IDocumentConverter strategy)
        {
            this.document = document;
            this.user = user;
            this.strategy = strategy;
            documentConverter = new DocumentConverter();
        }
        public void execute()
        {
            documentConverter.SetStrategy(strategy);
            // Call the conversion operation in Document.
            //document.ConvertTo("PDF");


            // Conversion logic: convert document content to PDF format
            //document.convertTo("PDF");
            // You might store the previous format if needed for undo
        }

        public void undo()
        {
            // Undo conversion – implementation may vary.
            //document.RevertConversion();


            // Undo conversion: this might revert to a default or previous format
            // Undo might be as simple as removing the generated PDF or setting the document format back
            //document.revertConversion();
        }

        public Document getResult()
        {
            return document;
        }
    }
}
