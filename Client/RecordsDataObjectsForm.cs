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
        //List<RecordDataObject> data;
        BindingList<RecordDataObject> bindingData;
        //BindingSource source = new BindingSource();

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

                bindingData = new BindingList<RecordDataObject>();
                RecordsDataObjectsView.AutoGenerateColumns = true;
                RecordsDataObjectsView.DataSource = bindingData;
                RecordsDataObjectsView.Update();
                //data = new List<RecordDataObject>();
                //UpdateView();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void UpdateView()
        //{
        //    bindingData = new BindingList<RecordDataObject>(data);
        //    source = new BindingSource(bindingData, null);
        //    RecordsDataObjectsView.AutoGenerateColumns = true;
        //    RecordsDataObjectsView.DataSource = source;
        //}

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cao = cao ?? new Remoting.Client.ClientActivated();
                RecordDataEditor f = new RecordDataEditor(new RecordDataObject(callwko.NextRecordID, string.Empty, DateTime.Now));
                f.ShowDialog();
                bindingData.Add(f.o);
                RecordsDataObjectsView.Update();
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
                RecordDataObject r = RecordsDataObjectsView.SelectedRows[0].DataBoundItem as RecordDataObject;
                RecordDataEditor f = new RecordDataEditor(r);
                f.ShowDialog();
                RecordsDataObjectsView.Update();
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
                RecordDataObject r = RecordsDataObjectsView.SelectedRows[0].DataBoundItem as RecordDataObject;
                bindingData.Remove(r);
                RecordsDataObjectsView.Update();
                cao.DeleteRecord(r);
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
                bindingData = new BindingList<RecordDataObject>(wko.GetPersistentData());
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
                List<RecordsDataChangeTransaction> lst = cao.ChangeTransaction;
                bindingData = new BindingList<RecordDataObject>();
                List<Color> colorLst = new List<Color>();
                foreach(var row in lst)
                {
                    Color c = Color.White;
                    if (row.Old == null)
                    {
                        c = Color.Green;
                        bindingData.Add(row.New);
                    }
                    else
                    {
                        if(row.New == null)
                        {
                            c = Color.OrangeRed;
                            bindingData.Add(row.Old);
                        }
                        else
                        {
                            c = Color.Yellow;
                            bindingData.Add(row.New);
                        }
                    }
                    colorLst.Add(c);
                }
                RecordsDataObjectsView.Update();
                for(int i = 0; i < colorLst.Count; i++)
                {
                    RecordsDataObjectsView.Rows[i].DefaultCellStyle.BackColor = colorLst.ElementAt(i);
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
                RecordsDataObjectsView.DefaultCellStyle.BackColor = Color.White;
                callwko.Commit(cao);
                cao.Clear();
                bindingData = new BindingList<RecordDataObject>(wko.GetPersistentData());
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
                callwko.Rollback(cao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
