using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Factory;
using SDP_T01_Group06.Iterator;

namespace SDP_T01_Group06
{
    public class User
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

        public int getNoOfDocuments()
        {
            return documentList.Count;
        }

        public void AddDocument(Document doc)
        {
            documentList.Add(doc);
        }

        public void ListRelatedDocuments()
        {
            DocumentIterator iterator = new AssociatedDocumentsIterator(this);
            Console.WriteLine($"\nDocuments associated with {Name}:");
            while (iterator.HasNext())
            {
                Document doc = iterator.Next();
                Console.WriteLine($"- {doc.DocumentName}");
            }
        }

        public void ListOwnedDocuments()
        {
            DocumentIterator iterator = new OwnedDocumentsIterator(this);
            Console.WriteLine($"\nDocuments Owned by {Name}:");
            while (iterator.HasNext())
            {
                Document doc = iterator.Next();
                Console.WriteLine($"- {doc.DocumentName}");
            }
        }
    }
}
