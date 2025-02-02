using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06
{
    public class DocumentSection : DocumentComponent
    {
        public List<DocumentComponent> children;
        private string sectionName;
        private bool isEditable;

        public string SectionName { get { return sectionName; } set { sectionName = value; } }
        public DocumentSection(string name, bool isEditable = true) // editable by default unless otherwise specified
        {
            this.sectionName = name;
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
    }
}
