﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battery_Life
{
    partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
           
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

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            minper.Text = trackBar1.Value.ToString()+"%";
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            maxper.Text = trackBar2.Value.ToString() + "%";
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            trackBar1.Value = Properties.Settings.Default.min;
            trackBar2.Value = Properties.Settings.Default.max;
            minper.Text = Properties.Settings.Default.min + "%";
            maxper.Text = Properties.Settings.Default.max + "%";

            if (Properties.Settings.Default.sound == 0)
            {
                checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.min = trackBar1.Value;
            Properties.Settings.Default.max= trackBar2.Value;
            if (checkBox1.Checked == false)
            {
                Properties.Settings.Default.sound = 0;
            }
            else
            {
                Properties.Settings.Default.sound =1;
            }

            Properties.Settings.Default.Save();
            this.Hide();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }
    }
}