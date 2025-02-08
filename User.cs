using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Factory;
using SDP_T01_Group06.Iterator;

namespace SDP_T01_Group06
{
    public class User : DocumentAggregate
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

        public User(string name)
        {
            this.userID = Guid.NewGuid();
            Name = name;
            DocumentList = new List<Document>();
        }

        public override string ToString() {
            return Name;
        }

        public Document CreateDocument(DocumentFactory factory)
        {
            Document newDoc = factory.CreateDocument(this);
            documentList.Add(newDoc);
            return newDoc;
        }

        public void AddDocument(Document doc)
        {
            documentList.Add(doc);
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
                iterator.Next(); // Move to the next document
            }

            if (!iterator.HasNext())
            {
                throw new ArgumentOutOfRangeException(nameof(index), "No document at the given index.");
            }

            return iterator.Next(); // Return the document at the correct index
        }


        public Document getPendingDocument(int index)
        {
            DocumentIterator iterator = new PendingDocumentsIterator(this);

            for (int i = 0; i < index; i++) // Iterate to position-1
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
                string stateName = doc.getCurrentState().GetType().Name; // Get class name of the state
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
            DocumentIterator iterator = createDocumentIterator("pending");
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
                    return new AssociatedDocumentsIterator(this);
                case "pending":
                    return new AssociatedDocumentsIterator(this);
            }
            throw new ArgumentException("Invalid iterator type");
        }
    }
}
