using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Observer
{
    public class Listener : IObserver
    {
        private User user;
        private List<DocumentObservable> associatedDocuments;

        public string Name { get { return user.Name; } }

        public Listener(User user)
        {
            this.user = user;
            associatedDocuments = new List<DocumentObservable>();
        }

        public void update(string documentName, DocumentState newState)
        {
            // Create notification message based on new state
            string stateChange = newState.GetType().Name.Replace("State", "");
            string message = $"Document '{documentName}' has changed to {stateChange} state";

            // Add notification to user
            user.AddNotification(new Notification(message));
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