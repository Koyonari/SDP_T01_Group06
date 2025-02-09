using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Observer
{
    public interface ISubject
    {
        public void registerObserver(IObserver observer);
        public void removeObserver(IObserver observer);
        public void notifyObservers();
    }
}
