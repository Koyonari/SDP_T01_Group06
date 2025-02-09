﻿using SDP_T01_Group06.States;

namespace SDP_T01_Group06.Observer
{
    public interface IObserver
    {
        public void update(string documentName, DocumentState newState);
    }
}
