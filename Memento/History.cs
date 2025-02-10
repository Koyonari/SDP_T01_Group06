using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Memento
{
    internal class History
    {
        private List<DocumentMemento> mementos;

        public History()
        {
            this.mementos = new List<DocumentMemento>();
        }

        public void addMemento(DocumentMemento memento)
        {
            this.mementos.Add(memento);
        }

        public DocumentMemento getMemento(int index)
        {
            return this.mementos[index];
        }
    }
}
