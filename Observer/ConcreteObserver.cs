using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Observer
{
    public class ConcreteObserver : IObserver
    {
        private ISubject _subject;
        private DocumentState _state;

        public ConcreteObserver(ISubject subject)
        {
            _subject = subject;
            _subject.registerObserver(this);
        }

        public void update(DocumentState state)
        {
            _state = state;
            Console.WriteLine($"Observer updated. Document state: {_state}");
        }
    }
}
