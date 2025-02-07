namespace SDP_T01_Group06.Converter
{
    public class PDFConverter : IDocumentConverter
    {
        public Document convert(Document document)
        {
            Document convertedDoc = document.clone();
            Console.WriteLine($"Converting {document.Documentname} to PDF format...");
            convertedDoc.Documentname = document.Documentname + ".pdf";
            return convertedDoc;
        }
    }
}
