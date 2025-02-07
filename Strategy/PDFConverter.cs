namespace SDP_T01_Group06.Converter
{
    public class PDFConverter : IDocumentConverter
    {
        public Document convert(Document document)
        {
            Document convertedDoc = document.clone();
            Console.WriteLine($"Converting {document.DocumentName} to PDF format...");
            convertedDoc.DocumentName = document.DocumentName + ".pdf";
            return convertedDoc;
        }
    }
}
