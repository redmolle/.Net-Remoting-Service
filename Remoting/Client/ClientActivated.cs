using System;
using System.Collections.Generic;

namespace Remoting.Client
{
    public class ClientActivated : MarshalByRefObject
    {
        private List<RecordsDataChangeTransaction> _ChangeTransaction { get; set; }
        public RecordsDataChangeTransaction[] ChangeTransaction { get { return _ChangeTransaction.ToArray(); } }


        public ClientActivated()
        {
            _ChangeTransaction = new List<RecordsDataChangeTransaction>();
        }

        public void CreateRecord(RecordDataObject o)
        {
            _ChangeTransaction.Add(new RecordsDataChangeTransaction
            {
                Old = null,
                New = o,
                ChangeDate = DateTime.Now
            });
        }
        public void UpdateRecord(RecordDataObject o, RecordDataObject n)
        {
            _ChangeTransaction.Add(new RecordsDataChangeTransaction
            {
                Old = o,
                New = n,
                ChangeDate = DateTime.Now
            });
        }
        public void DeleteRecord(RecordDataObject o)
        {
            _ChangeTransaction.Add(new RecordsDataChangeTransaction
            {
                Old = o,
                New = null,
                ChangeDate = DateTime.Now
            });
        }
        public void Clear()
        {
            _ChangeTransaction.Clear();
        }
    }
}
