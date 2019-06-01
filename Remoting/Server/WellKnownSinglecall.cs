using System;
using System.Collections.Generic;
using Remoting.Client;

namespace Remoting.Server
{
    public class WellKnownSinglecall : MarshalByRefObject
    {
        public WellKnownSinglecall() { }

        public void Commit(ClientActivated cao)
        {
            //Remoting.Server.WellKnownSingleton wko = new Remoting.Server.WellKnownSingleton();

            WellKnownSingleton wko = 
                (WellKnownSingleton)Activator.GetObject(
                    typeof(WellKnownSingleton), 
                    "http://localhost:13000/MyURITON.soap"
                    );
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
