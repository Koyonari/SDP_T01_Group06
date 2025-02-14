using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06.Composite;
using SDP_T01_Group06.Observer;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Memento
{
    public class DocumentMemento
    {
        private string documentName;
        private DocumentSection rootSection;
        private DocumentSection currentSectionPath; // Store path instead of section reference
        private DocumentState currentState;
        private bool isedited;

        public string DocumentName => documentName;
        public DocumentSection RootSectionClone => (DocumentSection)rootSection.Clone();
        //public DocumentSection CurrentSectionClone
        //{
        //    get
        //    {
        //        var root = RootSectionClone;
        //        return FindSectionByPath(root, currentSectionPath);
        //    }
        //}
        public DocumentSection CurrentSectionClone => (DocumentSection)currentSectionPath.Clone();
        public DocumentState CurrentState => currentState;
        public bool IsEdited => isedited;

        public DocumentMemento(string documentName, DocumentSection rootSection, DocumentSection currentSection, DocumentState state, bool isEdited)
        {
            this.documentName = documentName;
            this.currentState = state;
            this.rootSection = (DocumentSection)rootSection.Clone(); // Create a deep clone
            this.currentSectionPath = (DocumentSection)currentSection.Clone();
            this.isedited = isEdited;
        }

        //// Helper method to build path from root to current section
        //private string BuildSectionPath(DocumentSection root, DocumentSection target)
        //{
        //    if (target == null) return "";
        //    if (root == target) return root.SectionName;

        //    foreach (var child in root.children)
        //    {
        //        if (child is DocumentSection section)
        //        {
        //            string path = BuildSectionPath(section, target);
        //            if (path != null)
        //            {
        //                return root.SectionName + "/" + path;
        //            }
        //        }
        //    }
        //    return null;
        //}

        //// Helper method to find section by path
        //private DocumentSection FindSectionByPath(DocumentSection root, string path)
        //{
        //    if (string.IsNullOrEmpty(path)) return null;

        //    string[] parts = path.Split('/');
        //    DocumentSection current = root;

        //    for (int i = 1; i < parts.Length; i++) // Start from 1 since first part is root
        //    {
        //        bool found = false;
        //        foreach (var child in current.children)
        //        {
        //            if (child is DocumentSection section && section.SectionName == parts[i])
        //            {
        //                current = section;
        //                found = true;
        //                break;
        //            }
        //        }
        //        if (!found) return null;
        //    }
        //    return current;
        //}

        // part 2
        //private string documentName;
        //private DocumentSection rootSection;
        //private DocumentSection currentSection;
        //private DocumentState currentState;
        //private bool isedited;

        //public string DocumentName
        //{
        //    get { return documentName; }
        //}
        //public DocumentSection RootSectionClone
        //{
        //    get { return rootSection; }
        //}
        //public DocumentSection CurrentSectionClone
        //{
        //    get { return currentSection; }
        //}
        //public DocumentState CurrentState
        //{
        //    get { return currentState; }
        //}
        //public bool IsEdited
        //{
        //    get { return isedited; }
        //}

        //public DocumentMemento(string documentName, DocumentSection rootSectionClone, DocumentSection currentSectionClone, DocumentState state, bool isEdited)
        //{
        //    this.documentName = documentName;
        //    this.currentState = state;  // Assuming DocumentState is immutable or you have a way to clone it.
        //    // Create deep clones to capture the state.
        //    this.rootSection = rootSectionClone;
        //    this.currentSection = currentSectionClone;
        //    this.isedited = isEdited;
        //}




        // Part 1
        //public DocumentMemento SaveState()
        //{
        //    // Assume you have a deep-cloning method for your DocumentSection.
        //    DocumentSection clonedRootSection = document.Rootsection;
        //    // You may need to clone or capture the state in a way that makes sense for DocumentState.
        //    DocumentState clonedState = currentState.Clone(); // if a Clone() method exists, or create an equivalent.

        //    return new DocumentMemento(documentName, clonedState, clonedRootSection, isedited);
        //}

        //public void RestoreState(DocumentMemento memento)
        //{
        //    if (memento == null) return;

        //    // Restore the editable parts of the document.
        //    this.documentName = memento.DocumentName;
        //    this.currentState = memento.State;  // Again, assumes proper restoration logic.
        //    this.rootsection = memento.RootSectionClone;  // Restore the deep-cloned content.
        //    this.isedited = memento.IsEdited;

        //    // Optionally, reinitialize currentSection if it’s derived from the restored rootsection.
        //}

    }
}
