using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;
using System.Drawing.Text;

namespace WallpaperClock
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer(Properties.Resources.dialogue);
            player.Play();
            DialogResult message =  MessageBox.Show("Go to bed?", "Morgana", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (message == DialogResult.OK)
            {
                MessageBox.Show("Have you saved your work(s), yet?", "Morgana", MessageBoxButtons.YesNo, MessageBoxIcon.Question);         
                if (message == DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    Process.Start("shutdown", "/s /t 1");
                }
            }
            else if (message == DialogResult.Cancel)
            {
                this.Close();              
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Hide();
            pictureBox4.Show();
            SoundPlayer player = new SoundPlayer(Properties.Resources.dialogue);
            player.Play();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.Hide();
            pictureBox5.Show();
            SoundPlayer player = new SoundPlayer(Properties.Resources.dialogue);
            player.Play();
        }
    }
}
