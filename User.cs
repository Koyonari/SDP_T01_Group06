
using SDP_T01_Group06.Factory;
using SDP_T01_Group06.Iterator;
using SDP_T01_Group06.Observer;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06
{
    public class User : IObserver
    {
        public string Name { get; set; }

        private Guid userID;
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private List<Document> documentList = new List<Document>();
        public List<Document> DocumentList
        {
            get { return documentList; }
            set { documentList = value; }
        }
        private List<Notification> notifications;
        private List<Document> observedDocuments;

        public User(string name)
        {
            this.userID = Guid.NewGuid();
            Name = name;
            DocumentList = new List<Document>();
            notifications = new List<Notification>();
            observedDocuments = new List<Document>();
        }

        public override string ToString()
        {
            return Name;
        }

        // IObserver implementation
        public void update(string documentName, DocumentState newState)
        {
            string stateChange = newState.GetType().Name.Replace("State", "");
            string message = $"Document '{documentName}' has changed to {stateChange} state";
            AddNotification(new Notification(message));
        }

        // Document observation methods
        public void ObserveDocument(Document document)
        {
            if (!observedDocuments.Contains(document))
            {
                observedDocuments.Add(document);
                document.registerObserver(this);
            }
        }

        public void StopObservingDocument(Document document)
        {
            if (observedDocuments.Contains(document))
            {
                observedDocuments.Remove(document);
                document.removeObserver(this);
            }
        }

        public Document CreateDocument(DocumentFactory factory)
        {
            Document newDoc = factory.CreateDocument(this);
            documentList.Add(newDoc);
            return newDoc;
        }

        public void AddDocument(Document document)
        {
            if (!documentList.Contains(document))
            {
                documentList.Add(document);
            }
        }

        public Document getRelatedDocument(int index)
        {
            DocumentIterator iterator = new AssociatedDocumentsIterator(this);

            for (int i = 0; i < index; i++)
            {
                if (!iterator.HasNext())
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "No document at the given index.");
                }
                iterator.Next();
            }

            if (!iterator.HasNext())
            {
                throw new ArgumentOutOfRangeException(nameof(index), "No document at the given index.");
            }

            return iterator.Next();
        }

        public Document getPendingDocument(int index)
        {
            DocumentIterator iterator = new PendingDocumentsIterator(this);

            for (int i = 0; i < index; i++)
            {
                if (!iterator.HasNext())
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "No pending document at the given position.");
                }
                iterator.Next();
            }

            if (!iterator.HasNext())
            {
                throw new ArgumentOutOfRangeException(nameof(index), "No pending document at the given position.");
            }

            return iterator.Next();
        }

        public void removeDocument(Document doc)
        {
            documentList.Remove(doc);
            StopObservingDocument(doc);
        }

        public int getNoOfOwnedDocuments()
        {
            DocumentIterator iterator = new OwnedDocumentsIterator(this);
            int count = 0;
            while (iterator.HasNext())
            {
                iterator.Next();
                count++;
            }
            return count;
        }

        public int getNoOfRelatedDocuments()
        {
            DocumentIterator iterator = new AssociatedDocumentsIterator(this);
            int count = 0;
            while (iterator.HasNext())
            {
                iterator.Next();
                count++;
            }
            return count;
        }

        public int getNoOfPendingDocuments()
        {
            DocumentIterator iterator = new PendingDocumentsIterator(this);
            int count = 0;
            while (iterator.HasNext())
            {
                iterator.Next();
                count++;
            }
            return count;
        }

        public void ListRelatedDocumentStatus()
        {
            DocumentIterator iterator = new AssociatedDocumentsIterator(this);
            Console.WriteLine($"\nDocuments associated with {Name}:");

            int index = 1;
            while (iterator.HasNext())
            {
                Document doc = iterator.Next();
                string stateName = doc.getCurrentState().GetType().Name;
                Console.WriteLine($"{index}. {doc.DocumentName} - {stateName}");
                index++;
            }
        }

        public void ListOwnedDocuments()
        {
            DocumentIterator iterator = createDocumentIterator("owned");
            Console.WriteLine($"\nDocuments Owned by {Name}:");
            int index = 1;
            while (iterator.HasNext())
            {
                Document doc = iterator.Next();
                Console.WriteLine($"{index}. {doc.DocumentName}");
                index++;
            }
        }

        public void ListRelatedDocuments()
        {
            DocumentIterator iterator = createDocumentIterator("associated");
            Console.WriteLine($"\nDocuments associated with {Name}:");
            int index = 1;
            while (iterator.HasNext())
            {
                Document doc = iterator.Next();
                Console.WriteLine($"{index}. {doc.DocumentName}");
                index++;
            }
        }

        public void ListPendingDocsForReview()
        {
            DocumentIterator iterator = new PendingDocumentsIterator(this);
            if (!iterator.HasNext())
            {
                Console.WriteLine($"\nYou have no documents pending for review.");
                return;
            }
            Console.WriteLine($"\nDocuments pending review for {Name}:");
            int index = 1;
            while (iterator.HasNext())
            {
                Document doc = iterator.Next();
                Console.WriteLine($"{index}. {doc.DocumentName}");
                index++;
            }
        }

        public DocumentIterator createDocumentIterator(string type)
        {
            switch (type.ToLower())
            {
                case "associated":
                    return new AssociatedDocumentsIterator(this);
                case "owned":
                    return new OwnedDocumentsIterator(this);
                case "pending":
                    return new PendingDocumentsIterator(this);
            }
            throw new ArgumentException("Invalid iterator type");
        }

        // Notification methods
        public void AddNotification(Notification notification)
        {
            if (!notifications.Contains(notification))
            {
                notifications.Add(notification);
            }
        }

        public List<Notification> GetNotifications(bool unreadOnly = false)
        {
            return unreadOnly
                ? notifications.Where(n => !n.IsRead).ToList()
                : notifications;
        }

        public void MarkAllNotificationsAsRead()
        {
            foreach (var notification in notifications)
            {
                notification.MarkAsRead();
            }
        }
    }
}