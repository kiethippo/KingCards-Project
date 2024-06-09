namespace Client2
{
    partial class ConnectMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectMenu));
            this.buttonCreate = new System.Windows.Forms.Button();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(214, 260);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(185, 31);
            this.buttonCreate.TabIndex = 0;
            this.buttonCreate.Text = "JOIN";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(216, 142);
            this.textBoxIP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxIP.Multiline = true;
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            this.textBoxIP.Size = new System.Drawing.Size(185, 28);
            this.textBoxIP.TabIndex = 2;
            this.textBoxIP.Text = "127.0.0.1";
            this.textBoxIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(216, 181);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(185, 28);
            this.textBoxName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(139, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(139, 193);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Algerian", 26.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.IndianRed;
            this.label3.Location = new System.Drawing.Point(135, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(333, 39);
            this.label3.TabIndex = 6;
            this.label3.Text = "TIEN LEN MIEN NAM";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(216, 295);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "HOW TO PLAY";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(214, 222);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(187, 34);
            this.button3.TabIndex = 7;
            this.button3.Text = "CREATE";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ConnectMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonCreate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ConnectMenu";
            this.Text = "TIEN LEN MIEN NAM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}