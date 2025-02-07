using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Iterator
{
    public class PendingDocumentsIterator : DocumentIterator
    {
        private User user;
        private List<Document> documents;
        private int index;

        public PendingDocumentsIterator(User user)
        {
            this.user = user;
            this.documents = user.DocumentList;
            index = 0;
        }

        public bool HasNext()
        {
            while (index < documents.Count)
            {
                if (documents[index].Approver == user)
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
