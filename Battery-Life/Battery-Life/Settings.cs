using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Battery_Life
{
    partial class Settings : Form
    {

        string bfullwav;
        string blowwav;
        
        string targetDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battery Life", "Sound");


        public Settings()
        {
            InitializeComponent();
            cdir();


        }
        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

    


        private void Settings_Load(object sender, EventArgs e)
        {
            trackBar1.Value = Properties.Settings.Default.min;
            trackBar2.Value = Properties.Settings.Default.max;
            minper.Text = Properties.Settings.Default.min + "%";
            maxper.Text = Properties.Settings.Default.max + "%";

            blow.Text = Properties.Settings.Default.blow;
            bfull.Text = Properties.Settings.Default.bfull;

            if (Properties.Settings.Default.sound == 0)
            {
                checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
            }

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.min = trackBar1.Value;
            Properties.Settings.Default.max = trackBar2.Value;


            //MessageBox.Show(targetDirectory);
            
            if(File.Exists(bfull.Text) && File.Exists(blow.Text))
            {
                Properties.Settings.Default.bfull = bfull.Text;
                Properties.Settings.Default.blow = blow.Text;

            }
            else
            {
                Properties.Settings.Default.bfull = @"C:\Program Files\Battery Life\Sound\charged.wav";
                Properties.Settings.Default.blow = @"C:\Program Files\Battery Life\Sound\uncharged.wav";
            }
          
            if (checkBox1.Checked == false)
            {
                Properties.Settings.Default.sound = 0;
            }
            else
            {
                Properties.Settings.Default.sound = 1;
            }

            Properties.Settings.Default.Save();
            this.Hide();
        }

        private void trackBar1_ValueChanged_1(object sender, EventArgs e)
        {
            minper.Text = trackBar1.Value.ToString() + "%";
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            maxper.Text = trackBar2.Value.ToString() + "%";
        }

        public void cdir()
        {
            if (!Directory.Exists(targetDirectory))
            {
                // Create the directory
                Directory.CreateDirectory(targetDirectory);
            }
        }

        public  void DeleteFilesExcept(string directoryPath)
        {
            // Define the exceptions
            string[] exceptions = { "charged.wav", "uncharged.wav" };

            // Get all files in the specified directory
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                // Get the file name without the directory path
                string fileName = Path.GetFileName(file);

                // If the file is not in the exceptions, delete it
                if (Array.IndexOf(exceptions, fileName) == -1)
                {
                    try
                    {
                        File.Delete(file);
                        
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }

            bfull.Text = @"C:\Program Files\Battery Life\Sound\charged.wav";
            blow.Text = @"C:\Program Files\Battery Life\Sound\uncharged.wav";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WAV files (*.wav)|*.wav";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
             
                // Get the selected file path
                bfullwav = openFileDialog.FileName;
                string destinationFile = Path.Combine(targetDirectory, Path.GetFileName(bfullwav));

                // Copy the file to the destination
                File.Copy(bfullwav, destinationFile, true);
                bfull.Text = destinationFile;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WAV files (*.wav)|*.wav";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
              
                // Get the selected file path
                blowwav = openFileDialog.FileName;
                string destinationFile = Path.Combine(targetDirectory, Path.GetFileName(blowwav));

                // Copy the file to the destination
                File.Copy(blowwav, destinationFile, true);
                blow.Text = destinationFile;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DeleteFilesExcept(targetDirectory);
        }
    }
}
