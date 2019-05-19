using System;
using System.Collections.Generic;
using System.Linq;

namespace Remoting.Server
{
    public class WellKnownSingleton : MarshalByRefObject
    {
        public int Count { get; set; }
        private List<RecordDataObject> RecordsData { get; set; }

        public WellKnownSingleton()
        {
            RecordsData = new List<RecordDataObject>();
            Count = RecordsData.Count;
        }

        public List<RecordDataObject> GetPersistentData()
        {
            return RecordsData;
        }

        public void Create(RecordDataObject o)
        {
            RecordsData.Add(o);
            Count = RecordsData.Count;

        }

        public void Update(RecordDataObject o, RecordDataObject n)
        {
            RecordDataObject r = RecordsData.First(f => f.id == o.id);
            if (r.id != o.id || 
                r.StringField != o.StringField || 
                r.DateField != o.DateField ||

                o.id != n.id)
                throw new Exception();

            RecordsData.Where(w => w.id == o.id).Select(s => s = n ).ToList();
            Count = RecordsData.Count;

        }

        public void Delete(RecordDataObject o)
        {
            RecordsData = RecordsData.Where(w => w.id != o.id).ToList();
            Count = RecordsData.Count;

        }
    }
}
