﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SDP_T01_Group06;
using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Command
{
    public class ApproveCommand : Command
    {
        public Document document;
        //public DocumentState previousState;

        public ApproveCommand(Document document)
        {
            this.document = document;
        }

        public void execute()
        {
            DocumentState currentState = document.getCurrentState();
            currentState.approve();
            Console.WriteLine("Document approved.");
        }

        public void undo()
        {
            Console.WriteLine("Approved document cannot be changed.");
        }
    }
}
