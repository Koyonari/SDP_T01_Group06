using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06
{
    public class AssociatedDocumentsIterator: DocumentIterator
    {
        private List<Document> documents;
        private User user;
        private int index = 0;

        public AssociatedDocumentsIterator(List<Document> documents, User user)
        {
            this.documents = documents;
            this.user = user;
        }

        public bool HasNext()
        {
            while (index < documents.Count)
            {
                if (documents[index].Collaborators.Contains(user) || documents[index].Owner == user)
                    return true;
                index++;
            }
            return false;
        }

        public Document Next()
        {
            if (!HasNext()) throw new InvalidOperationException("No more documents.");
            return documents[index++];
        }

    }
}
