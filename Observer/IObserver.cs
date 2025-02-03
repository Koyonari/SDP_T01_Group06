using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Observer
{
    public interface IObserver
    {
        void update(DocumentState state);
    }
}
