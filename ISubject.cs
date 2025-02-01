namespace SDP_T01_Group06
{
    public interface ISubject
    {
        void registerObserver(IObserver observer);
        void removeObserver(IObserver observer);
        void notifyObservers();
    }
}
