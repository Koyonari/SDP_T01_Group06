using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Converter;
using SDP_T01_Group06.Strategy;

namespace SDP_T01_Group06.Command
{
    public class ConvertToWordCommand : IResultCommand
    {
        private User user;
        private Document document;
        private IDocumentConverter strategy;
        private DocumentConverter documentConverter;
        public ConvertToWordCommand(User user, Document document, IDocumentConverter strategy)
        {
            this.user = user;
            this.document = document;
            this.strategy = strategy;
            documentConverter = new DocumentConverter();
        }

        public void execute()
        {
            documentConverter.SetStrategy(strategy);
            document = documentConverter.convert(document);
        }

        public void undo() { }
        public Document getResult()
        {
            return document;
        }
    }
}
