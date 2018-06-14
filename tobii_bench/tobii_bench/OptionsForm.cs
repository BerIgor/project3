using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tobii_bench
{
    public partial class OptionsForm : Form
    {
        public int screen_width_mm;
        public int screen_height_mm;

        public bool calibrate;
        public bool display_error;

        private Hashtable args_hash;
        private StreamWriter outputFile;

        public OptionsForm(StreamWriter outputFile)
        {
            this.outputFile = outputFile;

            args_hash = new Hashtable
            {
                ["user"] = "",
                ["environment"] = "",
                ["lighting"] = "",
                ["glasses"] = false,
            };


            InitializeComponent();
        }

        public Hashtable GetArgs()
        {
            return args_hash;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            args_hash["user"] = this.UserText.Text.ToString();
            args_hash["environment"] = this.EnvironmentCombo.SelectedItem.ToString();
            args_hash["lighting"] = this.LightingCombo.SelectedItem.ToString();
            args_hash["glasses"] = this.GlassesCheckBox.Checked;

            outputFile.WriteLine("User:" + args_hash["user"]);
            outputFile.WriteLine("Environment:" + args_hash["environment"]);
            outputFile.WriteLine("Lighting:" + args_hash["lighting"]);
            outputFile.WriteLine("Glasses:" + args_hash["glasses"]);
            outputFile.WriteLine("Resolution:" + GetScreenResolution());
            outputFile.WriteLine("Monitor dimensions:" + GetMonitorDimensions());
            outputFile.WriteLine("=========DATA=========");
            outputFile.WriteLine("Point Coords | Click Coords | Gaze Coords");

            this.Close();
        }

        private string GetMonitorDimensions()
        {
            string monitorDimensions;
            switch (this.screenComboBox.SelectedItem.ToString())
            {
                case "14\"":
                    monitorDimensions = "31.0x17.4";
                    break;
                case "15\"":
                    monitorDimensions = "33.2x18.7";
                    break;
                default:
                    throw new System.ArgumentException("Bad screen size selection");
            }
            return monitorDimensions;
        }

        private string GetScreenResolution()
        {
            Rectangle screen = Screen.PrimaryScreen.Bounds;
            string resolution = screen.Width.ToString() + "x" + screen.Height.ToString();
            return resolution;
        }
    }
}
