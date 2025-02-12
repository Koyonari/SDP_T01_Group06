using SDP_T01_Group06.Converter;

namespace SDP_T01_Group06.Command
{
    public class ConvertToWordCommand : IResultCommand
    {
        private User user;
        private Document document;
        private IDocumentConverter strategy;
        public ConvertToWordCommand(User user, Document document, IDocumentConverter strategy)
        {
            this.user = user;
            this.document = document;
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
