using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Observer
{
    public class ConcreteSubject : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private DocumentState _state;

        public DocumentState State
        {
            get { return _state; }
            set
            {
                _state = value;
                notifyObservers();
            }
        }

        public void registerObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void removeObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void notifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.update(_state);
            }
        }
    }
}
