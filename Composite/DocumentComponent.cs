using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Composite
{
    public abstract class DocumentComponent // component in Composite pattern
    {
        public virtual void add(DocumentComponent component)
        {
            throw new NotSupportedException();
        }

        public virtual void remove(DocumentComponent component)
        {
            throw new NotSupportedException();
        }

        public virtual DocumentComponent getChild(int index)
        {
            throw new NotSupportedException();
        }

        public virtual void display()
        {
            throw new NotSupportedException();
        }

        // common properties for all document elements
        public virtual string Content
        {
            get { throw new NotSupportedException(); }
        }

        public virtual bool IsEditable
        {
            get { throw new NotSupportedException(); }
        }
    }
}
