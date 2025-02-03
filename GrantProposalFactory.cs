using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06
{
    public class GrantProposalFactory:DocumentFactory
    {
        public override Document CreateDocument(User user)
        {
            Document doc = new GrantProposal(user);
            doc.assembleDocument();
            return doc;
        }
    }
}
