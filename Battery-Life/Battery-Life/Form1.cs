using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Battery_Life
{
    public partial class Form1 : Form
    {
        private System.Threading.Timer _timer;
        private bool dragging = false;
        private Point startPoint;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;

        public Form1()
        {
            InitializeComponent();
            InitializeTrayIcon();
        }

        private void InitializeTrayIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Text = "Battery Life Monitor";
            notifyIcon.Visible = true;
            notifyIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem openMenuItem = new ToolStripMenuItem("Settings");
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit");

            Settings setn = new Settings();

            openMenuItem.Click += (sender, e) => setn.ShowDialog();
            exitMenuItem.Click += (sender, e) => Application.Exit();

            contextMenuStrip.Items.AddRange(new ToolStripMenuItem[] { openMenuItem, exitMenuItem });
            notifyIcon.ContextMenuStrip = contextMenuStrip;

            notifyIcon.DoubleClick += (sender, e) => this.Show();
        }

        private void StartTimer()
        {
            _timer = new System.Threading.Timer(Callback, null, 0, 10000);
        }

        private void Callback(object state)
        {

           
            this.Invoke((MethodInvoker)delegate
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
                        this.Show();
                        pictureBox1.Image = Properties.Resources.full1;
                        label1.Text = "Battery is fully charged.";
                      
                        this.Opacity = 99;
                        try
                        {
                            if (Properties.Settings.Default.sound == 1)
                            {
                                SoundPlayer player = new SoundPlayer(@"C:\wav\charged.wav");
                                player.Play();
                            }
                               
                        }
                        catch (Exception ex)
                        {
                            StopTimer();
                            MessageBox.Show(ex.Message + "\n" + @"C:\wav\charged.wav" + "\n" + @"C:\wav\uncharged.wav", "No Sound Files Found",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                         
                        }
                        StopTimer();
                    }
                    else if (power == "Offline" && a <= min)
                    {
                        this.Show();
                        pictureBox1.Image = Properties.Resources.low;
                        label1.Text = "Battery is low.";
                        
                        this.Opacity = 99;
                        try {

                            if (Properties.Settings.Default.sound == 1)
                            {
                                SoundPlayer player = new SoundPlayer(@"C:\wav\uncharged.wav");
                                player.Play();
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            StopTimer();
                            MessageBox.Show(ex.Message + "\n" + @"C:\wav\charged.wav" + "\n" + @"C:\wav\uncharged.wav", "No Sound Files Found", MessageBoxButtons.OK, MessageBoxIcon.Error);


                        }
                        StopTimer();
                    }
                }
                catch (Exception)
                {
                    // Handle exception
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
            this.Hide(); // Hide the form on startup
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Prevent form from closing
            this.Hide(); // Hide form instead
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Thread.Sleep(1000);
            StartTimer();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings st = new Settings();
            st.ShowDialog();
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

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
