using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Observer
{
    public class Listener : IObserver
    {
        public string Name { get; private set; }
        private List<DocumentObservable> associatedDocuments;

        public Listener(string name)
        {
            Name = name;
            associatedDocuments = new List<DocumentObservable>();
        }

        public void update(string documentName, DocumentState newState)
        {
            Console.WriteLine($"{documentName} has been {newState.GetType().Name}.");
        }

        public void AddDocument(DocumentObservable document)
        {
            if (!associatedDocuments.Contains(document))
            {
                associatedDocuments.Add(document);
                document.registerObserver(this);
            }
        }

        public void RemoveDocument(DocumentObservable document)
        {
            if (associatedDocuments.Contains(document))
            {
                associatedDocuments.Remove(document);
                document.removeObserver(this);
            }
        }
    }
}
