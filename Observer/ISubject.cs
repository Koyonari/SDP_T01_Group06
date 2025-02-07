using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Observer
{
    public interface ISubject
    {
        void registerObserver(IObserver observer);
        void removeObserver(IObserver observer);
        void notifyObservers();
    }
}
