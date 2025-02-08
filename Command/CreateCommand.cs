using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Factory;

namespace SDP_T01_Group06.Command
{
    internal class CreateCommand : ICommand, IResultCommand
    {
        private DocumentFactory documentFactory;
        private User user;
        private Document newDocument;
        public CreateCommand(DocumentFactory documentFactory, User user)
        {
            this.documentFactory = documentFactory;
            this.user = user;
        }
        public void execute()
        {
            // Create the document
            newDocument = user.CreateDocument(documentFactory);
        }
        public void undo()
        {
            // Delete the document
            
        }
        public Document getResult()
        {
            return newDocument;
        }
    }
}
