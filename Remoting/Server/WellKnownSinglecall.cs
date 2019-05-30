using System;
using System.Collections.Generic;
using Remoting.Client;

namespace Remoting.Server
{
    public class WellKnownSinglecall : MarshalByRefObject
    {
        public int NextRecordID
        {
            get
            {
                wko = new WellKnownSingleton();
                return wko.Count;
            }
        }

        private WellKnownSingleton wko;

        public WellKnownSinglecall() { }

        public RecordDataObject[] GetData()
        {
            wko = new WellKnownSingleton();
            //wko = (WellKnownSingleton)Activator.GetObject(typeof(WellKnownSingleton), "http://localhost:13000/MyURI.soap");
            RecordDataObject[] r = wko.GetPersistentData().ToArray();
            return r;
        }

        public void Commit(ClientActivated cao)
        {
            wko = new WellKnownSingleton();
            //wko = (WellKnownSingleton)Activator.GetObject(typeof(WellKnownSingleton), "http://localhost:13000/MyURI.soap");
            foreach (var v in cao.ChangeTransaction)
            {
                if (v.Old == null)
                    wko.Create(v.New);
                else
                {
                    if (v.New == null)
                        wko.Delete(v.Old);
                    else
                        wko.Update(v.Old, v.New);
                }
            }
        }

        public void Rollback(ClientActivated cao)
        {
            cao.Clear();
        }
    }
}
