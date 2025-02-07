using SDP_T01_Group06.Converter;

namespace SDP_T01_Group06.Strategy
{
    public class DocumentConverter
    {
        private IDocumentConverter _strategy;

        public void SetStrategy(IDocumentConverter strategy)
        {
            _strategy = strategy;
        }

        public Document convert(Document document)
        {
            if (_strategy == null)
            {
                throw new InvalidOperationException("Conversion strategy not set");
            }
            return _strategy.convert(document);
        }
    }
}
