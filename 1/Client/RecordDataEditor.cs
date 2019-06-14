using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class RecordDataEditor : Form
    {
        public RecordDataObject o { get; set; }


        public RecordDataEditor(RecordDataObject _o)
        {
            InitializeComponent();
            o = _o;

            StringField.Text = o.StringField ?? string.Empty;
            DateField.Value = o.DateField;
            RecordID.Text = o?.id.ToString();
        }

        private void RecordDataEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            o.StringField = StringField.Text;
            o.DateField = DateField.Value;
        }
    }
}
