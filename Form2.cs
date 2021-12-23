using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperClock
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        string currenttime;
        string notetime;
        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
        }       

        //run the clock
        private void timer1_Tick(object sender, EventArgs e)
        {
            currenttime = DateTime.Now.ToString("hh:mm:ss tt");
            label1.Text = currenttime;
        }
        //end

        //set the clock for reminder, will show message box
        private void timer2_Tick(object sender, EventArgs e)
        {
            notetime = maskedTextBox1.Text + " " + comboBox1.Text;
            label4.Text = "Reminder is set for: " + notetime;

            if (currenttime == notetime)
            {
                timer2.Stop();
                MessageBox.Show(textBox2.Text, "Reminder");
                button1.Enabled = true;
                button2.Enabled = false;
            }
        }
        //end

        //start
        private void button1_Click(object sender, EventArgs e)
        {
            timer2.Start();
            button2.Enabled = true;
            button1.Enabled = false;
        }
        //end

        //stop
        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            button1.Enabled = true;
            button2.Enabled = false;
            label4.Text = "";
        }
        //end

        //hide app into tray stuffs
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(1000); 
        }
        //end 
    }
}
