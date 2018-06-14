using System;

namespace tobii_bench
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GlassesCheckBox = new System.Windows.Forms.CheckBox();
            this.EnvironmentCombo = new System.Windows.Forms.ComboBox();
            this.UserText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LightingCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.Calibrate = new System.Windows.Forms.CheckBox();
            this.screenComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GlassesCheckBox
            // 
            this.GlassesCheckBox.AutoSize = true;
            this.GlassesCheckBox.Location = new System.Drawing.Point(12, 181);
            this.GlassesCheckBox.Name = "GlassesCheckBox";
            this.GlassesCheckBox.Size = new System.Drawing.Size(63, 17);
            this.GlassesCheckBox.TabIndex = 0;
            this.GlassesCheckBox.Text = "Glasses";
            this.GlassesCheckBox.UseVisualStyleBackColor = true;
            // 
            // EnvironmentCombo
            // 
            this.EnvironmentCombo.FormattingEnabled = true;
            this.EnvironmentCombo.Items.AddRange(new object[] {
            "Indoors",
            "Outdoors"});
            this.EnvironmentCombo.Location = new System.Drawing.Point(81, 101);
            this.EnvironmentCombo.Name = "EnvironmentCombo";
            this.EnvironmentCombo.Size = new System.Drawing.Size(121, 21);
            this.EnvironmentCombo.TabIndex = 1;
            // 
            // UserText
            // 
            this.UserText.Location = new System.Drawing.Point(81, 48);
            this.UserText.Name = "UserText";
            this.UserText.Size = new System.Drawing.Size(100, 20);
            this.UserText.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "User:";
            // 
            // LightingCombo
            // 
            this.LightingCombo.FormattingEnabled = true;
            this.LightingCombo.Items.AddRange(new object[] {
            "Dark",
            "Light",
            "Very bright"});
            this.LightingCombo.Location = new System.Drawing.Point(81, 128);
            this.LightingCombo.Name = "LightingCombo";
            this.LightingCombo.Size = new System.Drawing.Size(121, 21);
            this.LightingCombo.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Screen";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(173, 258);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 7;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // Calibrate
            // 
            this.Calibrate.AutoSize = true;
            this.Calibrate.Location = new System.Drawing.Point(12, 204);
            this.Calibrate.Name = "Calibrate";
            this.Calibrate.Size = new System.Drawing.Size(67, 17);
            this.Calibrate.TabIndex = 12;
            this.Calibrate.Text = "Calibrate";
            this.Calibrate.UseVisualStyleBackColor = true;
            // 
            // screenComboBox
            // 
            this.screenComboBox.FormattingEnabled = true;
            this.screenComboBox.Items.AddRange(new object[] {
            "14\"",
            "15\""});
            this.screenComboBox.Location = new System.Drawing.Point(81, 74);
            this.screenComboBox.Name = "screenComboBox";
            this.screenComboBox.Size = new System.Drawing.Size(121, 21);
            this.screenComboBox.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Environment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Lighting";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 293);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.screenComboBox);
            this.Controls.Add(this.Calibrate);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LightingCombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserText);
            this.Controls.Add(this.EnvironmentCombo);
            this.Controls.Add(this.GlassesCheckBox);
            this.Name = "OptionsForm";
            this.Text = "Session Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }





        #endregion

        private System.Windows.Forms.CheckBox GlassesCheckBox;
        private System.Windows.Forms.ComboBox EnvironmentCombo;
        private System.Windows.Forms.TextBox UserText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox LightingCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.CheckBox Calibrate;
        private System.Windows.Forms.ComboBox screenComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}