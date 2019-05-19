using System;
using Remoting.Client;

namespace Remoting.Server
{
    public class WellKnownSinglecall : MarshalByRefObject
    {
        public int NextRecordID { get { return wko == null ? 0 : wko.Count + 1; } }

        private WellKnownSingleton wko;

        public WellKnownSinglecall() { }

        public void Commit(ClientActivated cao)
        {
            wko = (WellKnownSingleton)Activator.GetObject(typeof(WellKnownSingleton), "MyURI.soap");
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
