using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Observer
{
    public class ConcreteObserver : IObserver
    {
        public string Name { get; private set; }
        private List<ConcreteSubject> associatedDocuments;

        public ConcreteObserver(string name)
        {
            Name = name;
            associatedDocuments = new List<ConcreteSubject>();
        }

        public void update(string documentName, DocumentState newState)
        {
            Console.WriteLine($"{documentName} has been {newState.GetType().Name}.");
        }

        public void AddDocument(ConcreteSubject document)
        {
            if (!associatedDocuments.Contains(document))
            {
                associatedDocuments.Add(document);
                document.registerObserver(this);
            }
        }

        public void RemoveDocument(ConcreteSubject document)
        {
            if (associatedDocuments.Contains(document))
            {
                associatedDocuments.Remove(document);
                document.removeObserver(this);
            }
        }
    }
}
