using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public OptionsForm()
        {
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
            this.Close();
            //throw new NotImplementedException();
        }

    }
}
