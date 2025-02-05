using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Command
{
    public abstract class ConversionCommand : Command
    {
        private Document document;
        //protected DocumentContext documentContext;

        public Document Document
        {
            get { return document; }
            set { document = value; }
        }

        public ConversionCommand(Document document)
        {
            this.document = document;
            //this.documentContext = documentContext;
        }

        // execute() and undo() to be implemented by concrete conversion commands
        public abstract void execute();
        public abstract void undo();
    }
}
