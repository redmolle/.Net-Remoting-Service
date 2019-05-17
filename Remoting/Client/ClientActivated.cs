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
            RecordsDataChangeTransaction.Add(new RecordDataStateObject(o, 1));
        }
        public void UpdateRecord(RecordDataObject o)
        {
            RecordsDataChangeTransaction.Add(new RecordDataStateObject(o, 2));
        }
        public void DeleteRecord(RecordDataObject o)
        {
            RecordsDataChangeTransaction.Add(new RecordDataStateObject(o, 3));
        }
        public void Clear()
        {
            RecordsDataChangeTransaction.Clear();
        }
    }
}
