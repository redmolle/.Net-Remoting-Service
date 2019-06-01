using System;
using System.Collections.Generic;
using System.Linq;

namespace Remoting.Server
{
    public class WellKnownSingleton : MarshalByRefObject
    {
        public int Count { get { return RecordsData?.Count ?? 0; } }
        private List<RecordDataObject> RecordsData { get; set; }

        public WellKnownSingleton()
        {
            RecordsData = new List<RecordDataObject>();
            RecordsData.Add(new RecordDataObject(1, "1", DateTime.Now));
            RecordsData.Add(new RecordDataObject(2, "2", DateTime.Now));
            RecordsData.Add(new RecordDataObject(3, "3", DateTime.Now));
            RecordsData.Add(new RecordDataObject(4, "4", DateTime.Now));
            RecordsData.Add(new RecordDataObject(5, "5", DateTime.Now));
            RecordsData.Add(new RecordDataObject(6, "6", DateTime.Now));
        }

        public RecordDataObject[] GetPersistentData()
        {
            return RecordsData.ToArray();
        }

        public void Create(RecordDataObject o)
        {
            RecordsData.Add(o);

        }

        public void Update(RecordDataObject o, RecordDataObject n)
        {
            RecordsData.Where(f => f.id == o.id && f.StringField == o.StringField && f.DateField == o.DateField).Select(s => s = n ).ToList();

        }

        public void Delete(RecordDataObject o)
        {
            RecordsData.Remove(o);
            //RecordsData = RecordsData.Where(w => w.id != o.id).ToList();

        }
    }
}
