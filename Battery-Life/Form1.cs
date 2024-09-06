using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Media;



namespace Battery_Life
{
    public partial class Form1 : Form
    {
        private System.Threading.Timer _timer;

        private bool dragging = false;
        private Point startPoint;



        public Form1()
        {
            InitializeComponent();

        }



        private void StartTimer()
        {
            _timer = new System.Threading.Timer(Callback, null, 0, 10000);
        }

     

        private void Callback(object state)
        {

            this.Invoke((MethodInvoker) delegate
            {
               
                try
                {
                    string power = SystemInformation.PowerStatus.PowerLineStatus.ToString();
                    PowerStatus p = SystemInformation.PowerStatus;
                    int a = (int)(p.BatteryLifePercent * 100);
                    int min = Properties.Settings.Default.min;
                    int max = Properties.Settings.Default.max;

                    if (power == "Online" && a >= max)
                    {
                        pictureBox1.Image = Properties.Resources.full1;
                        label1.Text = "Battery is fully charged.";
                        this.WindowState = FormWindowState.Normal;
                        this.Opacity = 99;
                        SoundPlayer player = new SoundPlayer(@"C:\wav\charged.wav");
                        player.Play();
                        StopTimer();

                    }
                    else if (power == "Offline" && a <= min)
                    {
                        pictureBox1.Image = Properties.Resources.low;
                        label1.Text = "Battery is low.";
                        this.WindowState = FormWindowState.Normal;
                        this.Opacity = 99;
                        SoundPlayer player = new SoundPlayer(@"C:\wav\uncharged.wav");
                        player.Play();
                        StopTimer();
                        StopTimer();

                    }


                }
                catch (Exception ex)
                {
                   

                }



            });
        }

        private void StopTimer()
        {
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.full1;
            StartTimer();



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.WindowState = FormWindowState.Minimized;
            Thread.Sleep(1000);
            StartTimer();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings st = new Settings();
            st.ShowDialog();

        }
    }
}
