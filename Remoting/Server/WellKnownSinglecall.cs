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

        public List<KeyValuePair<int, bool>> Commit(ClientActivated cao)
        {
            List<KeyValuePair<int, bool>> result = new List<KeyValuePair<int, bool>>(); //id объекта, результат комита

            WellKnownSingleton wko = (WellKnownSingleton)Activator.GetObject(typeof(WellKnownSingleton), "MyURI.soap");

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
                    result.Add(
                        new KeyValuePair<int, bool>(v.id,true)
                    );
                }
                catch(Exception ex)
                {
                    result.Add(
                        new KeyValuePair<int, bool>(v.id,false)
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
