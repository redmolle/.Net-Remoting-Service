using System;
using System.Collections.Generic;
using System.Linq;
using Remoting.Client;

namespace Remoting.Server
{
    public class WellKnownSinglecall : MarshalByRefObject
    {
        public WellKnownSinglecall()
        {

        }

        public List<KeyValuePair<int, string>> Commit(ClientActivated cao)
        {
            List<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();

            WellKnownSingleton wko = (WellKnownSingleton)Activator.GetObject(typeof(WellKnownSingleton), "ipc://ServIPC/WellKnownSingleton.soap");

            foreach(var v in cao.RecordsDataChangeTransaction)
            {
                try
                {
                    switch (v.state)
                    {
                        case StateDict.Create:
                            wko.Create(v.Data);
                            break;

                        case StateDict.Update:
                            wko.Update(v.Data);
                            break;

                        case StateDict.Delete:
                            wko.Delete(v.Data);
                            break;
                    }
                }
                catch(Exception ex)
                {
                    result.Add(
                        new KeyValuePair<int, string>(v.id, $"Не удалось {StateDict.Names.FirstOrDefault(w => w.Key == v.id)} запись")
                    );
                }
            }

            return result;
        }

        public void Rollback(ClientActivated cao)
        {
            cao.Clear();
        }
    }
}
