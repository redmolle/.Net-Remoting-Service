using System;
using System.Collections.Generic;

namespace Remoting.Client
{
    public class ClientActivated : MarshalByRefObject
    {
        public List<RecordsDataChangeTransaction> ChangeTransaction { get; private set; }

        public ClientActivated()
        {
            ChangeTransaction = new List<RecordsDataChangeTransaction>();
        }

        public void CreateRecord(RecordDataObject o)
        {
            ChangeTransaction.Add(new RecordsDataChangeTransaction
            {
                Old = null,
                New = o,
                ChangeDate = DateTime.Now
            });
        }
        public void UpdateRecord(RecordDataObject o, RecordDataObject n)
        {
            ChangeTransaction.Add(new RecordsDataChangeTransaction
            {
                Old = o,
                New = n,
                ChangeDate = DateTime.Now
            });
        }
        public void DeleteRecord(RecordDataObject o)
        {
            ChangeTransaction.Add(new RecordsDataChangeTransaction
            {
                Old = o,
                New = null,
                ChangeDate = DateTime.Now
            });
        }
        public void Clear()
        {
            ChangeTransaction.Clear();
        }
    }
}
