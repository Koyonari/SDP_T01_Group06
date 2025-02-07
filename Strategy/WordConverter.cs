namespace SDP_T01_Group06.Converter
{
    public class WordConverter: IDocumentConverter
    {
        public Document convert(Document document)
        {
            Document convertedDoc = document.clone();
            Console.WriteLine($"Converting {document.DocumentName} to Word format...");
            convertedDoc.DocumentName = document.DocumentName + ".word";
            return convertedDoc;
        }
    }
}
