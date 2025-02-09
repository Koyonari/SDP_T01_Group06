using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Factory;

namespace SDP_T01_Group06.Command
{
    internal class CreateCommand : IResultCommand
    {
        private DocumentFactory documentFactory;
        private User user;
        private Document newDocument;
        private List<Document> documents;

        public CreateCommand(DocumentFactory documentFactory, User user, List<Document> documents)
        {
            this.documentFactory = documentFactory;
            this.user = user;
            this.documents = documents;
        }
        public void execute()
        {
            newDocument = user.CreateDocument(documentFactory);
        }
        public void undo()
        {
            // Delete the document  
            documents.Remove(newDocument);
            user.DocumentList.Remove(newDocument);
        }
        public bool isUndoable()
        {
            return true;
        }
        public Document getResult()
        {
            return newDocument;
        }
    }
}
