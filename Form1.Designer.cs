
namespace gedcomtodatabase_c
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
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblDataset = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.textBoxDataset = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.lblHeading = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblHost = new System.Windows.Forms.Label();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.AcceptsReturn = true;
            this.textBoxMsg.CausesValidation = false;
            this.textBoxMsg.ForeColor = System.Drawing.Color.Blue;
            this.textBoxMsg.Location = new System.Drawing.Point(37, 197);
            this.textBoxMsg.Multiline = true;
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.ReadOnly = true;
            this.textBoxMsg.Size = new System.Drawing.Size(663, 57);
            this.textBoxMsg.TabIndex = 12;
            this.textBoxMsg.TabStop = false;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(64, 52);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(26, 13);
            this.lblFile.TabIndex = 2;
            this.lblFile.Text = "File:";
            // 
            // lblDataset
            // 
            this.lblDataset.AutoSize = true;
            this.lblDataset.Location = new System.Drawing.Point(43, 105);
            this.lblDataset.Name = "lblDataset";
            this.lblDataset.Size = new System.Drawing.Size(57, 13);
            this.lblDataset.TabIndex = 6;
            this.lblDataset.Text = "DataBase:";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(56, 130);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(32, 13);
            this.lblUser.TabIndex = 8;
            this.lblUser.Text = "User:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(34, 155);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password:";
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(100, 50);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(600, 20);
            this.textBoxFile.TabIndex = 3;
            // 
            // textBoxDataset
            // 
            this.textBoxDataset.Location = new System.Drawing.Point(100, 100);
            this.textBoxDataset.Name = "textBoxDataset";
            this.textBoxDataset.Size = new System.Drawing.Size(600, 20);
            this.textBoxDataset.TabIndex = 7;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(100, 125);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(600, 20);
            this.textBoxUser.TabIndex = 9;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(100, 150);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(600, 20);
            this.textBoxPassword.TabIndex = 11;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Red;
            this.lblHeading.Location = new System.Drawing.Point(234, 9);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(316, 26);
            this.lblHeading.TabIndex = 1;
            this.lblHeading.Text = "Convert GEDCOM to Database";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(350, 347);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 13;
            this.btnConvert.TabStop = false;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(55, 80);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(32, 13);
            this.lblHost.TabIndex = 4;
            this.lblHost.Text = "Host:";
            // 
            // textBoxHost
            // 
            this.textBoxHost.Location = new System.Drawing.Point(100, 75);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(600, 20);
            this.textBoxHost.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxHost);
            this.Controls.Add(this.lblHost);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.textBoxDataset);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblDataset);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.textBoxMsg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "getcomtodatabase-c";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMsg;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblDataset;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.TextBox textBoxDataset;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox textBoxHost;
    }
}

