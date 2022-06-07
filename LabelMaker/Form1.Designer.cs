namespace LabelMaker
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.PN_TB = new System.Windows.Forms.TextBox();
            this.Type_CB = new System.Windows.Forms.ComboBox();
            this.DataRate_CB = new System.Windows.Forms.ComboBox();
            this.Temp_CB = new System.Windows.Forms.ComboBox();
            this.Mode_CB = new System.Windows.Forms.ComboBox();
            this.Wavelength_CB = new System.Windows.Forms.ComboBox();
            this.WDM_CB = new System.Windows.Forms.ComboBox();
            this.Distance_CB = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Compliance_CB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // PN_TB
            // 
            this.PN_TB.Location = new System.Drawing.Point(12, 26);
            this.PN_TB.Name = "PN_TB";
            this.PN_TB.Size = new System.Drawing.Size(100, 20);
            this.PN_TB.TabIndex = 0;
            // 
            // Type_CB
            // 
            this.Type_CB.FormattingEnabled = true;
            this.Type_CB.Location = new System.Drawing.Point(118, 26);
            this.Type_CB.Name = "Type_CB";
            this.Type_CB.Size = new System.Drawing.Size(121, 21);
            this.Type_CB.TabIndex = 1;
            // 
            // DataRate_CB
            // 
            this.DataRate_CB.FormattingEnabled = true;
            this.DataRate_CB.Location = new System.Drawing.Point(245, 26);
            this.DataRate_CB.Name = "DataRate_CB";
            this.DataRate_CB.Size = new System.Drawing.Size(121, 21);
            this.DataRate_CB.TabIndex = 2;
            // 
            // Temp_CB
            // 
            this.Temp_CB.FormattingEnabled = true;
            this.Temp_CB.Location = new System.Drawing.Point(880, 26);
            this.Temp_CB.Name = "Temp_CB";
            this.Temp_CB.Size = new System.Drawing.Size(121, 21);
            this.Temp_CB.TabIndex = 3;
            // 
            // Mode_CB
            // 
            this.Mode_CB.FormattingEnabled = true;
            this.Mode_CB.Location = new System.Drawing.Point(626, 26);
            this.Mode_CB.Name = "Mode_CB";
            this.Mode_CB.Size = new System.Drawing.Size(121, 21);
            this.Mode_CB.TabIndex = 4;
            // 
            // Wavelength_CB
            // 
            this.Wavelength_CB.FormattingEnabled = true;
            this.Wavelength_CB.Location = new System.Drawing.Point(499, 26);
            this.Wavelength_CB.Name = "Wavelength_CB";
            this.Wavelength_CB.Size = new System.Drawing.Size(121, 21);
            this.Wavelength_CB.TabIndex = 5;
            // 
            // WDM_CB
            // 
            this.WDM_CB.FormattingEnabled = true;
            this.WDM_CB.Location = new System.Drawing.Point(372, 26);
            this.WDM_CB.Name = "WDM_CB";
            this.WDM_CB.Size = new System.Drawing.Size(121, 21);
            this.WDM_CB.TabIndex = 6;
            // 
            // Distance_CB
            // 
            this.Distance_CB.FormattingEnabled = true;
            this.Distance_CB.Location = new System.Drawing.Point(753, 26);
            this.Distance_CB.Name = "Distance_CB";
            this.Distance_CB.Size = new System.Drawing.Size(121, 21);
            this.Distance_CB.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 153);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "PN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(242, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Data Rate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(369, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "WDM";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(496, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Wavelength";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(623, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "SM/MM";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(750, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Distance";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(877, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Temperature";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 84);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 18;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(118, 84);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 19;
            // 
            // Compliance_CB
            // 
            this.Compliance_CB.FormattingEnabled = true;
            this.Compliance_CB.Location = new System.Drawing.Point(499, 26);
            this.Compliance_CB.Name = "Compliance_CB";
            this.Compliance_CB.Size = new System.Drawing.Size(121, 21);
            this.Compliance_CB.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 188);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Distance_CB);
            this.Controls.Add(this.WDM_CB);
            this.Controls.Add(this.Wavelength_CB);
            this.Controls.Add(this.Compliance_CB);
            this.Controls.Add(this.Mode_CB);
            this.Controls.Add(this.Temp_CB);
            this.Controls.Add(this.DataRate_CB);
            this.Controls.Add(this.Type_CB);
            this.Controls.Add(this.PN_TB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Sandstone Label God";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PN_TB;
        private System.Windows.Forms.ComboBox Type_CB;
        private System.Windows.Forms.ComboBox DataRate_CB;
        private System.Windows.Forms.ComboBox Temp_CB;
        private System.Windows.Forms.ComboBox Mode_CB;
        private System.Windows.Forms.ComboBox Wavelength_CB;
        private System.Windows.Forms.ComboBox WDM_CB;
        private System.Windows.Forms.ComboBox Distance_CB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox Compliance_CB;
    }
}