namespace SDP_T01_Group06.Converter
{
    public class DocumentContext
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
