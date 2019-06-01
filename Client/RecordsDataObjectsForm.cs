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
        BindingList<RecordDataObject> bindingData;
        List<RecordDataObject> Data;

        Remoting.Client.ClientActivated cao;
        Remoting.Server.WellKnownSingleton wko;
        Remoting.Server.WellKnownSinglecall callwko;

        public RecordsDataObjectsForm()
        {
            InitializeComponent();

            try
            {
                RemotingConfiguration.Configure("Client.exe.config", false);
                wko = new Remoting.Server.WellKnownSingleton();
                callwko = new Remoting.Server.WellKnownSinglecall();

                rdoView.Rows.Clear();
                Data = new List<RecordDataObject>();
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update()
        {
            bindingData = new BindingList<RecordDataObject>(Data);
            rdoView.DataSource = new BindingSource(bindingData, null);
            rdoView.Update();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cao = cao ?? new Remoting.Client.ClientActivated();
                int id = wko.Count + cao.ChangeTransaction.Length + 1;
                RecordDataEditor f = new RecordDataEditor(new RecordDataObject(id, string.Empty, DateTime.Now));
                f.ShowDialog();
                Data.Add(f.o);
                Update();
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
                cao = cao ?? new Remoting.Client.ClientActivated();
                RecordDataObject r = rdoView.SelectedRows[0].DataBoundItem as RecordDataObject;
                RecordDataObject old = new RecordDataObject(r);
                RecordDataEditor f = new RecordDataEditor(r);
                f.ShowDialog();
                Data.Where(w => w.id == f.o.id).Select(s => s = f.o).ToList();
                Update();
                cao.UpdateRecord(old, f.o);
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
                cao = cao ?? new Remoting.Client.ClientActivated();
                RecordDataObject r = rdoView.SelectedRows[0].DataBoundItem as RecordDataObject;
                Data.Remove(r);
                Update();
                cao.DeleteRecord(r);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void downloadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cao = cao ?? new Remoting.Client.ClientActivated();
                //Data = new List<RecordDataObject>(callwko.GetData());
                Data = new List<RecordDataObject>(wko.GetPersistentData());
                Update();
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
                List<RecordsDataChangeTransaction> lst = cao.ChangeTransaction.ToList();
                Data = new List<RecordDataObject>(
                    lst.Select(s => s.New ?? s.Old).ToList()
                    );
                Update();
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
                callwko.Commit(cao);

                //Data = new List<RecordDataObject>(callwko.GetData());
                Data = new List<RecordDataObject>(wko.GetPersistentData());
                Update();
                cao.Clear();
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
                callwko.Rollback(cao);
                Data = new List<RecordDataObject>();
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
