using System;
using System.Collections.Generic;

namespace Remoting.Client
{
    public class ClientActivated : MarshalByRefObject
    {
        public List<RecordDataStateObject> RecordsDataChangeTransaction { get; private set; }

        public ClientActivated()
        {
            RecordsDataChangeTransaction = new List<RecordDataStateObject>();
        }

        public void CreateRecord(RecordDataObject o)
        {
            RecordsDataChangeTransaction.Add(new RecordDataStateObject(o, StateDict.Create));
        }
        public void UpdateRecord(RecordDataObject o)
        {
            RecordsDataChangeTransaction.Add(new RecordDataStateObject(o, StateDict.Update));
        }
        public void DeleteRecord(RecordDataObject o)
        {
            RecordsDataChangeTransaction.Add(new RecordDataStateObject(o, StateDict.Delete));
        }
        public void Clear()
        {
            RecordsDataChangeTransaction.Clear();
        }
    }
}
