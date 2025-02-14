using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Composite
{
    public class DocumentItem : DocumentComponent // leaf in Composite pattern
    {
        private string content;
        private string elementType;
        private bool isEditable;

        public DocumentItem(string content, string elementType, bool isEditable = true)
        {
            this.content = content;
            this.elementType = elementType;
            this.isEditable = isEditable;
        }

        public override string Content
        {
            get { return content; }
        }

        public override bool IsEditable
        {
            get { return isEditable; }
        }

        public override void display()
        {
            Console.WriteLine($"{elementType}: {content}");
        }

        // Memento
        public override DocumentComponent Clone()
        {
            return new DocumentItem(this.content, this.elementType, this.isEditable);
        }
    }
}
