﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06.Iterator
{
    public interface DocumentAggregate
    {
        DocumentIterator createDocumentIterator(string type);
    }
}
