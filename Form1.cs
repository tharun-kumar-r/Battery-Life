using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battery
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

           

         

           

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string power = SystemInformation.PowerStatus.PowerLineStatus.ToString();
                PowerStatus p = SystemInformation.PowerStatus;
                int a = (int)(p.BatteryLifePercent * 100);

                if (power == "Online" && a >= 89)
                {
                    timer1.Enabled = false;
                    charged cd = new charged();
                    cd.ShowDialog();
                    
                }
                else if (power == "Offline" && a <= 20)
                {
                    timer1.Enabled = false;
                    uncharged ud = new uncharged();
                    ud.ShowDialog();
                   
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();


        }
    }
}
