using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Composite
{
    public class DocumentSection : DocumentComponent
    {
        public List<DocumentComponent> children;
        private string sectionName;
        private bool isEditable;

        public string SectionName { get { return sectionName; } set { sectionName = value; } }
        public DocumentSection(string name, bool isEditable = true) // editable by default unless otherwise specified
        {
            sectionName = name;
            this.isEditable = isEditable;
            children = new List<DocumentComponent>();
        }



        public override void add(DocumentComponent component)
        {
            children.Add(component);
        }

        public override void remove(DocumentComponent component)
        {
            children.Remove(component);
        }

        public override DocumentComponent getChild(int index)
        {
            return children[index];
        }

        public override bool IsEditable
        {
            get { return isEditable; }
        }

        public override void display()
        {
            Console.WriteLine($"Section: {sectionName}");
            foreach (var child in children)
            {
                child.display();
            }
        }

        // get combined content of all children
        public override string Content
        {
            get
            {
                return string.Join("\n", children.Select(c => c.Content));
            }
        }

        // Memento
        //public override DocumentComponent Clone()
        //{
        //    DocumentSection clone = new DocumentSection(this.sectionName, this.isEditable);
        //    foreach (var child in this.children)
        //    {
        //        clone.children.Add(child.Clone()); // Deep copy of each child
        //    }
        //    return clone;
        //}

        public override DocumentComponent Clone()
        {
            // Create a new instance for this section.
            DocumentSection clone = new DocumentSection(this.SectionName, this.IsEditable);

            // Deep clone each child.
            foreach (DocumentComponent child in this.children)
            {
                DocumentComponent childClone = child.Clone();
                clone.children.Add(childClone);
                //Console.WriteLine("Childcloned" + childClone.ToString());
            }
            return clone;
        }

    }
}
