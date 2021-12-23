using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Net;
using System.Media;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using WallpaperClock.Properties;
using System.Threading;
using Microsoft.Win32;

namespace WallpaperClock
{
    public partial class Form1 : Form
    {
        Image reminder = Resources.reminder;
        Image note = Resources.note;
        Image help = Resources.help;
        Image about = Resources.about;
        Image refresh = Resources.refresh;     

            //Move borderless form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        //

        public Form1()
        {           
            InitializeComponent(); 
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 10; //0.01s, run the circular clock immediately
            t.Elapsed += Timer_Elapsed;
            t.Start();
            timer1.Start();
            timer2.Start();
            timer3.Start();
            timer4.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString();
            timer1.Start();
            timer2.Start();
            timer3.Start();
            timer4.Start();
            System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
            time.Interval = 1800000; // 30mins
            time.Tick += new EventHandler(ticked);
            time.Start(); //Morgana will be back every 30min 

            //thanksgiving   
            DateTime starttime = DateTime.Now;
            DateTime thanksgiving = new DateTime(DateTime.Now.Year, 11, 25, 0, 0, 0);
            if ((thanksgiving - starttime).TotalDays > 0) //if thanksgiving hasn't been reached
            {
                timer1.Start();
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
            }
            else if (Math.Abs((thanksgiving - starttime).TotalDays) < 1) //If currently the thanksgiving day
            {
                timer1.Stop();
                label7.Text = "Happy Thanksgiving!";
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                pictureBox9.Image = Properties.Resources.thanksgiving;
                pictureBox8.Visible = false;
            }
            else
            {
                timer1.Stop();
                label7.Text = "The fun is now over. Wait until next year!";
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                timer2.Start();
                timer3.Start();               
            }
            //end

            //christmas   
            DateTime starttime1 = DateTime.Now;
            DateTime christmas = new DateTime(DateTime.Now.Year, 12, 25, 0, 0, 0);
            if ((christmas - starttime1).TotalDays > 0)
            {
                timer2.Start();
                label14.Visible = true;
                label13.Visible = true;
                label12.Visible = true;
                label11.Visible = true;
            }
            else if (Math.Abs((starttime1 - christmas).TotalDays) < 1)
            {
                timer2.Stop();
                label14.Text = "Merry Christmas!";
                label13.Visible = false;
                label12.Visible = false;
                label11.Visible = false;
                SoundPlayer player = new SoundPlayer(Properties.Resources.christmas_wav);
                player.PlayLooping();
                pictureBox9.Image = Properties.Resources.merrychristmas;
                pictureBox8.Visible = false;
            }
            else
            {
                label14.Text = "The fun is over. Wait until next year!";
                label13.Visible = false;
                label12.Visible = false;
                label11.Visible = false;
                timer1.Stop();                
                timer2.Stop();
                timer3.Start();
            }
            //end 

            //new year
            DateTime starttime2 = DateTime.Now;
            DateTime newyear = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            if ((newyear - starttime2).TotalDays > 0)
            {
                timer3.Start();
            }            
            else if (Math.Abs((starttime2 - newyear).TotalDays) < 1)
            {
                timer3.Stop();
                label18.Text = "Happy New Year!";
                label17.Visible = false;
                label16.Visible = false;
                label15.Visible = false;
                SoundPlayer player = new SoundPlayer(Properties.Resources.newyear_wav);
                player.PlayLooping();
                pictureBox9.Image = Properties.Resources.happynewyear;
                pictureBox8.Visible = false;
            }
            else
            {
                //
            }
            //end

            //hours and greeting
            if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour < 12)
            {
                this.BackgroundImage = Properties.Resources.day;
                pictureBox8.Image = Properties.Resources.goodmor;
                circularProgressBar1.ForeColor = Color.Red;
                circularProgressBar1.SubscriptColor = Color.Red;
                circularProgressBar1.ProgressColor = Color.PapayaWhip;
                label3.ForeColor = Color.Red;
                label19.ForeColor = Color.Red;
                label24.ForeColor = Color.Red;
                label25.ForeColor = Color.Red;
                label26.ForeColor = Color.Red;
                label27.ForeColor = Color.Red;
                label28.ForeColor = Color.Red;
                label29.ForeColor = Color.Red;
                label30.ForeColor = Color.Red;
                label31.ForeColor = Color.Red;
                label32.ForeColor = Color.Red;
                label33.ForeColor = Color.Red;
                label34.ForeColor = Color.Red;
                label35.ForeColor = Color.Red;
            }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour <= 18)
            {
                this.BackgroundImage = Properties.Resources.afternoon;
                pictureBox8.Image = Properties.Resources.goodafter;
                circularProgressBar1.ForeColor = Color.Navy;
                circularProgressBar1.SubscriptColor = Color.Navy;
                circularProgressBar1.ProgressColor = Color.LightPink;
                circularProgressBar1.OuterColor = Color.Lime;
                label3.ForeColor = Color.Navy;
                label19.ForeColor = Color.Navy;
                label24.ForeColor = Color.Navy;
                label25.ForeColor = Color.Navy;
                label26.ForeColor = Color.Navy;
                label27.ForeColor = Color.Navy;
                label28.ForeColor = Color.Navy;
                label29.ForeColor = Color.Navy;
                label30.ForeColor = Color.Navy;
                label31.ForeColor = Color.Navy;
                label32.ForeColor = Color.Navy;
                label33.ForeColor = Color.Navy;
                label34.ForeColor = Color.Navy;
                label35.ForeColor = Color.Navy;
            }
            else if (DateTime.Now.Hour > 18 && DateTime.Now.Hour <= 22)
            {
                this.BackgroundImage = Properties.Resources.night;
                circularProgressBar1.ProgressColor = Color.LightYellow;
                pictureBox8.Image = Properties.Resources.goodeve;
                circularProgressBar1.ForeColor = Color.Azure;
                circularProgressBar1.SubscriptColor = Color.Azure;
                circularProgressBar1.ProgressColor = Color.Azure;
                label3.ForeColor = Color.Lime;
                label19.ForeColor = Color.Lime;
                label24.ForeColor = Color.Lime;
                label25.ForeColor = Color.Lime;
                label26.ForeColor = Color.Lime;
                label27.ForeColor = Color.Lime;
                label28.ForeColor = Color.Lime;
                label29.ForeColor = Color.Lime;
                label30.ForeColor = Color.Lime;
                label31.ForeColor = Color.Lime;
                label32.ForeColor = Color.Lime;
                label33.ForeColor = Color.Lime;
                label34.ForeColor = Color.Lime;
                label35.ForeColor = Color.Lime;                
            }
            else
            {
                pictureBox8.Image = Properties.Resources.bed1;
                this.BackgroundImage = Properties.Resources.bed;
                Form6 frm6 = new Form6();
                frm6.Show(); //Morgana will show up and tell you to go the fuck to sleep.
                frm6.TopMost = true; //bring form6 to front.   
                circularProgressBar1.ForeColor = Color.Azure;
                circularProgressBar1.SubscriptColor = Color.Azure;
                circularProgressBar1.ProgressColor = Color.Azure;
                label3.ForeColor = Color.Lime;
                label19.ForeColor = Color.Lime;
                label24.ForeColor = Color.Lime;
                label25.ForeColor = Color.Lime;
                label26.ForeColor = Color.Lime;
                label27.ForeColor = Color.Lime;
                label28.ForeColor = Color.Lime;
                label29.ForeColor = Color.Lime;
                label30.ForeColor = Color.Lime;
                label31.ForeColor = Color.Lime;
                label32.ForeColor = Color.Lime;
                label33.ForeColor = Color.Lime;
                label34.ForeColor = Color.Lime;
                label35.ForeColor = Color.Lime;
            }
              //end                                   
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            circularProgressBar1.Invoke((MethodInvoker)delegate
            {
                circularProgressBar1.Text = DateTime.Now.ToString("hh:mm:ss");
                circularProgressBar1.SubscriptText = DateTime.Now.ToString("tt");
            });
        }


        //-----------------------------------------Countdown area-------------------------------------------//
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            DateTime thanksgiving = new DateTime(DateTime.Now.Year, 11, 25);
            TimeSpan tgv = thanksgiving.Subtract(DateTime.Now);
            label7.Text = tgv.Days.ToString();
            label8.Text = tgv.Hours.ToString();
            label9.Text = tgv.Minutes.ToString();
            label10.Text = tgv.Seconds.ToString();            
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Start();
            DateTime christmas = new DateTime(DateTime.Now.Year, 12, 25);
            TimeSpan cm = christmas.Subtract(DateTime.Now);
            label14.Text = cm.Days.ToString();
            label13.Text = cm.Hours.ToString();
            label12.Text = cm.Minutes.ToString();
            label11.Text = cm.Seconds.ToString();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Start();
            DateTime newyear = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            TimeSpan ny = newyear.AddYears(1).Subtract(DateTime.Now);
            label18.Text = ny.Days.ToString();
            label17.Text = ny.Hours.ToString();
            label16.Text = ny.Minutes.ToString();
            label15.Text = ny.Seconds.ToString();
        }
        //-----------------------------------------------End--------------------------------------------//


        //----------------------------------------------Minimized area-----------------------------------------------//    
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            Application.Restart();
        }
        //----------------------------------------------------End----------------------------------------------------//


        //----------------------------------------------------Weather area--------------------------------------------//
        private void button2_Click(object sender, EventArgs e)
        {
            string city;
            city = richTextBox1.Text;           

            string uri = string.Format("http://api.weatherapi.com/v1/forecast.xml?key=1f9ee30b31fc4f97b6c33154200311&q={0}&days=1", city);
            XDocument doc = XDocument.Load(uri);

            string iconUri = (string)doc.Descendants("icon").FirstOrDefault();

            WebClient client = new WebClient();

            byte[] image = client.DownloadData("http:" + iconUri);

            MemoryStream stream = new MemoryStream(image);
            Bitmap newBitmap = new Bitmap(stream);

            string temp = (string)doc.Descendants("temp_f").FirstOrDefault();
            

            string maxTemp = (string)doc.Descendants("maxtemp_f").FirstOrDefault();
            string minTemp = (string)doc.Descendants("mintemp_f").FirstOrDefault();

            string windmph = (string)doc.Descendants("maxwind_mph").FirstOrDefault();

            string humidity = (string)doc.Descendants("avghumidity").FirstOrDefault();

            string country = (string)doc.Descendants("country").FirstOrDefault();

            string csrs = (string)doc.Descendants("text").FirstOrDefault(); //csrs: cloudy,sunny,rain,snow 

            Bitmap icon = newBitmap;

            label19.Text = temp + " °F";
            label31.Text = maxTemp + " °F";
            label32.Text = minTemp + " °F";
            label33.Text = windmph + " mph";
            label34.Text = humidity + " %";
            label27.Text = csrs;
            label35.Text = country;
            pictureBox1.Image = icon;
        }            
        //------------------------------------------------End---------------------------------------------------//


        //---------------------------------------------Morgana Role-------------------------------------------//
        private void timer4_Tick(object sender, EventArgs e) //making Morgana comes back every 30min 
        {
            System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
            time.Interval = 1800000; //30min
            time.Tick += new EventHandler(ticked);
            timer4.Start();
        }

        private void ticked(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show();
        }

        //--------------------------------------------PictureBox------------------------------------------//
        private void pictureBox6_Click(object sender, EventArgs e) //refresh
        {
            Application.Restart();
        }

        private void pictureBox2_Click(object sender, EventArgs e) //reminder
        {
            Form2 frm = new Form2();
            frm.Show();
            Form8 frm8 = new Form8();
            frm8.Hide();         
        }

        private void pictureBox3_Click(object sender, EventArgs e) //note
        {
            Form8 frm8 = new Form8();
            frm8.Show();
            Form2 frm2 = new Form2();
            frm2.Hide();
            
        }

        private void pictureBox4_Click(object sender, EventArgs e) //help
        {
            Form3 frm1 = new Form3();
            frm1.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e) //about
        {
            Form7 frm7 = new Form7();
            frm7.Show();
        }
        
        private void pictureBox2_MouseHover(object sender, EventArgs e) //reminder
        {
            int reminder_width = reminder.Width + ((reminder.Width * 100) / 100);
            int reminder_height = reminder.Height + ((reminder.Height * 100) / 100);

            Bitmap reminder1 = new Bitmap(reminder_width, reminder_height);
            Graphics r = Graphics.FromImage(reminder1);
            r.DrawImage(reminder, new Rectangle(Point.Empty, reminder1.Size));
            pictureBox2.Image = reminder1;            
        }
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = reminder;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e) //note
        {
            int note_width = note.Width + ((note.Width * 100) / 100);
            int note_height = note.Height + ((note.Height * 100) / 100);

            Bitmap note1 = new Bitmap(note_width, note_height);
            Graphics r = Graphics.FromImage(note1);
            r.DrawImage(note, new Rectangle(Point.Empty, note1.Size));
            pictureBox3.Image = note1;
        }
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = note;
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e) //help
        {
            int help_width = help.Width + ((help.Width * 100) / 100);
            int help_height = help.Height + ((help.Height * 100) / 100);

            Bitmap help1 = new Bitmap(help_width, help_height);
            Graphics r = Graphics.FromImage(help1);
            r.DrawImage(help, new Rectangle(Point.Empty, help1.Size));
            pictureBox4.Image = help1;
        }
        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = help;
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e) //about
        {
            int about_width = about.Width + ((about.Width * 100) / 100);
            int about_height = about.Height + ((help.Height * 100) / 100);

            Bitmap about1 = new Bitmap(about_width, about_height);
            Graphics r = Graphics.FromImage(about1);
            r.DrawImage(about, new Rectangle(Point.Empty, about1.Size));
            pictureBox5.Image = about1;
        }
        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = about;
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e) //refresh
        {
            int refresh_width = refresh.Width + ((refresh.Width * 100) / 100);
            int refresh_height = refresh.Height + ((refresh.Height * 100) / 100);

            Bitmap refresh1 = new Bitmap(refresh_width, refresh_height);
            Graphics r = Graphics.FromImage(refresh1);
            r.DrawImage(refresh, new Rectangle(Point.Empty, refresh1.Size));
            pictureBox6.Image = refresh1;
        }
        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Image = refresh;
        }
        //end

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string city;
                city = richTextBox1.Text;

                string uri = string.Format("http://api.weatherapi.com/v1/forecast.xml?key=1f9ee30b31fc4f97b6c33154200311&q={0}&days=1", city);
                XDocument doc = XDocument.Load(uri);

                string iconUri = (string)doc.Descendants("icon").FirstOrDefault();

                WebClient client = new WebClient();

                byte[] image = client.DownloadData("http:" + iconUri);

                MemoryStream stream = new MemoryStream(image);
                Bitmap newBitmap = new Bitmap(stream);

                string temp = (string)doc.Descendants("temp_f").FirstOrDefault();

                string maxTemp = (string)doc.Descendants("maxtemp_f").FirstOrDefault();
                string minTemp = (string)doc.Descendants("mintemp_f").FirstOrDefault();

                string windmph = (string)doc.Descendants("maxwind_mph").FirstOrDefault();

                string humidity = (string)doc.Descendants("avghumidity").FirstOrDefault();

                string country = (string)doc.Descendants("country").FirstOrDefault();

                string csrs = (string)doc.Descendants("text").FirstOrDefault(); //csrs: cloudy,sunny,rain,snow 

                Bitmap icon = newBitmap;

                label19.Text = temp + " °F";
                label31.Text = maxTemp + " °F";
                label32.Text = minTemp + " °F";
                label33.Text = windmph + " mph";
                label34.Text = humidity + " %";
                label27.Text = csrs;
                label35.Text = country;
                pictureBox1.Image = icon;
                richTextBox1.Text = city;
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                toolTip1.IsBalloon = true;
                toolTip1.ToolTipTitle = "Invalid input.";
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(richTextBox1, "Letters only!");
            }
        }

        //Move borderless form
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        //------------------------------------------------End---------------------------------------------------//
    }
}
