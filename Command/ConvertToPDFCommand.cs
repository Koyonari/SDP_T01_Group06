using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Converter;

namespace SDP_T01_Group06.Command
{
    internal class ConvertToPDFCommand : IResultCommand
    {
        private User user;
        private Document document;
        private IDocumentConverter strategy;

        public ConvertToPDFCommand(User user, Document document, IDocumentConverter strategy)
        {
            this.document = document;
            this.user = user;
            this.strategy = strategy;
        }
        public void execute()
        {
            document.setConversionStrategy(strategy);
            document = document.convert();
        }

        public void undo() { }

        public bool isUndoable()
        {
            return false;
        }
        public Document getResult()
        {
            return document;
        }
    }
}
