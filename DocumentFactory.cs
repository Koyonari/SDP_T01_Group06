using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06
{
    public abstract class DocumentFactory
    {
        public abstract Document CreateDocument(User user);
    }
}
