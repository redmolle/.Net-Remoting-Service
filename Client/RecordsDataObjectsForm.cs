using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Remoting;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace Client
{

    public partial class RecordsDataObjectsForm : Form
    {
        List<RecordDataObject> data;
        BindingList<RecordDataObject> bindingData;
        BindingSource source = new BindingSource();

        Remoting.Client.ClientActivated cao;
        Remoting.Server.WellKnownSingleton wko;
        Remoting.Server.WellKnownSinglecall callwko;

        //private void UpdateView(object sender, EventArgs e)
        //{
        //    RecordsDataObjectsView.Rows.Clear();
        //    RecordsDataObjectsView.DataSource = data;
        //    foreach(var r in RecordsDataObjectsView.DataSource)
        //    {
        //        RecordsDataObjectsView.Rows.Add
        //    }
        //}


        public RecordsDataObjectsForm()
        {
            InitializeComponent();

            try
            {
                RemotingConfiguration.Configure("Client.exe.config", false);

                wko = new Remoting.Server.WellKnownSingleton();
                callwko = new Remoting.Server.WellKnownSinglecall();


                data = new List<RecordDataObject>();
                UpdateView();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateView()
        {
            bindingData = new BindingList<RecordDataObject>(data);
            source = new BindingSource(bindingData, null);
            RecordsDataObjectsView.AutoGenerateColumns = true;
            RecordsDataObjectsView.DataSource = source;
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RecordDataEditor f = new RecordDataEditor(new RecordDataObject(wko.id + 1, string.Empty, DateTime.Now));
                f.ShowDialog();
                data.Add(f.o);
                //RecordsDataObjectsView.DataSource = data;
                UpdateView();
                cao.CreateRecord(f.o);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RecordDataObject r = RecordsDataObjectsView.SelectedRows[0].DataBoundItem as RecordDataObject;

                RecordDataEditor f = new RecordDataEditor(r);
                f.ShowDialog();

                RecordsDataObjectsView.Update();
                cao.UpdateRecord(f.o);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RecordDataObject r = RecordsDataObjectsView.SelectedRows[0].DataBoundItem as RecordDataObject;

                data.Remove(r);

                RecordsDataObjectsView.Update();
                cao.UpdateRecord(r);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void downloadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordsDataObjectsView.DefaultCellStyle.BackColor = Color.White;
            try
            {
                cao = cao ?? new Remoting.Client.ClientActivated();

                data = new List<RecordDataObject>(wko.GetPersistentData());
                RecordsDataObjectsView.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RecordsDataObjectsView.DefaultCellStyle.BackColor = Color.White;
                data.Clear();
                List<RecordDataObject> lst = cao.RecordsDataChangeTransaction.Select(s => s.Data).ToList();
                data = new List<RecordDataObject>(lst);
                RecordsDataObjectsView.Update();

                int i = 0;
                foreach (var r in cao.RecordsDataChangeTransaction)
                {
                    Color c = Color.Green;
                    switch (r.state)
                    {
                        case StateDict.Create:
                            break;

                        case StateDict.Update:
                            c = Color.Yellow;
                            break;

                        case StateDict.Delete:
                            c = Color.OrangeRed;
                            break;
                    }
                    RecordsDataObjectsView.Rows[i].DefaultCellStyle.BackColor = c;
                    i++;
                }
                RecordsDataObjectsView.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void commitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<KeyValuePair<int, bool>> lst = callwko.Commit(cao);


                cao.Clear();
                data = new List<RecordDataObject>(wko.GetPersistentData());
                RecordsDataObjectsView.Update();

                int i = 0;
                foreach(var d in data)
                {
                    Color c = lst.FirstOrDefault(f => f.Key == d.id).Value ?
                        Color.Green : Color.OrangeRed;
                    RecordsDataObjectsView.Rows[i].DefaultCellStyle.BackColor = c;
                    i++;
                }

                RecordsDataObjectsView.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rollbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RecordsDataObjectsView.DefaultCellStyle.BackColor = Color.White;
                cao.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
