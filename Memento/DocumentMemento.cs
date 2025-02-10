using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Composite;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Memento
{
    internal class DocumentMemento
    {
        private string documentName;
        private DocumentState currentState;
        private DocumentSection rootsection;
        private bool isedited;

        public string DocumentName { get; }
        public DocumentState State { get; }
        public DocumentSection RootSectionClone { get; }
        public bool IsEdited { get; }

        public DocumentMemento(string documentName, DocumentState state, DocumentSection rootSectionClone, bool isEdited)
        {
            DocumentName = documentName;
            State = state;  // Assuming DocumentState is immutable or you have a way to clone it.
            RootSectionClone = rootSectionClone;
            IsEdited = isEdited;
        }

        public DocumentMemento SaveState()
        {
            // Assume you have a deep-cloning method for your DocumentSection.
            DocumentSection clonedRootSection = document.Rootsection;
            // You may need to clone or capture the state in a way that makes sense for DocumentState.
            DocumentState clonedState = currentState.Clone(); // if a Clone() method exists, or create an equivalent.

            return new DocumentMemento(documentName, clonedState, clonedRootSection, isedited);
        }

        public void RestoreState(DocumentMemento memento)
        {
            if (memento == null) return;

            // Restore the editable parts of the document.
            this.documentName = memento.DocumentName;
            this.currentState = memento.State;  // Again, assumes proper restoration logic.
            this.rootsection = memento.RootSectionClone;  // Restore the deep-cloned content.
            this.isedited = memento.IsEdited;

            // Optionally, reinitialize currentSection if it’s derived from the restored rootsection.
        }

    }
}
