using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06
{
    public class User
    {
        public string Name { get; set; }

        public User( string name)
        {
            Name = name;
        }

        public override string ToString() {
            return Name;
        }
    }
}
