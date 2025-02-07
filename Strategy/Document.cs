using SDP_T01_Group06.Converter;

namespace SDP_T01_Group06.Strategy
{
    public class Document
    {
        private IDocumentConverter _converter;

        public void SetConverter(IDocumentConverter converter)
        {
            _converter = converter;
        }

        public void ConvertDocument()
        {
            _converter.convert();
        }
    }
}
