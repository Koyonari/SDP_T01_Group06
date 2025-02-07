using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Observer
{
    public class ConcreteSubject : ISubject
    {
        public string DocumentName { get; private set; }
        public DocumentState CurrentState { get; private set; }
        private List<IObserver> observers;

        public ConcreteSubject(string documentName, DocumentState initialState)
        {
            DocumentName = documentName;
            CurrentState = initialState;
            observers = new List<IObserver>();
        }

        public void setState(DocumentState newState)
        {
            CurrentState = newState;
            notifyObservers();
        }

        public void registerObserver(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void removeObserver(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public void notifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.update(DocumentName, CurrentState);
            }
        }
    }
}