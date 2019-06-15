using System;
using System.Collections.Generic;
using System.Linq;

namespace Remoting.Server {
    public class WellKnownSingleton : MarshalByRefObject {
        public int Count { get { return RecordsData?.Count ?? 0; } }
        private List<RecordDataObject> RecordsData { get; set; }

        public WellKnownSingleton() {
            RecordsData = new List<RecordDataObject>();
            RecordsData.Add(new RecordDataObject(1, "1", DateTime.Now));
            RecordsData.Add(new RecordDataObject(2, "2", DateTime.Now));
            RecordsData.Add(new RecordDataObject(3, "3", DateTime.Now));
            RecordsData.Add(new RecordDataObject(4, "4", DateTime.Now));
            RecordsData.Add(new RecordDataObject(5, "5", DateTime.Now));
            RecordsData.Add(new RecordDataObject(6, "6", DateTime.Now));
            Console.WriteLine("Remoting.Server.WellKnownSingleton.WellKnownSingleton()");
        }

        public RecordDataObject[] GetPersistentData() {
            return RecordsData.ToArray();
        }

        public void Create(RecordDataObject o) {
            RecordsData.Add(o);
            Console.WriteLine("Remoting.Server.WellKnownSingleton.Create(RecordDataObject o)");
        }

        public void Update(RecordDataObject o, RecordDataObject n) {
            RecordsData =
                RecordsData.Select(s => s.id == o.id && s.StringField == o.StringField && s.DateField == o.DateField ? n : s).ToList();
            Console.WriteLine("Remoting.Server.WellKnownSingleton.Update(RecordDataObject o, RecordDataObject n)");
        }

        public void Delete(RecordDataObject o) {
            RecordsData =
                RecordsData.Where(w => w.id != o.id || w.StringField != o.StringField || w.DateField != o.DateField).ToList();
            //RecordsData = RecordsData.Where(w => w.id != o.id).ToList();
            //  Console.WriteLine(Remoting.Server.WellKnownSingleton.Delete());
            Console.WriteLine("Remoting.Server.WellKnownSingleton.Delete(RecordDataObject o)");
        }
    }
}
