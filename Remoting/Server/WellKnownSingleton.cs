using System;
using System.Collections.Generic;
using System.Linq;

namespace Remoting.Server
{
    public class WellKnownSingleton : MarshalByRefObject
    {
        public int id { get; private set; }
        private List<RecordDataObject> RecordsData { get; set; }

        public WellKnownSingleton()
        {
            RecordsData = new List<RecordDataObject>();

        }

        public List<RecordDataObject> GetPersistentData()
        {
            return RecordsData;
        }

        public void Create(RecordDataObject o)
        {
            RecordsData.Add(o);
        }

        public void Update(RecordDataObject o)
        {
            RecordsData.Where(w => w.id == o.id).Select(s => s = o ).ToList();
        }

        public void Delete(RecordDataObject o)
        {
            RecordsData = RecordsData.Where(w => w.id != o.id).ToList();
        }
    }
}
