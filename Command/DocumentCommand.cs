using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    public abstract class DocumentCommand : Command
    {
        private User user;
        private List<Document> allDocuments;

        public User User
        {
            get { return user; }
            set { user = value; }
        }
        public List<Document> AllDocuments
        {
            get { return allDocuments; }
            set { allDocuments = value; }
        }

        //protected DocumentContext documentContext;

        public DocumentCommand(User user, List<Document> document)
        {
            this.user = user;
            this.allDocuments = document;
            //this.documentContext = documentContext;
        }

        // execute() and undo() to be implemented by concrete conversion commands
        public abstract void execute();
        public abstract void undo();
    }
}
