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

        public User( string name)
        {
            Name = name;
        }

        public override string ToString() {
            return Name;
        }

        public Document CreateDocument(DocumentFactory factory)
        {
            Document newDoc = factory.CreateDocument(this);
            return newDoc;
        }

        public void ListRelatedDocuments(List<Document> allDocuments)
        {
            DocumentIterator iterator = new AssociatedDocumentsIterator(allDocuments, this);
            Console.WriteLine($"\nDocuments associated with {Name}:");
            while (iterator.HasNext())
            {
                Document doc = iterator.Next();
                Console.WriteLine($"- {doc.Documentname}");
            }
        }

        public void ListOwnedDocuments(List<Document> allDocuments)
        {
            DocumentIterator iterator = new OwnedDocumentsIterator(allDocuments, this);
            Console.WriteLine($"\nDocuments Owned by {Name}:");
            while (iterator.HasNext())
            {
                Document doc = iterator.Next();
                Console.WriteLine($"- {doc.Documentname}");
            }
        }
    }
}
