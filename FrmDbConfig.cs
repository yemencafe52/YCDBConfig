using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YCDbConfig
{
    public partial class FrmDbConfig : Form
    {
        public FrmDbConfig()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {
            bool res = false;
            Display();
            return res;
        }

        private void Display()
        {
            comboBox2.SelectedIndex = 0;

            YcDbConfg yc;
            if(YcDbConfg.Read(out yc))
            {   
                textBox3.Text = yc.IP;
                textBox1.Text = yc.GetUserNamme;
                textBox2.Text = yc.GetPassword;
                textBox5.Text = yc.GetDbName;
                comboBox2.SelectedIndex = 1;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string msg;
            if(!Save(out msg))
            {
                MessageBox.Show(msg);
            }
        }

        private bool Save(out string msg)
        {
            bool res = false;
            msg = "";

            if (comboBox2.SelectedIndex == 0)
            {
                YcDbConfg.Delete();
                res = true;
                return res;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                msg = "ادخل عنوان السيرفر.";
                textBox3.Focus();
                return res;
            }

            if (string.IsNullOrEmpty(textBox5.Text))
            {
                msg = " ادخل مسار القاعدة .";
                textBox5.Focus();
                return res;
            }

            YcDbConfg dbc = new YcDbConfg(textBox3.Text, (UInt16)numericUpDown1.Value, textBox1.Text, textBox2.Text, textBox5.Text);

            if (!(YcDbConfg.Write(dbc)))
            {
                msg = "تعذر تنفيذ العملية  .";
                return res;
            }

            res = true;
            return res;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string msg;
            if (Save(out msg))
            {
                string connecting_sting;
                YcDbConfg yc;

                YcDbConfg.Read(out yc);
                connecting_sting = YcDbConfg.BuildConnectionString();

                if (Utilities.CheckServerSec(connecting_sting))
                {
                    MessageBox.Show("تم الإتصال بنجاح.", "يمن كافي", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.RtlReading);
                }
                else
                {
                    MessageBox.Show("تعذر الإتصال.", "يمن كافي", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.RtlReading);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult r = openFileDialog.ShowDialog();

            if(r == DialogResult.OK)
            {
                textBox5.Text = openFileDialog.FileName;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 0)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox5.Text = "";
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
            }
        }
    }
}
