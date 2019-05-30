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

        Remoting.Client.ClientActivated cao;
        Remoting.Server.WellKnownSingleton wko;
        Remoting.Server.WellKnownSinglecall callwko;

        public RecordsDataObjectsForm()
        {
            InitializeComponent();

            try
            {
                RemotingConfiguration.Configure("Client.exe.config", false);
                //wko = new Remoting.Server.WellKnownSingleton();
                callwko = new Remoting.Server.WellKnownSinglecall();

                rdoView.Rows.Clear();
                bindingData = new BindingList<RecordDataObject>();
                rdoView.DataSource = new BindingSource(bindingData, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cao = cao ?? new Remoting.Client.ClientActivated();
                int id = callwko.NextRecordID + cao.ChangeTransaction.Length;
                RecordDataEditor f = new RecordDataEditor(new RecordDataObject(id, string.Empty, DateTime.Now));
                f.ShowDialog();
                bindingData.Add(f.o);
                rdoView.Update();
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
                RecordDataEditor f = new RecordDataEditor(r);
                f.ShowDialog();
                rdoView.Update();
                cao.UpdateRecord(r, f.o);
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
                bindingData.Remove(r);
                rdoView.DataSource = new BindingSource(bindingData, null);
                rdoView.Update();
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
                bindingData = new BindingList<RecordDataObject>(callwko.GetData());

                rdoView.DataSource = new BindingSource(bindingData, null);
                rdoView.Update();
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
                bindingData = new BindingList<RecordDataObject>(
                    lst.Select(s => s.New ?? s.Old).ToList()
                    );
                rdoView.DataSource = new BindingSource(bindingData, null);
                rdoView.Update();
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

                bindingData = new BindingList<RecordDataObject>(callwko.GetData());
                rdoView.DataSource = new BindingSource(bindingData, null);
                rdoView.Update();
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
                bindingData = new BindingList<RecordDataObject>();
                rdoView.DataSource = new BindingSource(bindingData, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
