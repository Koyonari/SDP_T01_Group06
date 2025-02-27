﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    internal class ViewCommand : ICommand
    {
        private User user;
        private string documentType;
        public ViewCommand(User user, string documentType)
        {
            this.user = user;
            this.documentType = documentType;
        }
        public void execute()
        {
            // View the document
            if (documentType == "Owned")
            {
                user.ListOwnedDocuments();
            }
            else if (documentType == "Associated")
            {
                user.ListRelatedDocuments();
            }
            else if (documentType == "Pending")
            {
                user.ListPendingDocsForReview();
            }
            else if (documentType == "Status")
            {
                Console.WriteLine("4");
                user.ListRelatedDocumentStatus();
            }
        }
        public void undo()
        {
            // Close the document
        }
        public bool isUndoable()
        {
            return false;
        }
    }
}
