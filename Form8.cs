using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WallpaperClock
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.FileName = " .txt";
            //saveFileDialog1.Title = "Save text Files";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = false;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //saveFileDialog1.FilterIndex = 2;
            //saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
                richTextBox1.Clear();
            }
            if (richTextBox1.Text.Trim().Length == 0)
            {
                button1.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.note = richTextBox1.Text;
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.note != string.Empty)
            {
                richTextBox1.Text = Properties.Settings.Default.note;
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            this.richTextBox1.AllowDrop = true;
            richTextBox1.Text = Properties.Settings.Default.note; //load remember note(s)          
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text != string.Empty)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }


        private void Form8_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
        }
    }

}
