namespace SDP_T01_Group06.Converter
{
    public class WordConverter: IDocumentConverter
    {
        public Document convert(Document document)
        {
            Document convertedDoc = document.clone();
            Console.WriteLine($"Converting {document.Documentname} to Word format...");
            convertedDoc.Documentname = document.Documentname + ".word";
            return convertedDoc;
        }
    }
}
