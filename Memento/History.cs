using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Memento
{
    public class History
    {
        private Stack<DocumentMemento> mementos;    

        public History()
        {
            this.mementos = new Stack<DocumentMemento>();
        }

        public void AddMemento(DocumentMemento memento)
        {
            this.mementos.Push(memento);
        }

        // In History class:
        // Option A: Using a History Method That Returns the Memento
        public DocumentMemento Undo()
        {
            if (mementos.Count == 0)
            {
                //Console.WriteLine("No more mementos to undo.");
                return null;
            }
            // Checking 
            //DocumentMemento memento = mementos.Peek();
            //if (memento.RootSectionClone == null)
            //{
            //    Console.WriteLine("Legit no rootsection clone bruh.");
            //    return null;
            //}
            //memento.RootSectionClone.display();
            ////memento.CurrentSectionClone.display();
            return mementos.Pop();
        }
        public void Clear()
        {
            mementos.Clear();
        }

        // Option B: Using a History Method That Restores the Memento
        //public void Undo(Document document)
        //{
        //    if (this.mementos.Count == 0)
        //    {
        //        Console.WriteLine("No more mementos to undo.");
        //        return;
        //    }
        //    DocumentMemento memento = mementos.Pop();
        //    document.restore(memento);
        //}
    }
}
