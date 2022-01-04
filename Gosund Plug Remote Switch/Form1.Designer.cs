namespace VRControl
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.onBtn = new System.Windows.Forms.Button();
            this.offBtn = new System.Windows.Forms.Button();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tokenTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.refTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // onBtn
            // 
            this.onBtn.Location = new System.Drawing.Point(32, 46);
            this.onBtn.Name = "onBtn";
            this.onBtn.Size = new System.Drawing.Size(117, 50);
            this.onBtn.TabIndex = 0;
            this.onBtn.Text = "On";
            this.onBtn.UseVisualStyleBackColor = true;
            this.onBtn.Click += new System.EventHandler(this.onBtn_Click);
            // 
            // offBtn
            // 
            this.offBtn.Location = new System.Drawing.Point(201, 46);
            this.offBtn.Name = "offBtn";
            this.offBtn.Size = new System.Drawing.Size(117, 50);
            this.offBtn.TabIndex = 0;
            this.offBtn.Text = "Off";
            this.offBtn.UseVisualStyleBackColor = true;
            this.offBtn.Click += new System.EventHandler(this.offBtn_Click);
            // 
            // ipTextBox
            // 
            this.ipTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ipTextBox.Location = new System.Drawing.Point(99, 111);
            this.ipTextBox.MaxLength = 15;
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(152, 23);
            this.ipTextBox.TabIndex = 2;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Location = new System.Drawing.Point(133, 19);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(86, 15);
            this.label_status.TabIndex = 3;
            this.label_status.Text = "Status: Inactive";
            this.label_status.Click += new System.EventHandler(this.label_status_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP:";
            // 
            // tokenTextBox
            // 
            this.tokenTextBox.Location = new System.Drawing.Point(99, 140);
            this.tokenTextBox.MaxLength = 32;
            this.tokenTextBox.Name = "tokenTextBox";
            this.tokenTextBox.Size = new System.Drawing.Size(152, 23);
            this.tokenTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Token:";
            // 
            // refTimer
            // 
            this.refTimer.Interval = 1000;
            this.refTimer.Tick += new System.EventHandler(this.refTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 178);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.tokenTextBox);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.offBtn);
            this.Controls.Add(this.onBtn);
            this.Name = "Form1";
            this.Text = "Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button onBtn;
        private Button offBtn;
        private TextBox ipTextBox;
        private Label label_status;
        private Label label1;
        private TextBox tokenTextBox;
        private Label label2;
        private System.Windows.Forms.Timer refTimer;
    }
}