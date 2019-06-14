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
            Console.WriteLine("Remoting.Client.ClientActivated.ClientActivated()");
        }

        public void CreateRecord(RecordDataObject o)
        {
            _ChangeTransaction.Add(new RecordsDataChangeTransaction
            {
                Old = null,
                New = o,
                ChangeDate = DateTime.Now
            });
            Console.WriteLine("Remoting.Client.ClientActivated.CreateRecord(RecordDataObject o)");
        }
        public void UpdateRecord(RecordDataObject o, RecordDataObject n)
        {
            _ChangeTransaction.Add(new RecordsDataChangeTransaction
            {
                Old = o,
                New = n,
                ChangeDate = DateTime.Now
            });
            Console.WriteLine("Remoting.Client.ClientActivated.UpdateRecord(RecordDataObject o, RecordDataObject n)");

        }
        public void DeleteRecord(RecordDataObject o)
        {
            _ChangeTransaction.Add(new RecordsDataChangeTransaction
            {
                Old = o,
                New = null,
                ChangeDate = DateTime.Now
            });
            Console.WriteLine("Remoting.Client.ClientActivated.DeleteRecord(RecordDataObject o)");
        }
        public void Clear()
        {
            _ChangeTransaction.Clear();
            Console.WriteLine("Remoting.Client.ClientActivated.Clear()");
        }
    }
}
