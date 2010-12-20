namespace RzezniaMagow
{
    partial class ClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.label1 = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.OK_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FIREradioButton = new System.Windows.Forms.RadioButton();
            this.LIFEradioButton = new System.Windows.Forms.RadioButton();
            this.ICEradioButton = new System.Windows.Forms.RadioButton();
            this.DEATHradioButton = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Podaj IP serwera";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(32, 29);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(128, 20);
            this.maskedTextBox1.TabIndex = 1;
            this.maskedTextBox1.Text = "192.168.1.3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(216, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Podaj Nick";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(220, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(77, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Newbie";
            // 
            // OK_button
            // 
            this.OK_button.Location = new System.Drawing.Point(32, 207);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(75, 50);
            this.OK_button.TabIndex = 4;
            this.OK_button.Text = "OK";
            this.OK_button.UseVisualStyleBackColor = true;
            this.OK_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cancel_button
            // 
            this.Cancel_button.Location = new System.Drawing.Point(113, 207);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(77, 50);
            this.Cancel_button.TabIndex = 5;
            this.Cancel_button.Text = "CANCEL";
            this.Cancel_button.UseVisualStyleBackColor = true;
            this.Cancel_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.Crimson;
            this.label3.Location = new System.Drawing.Point(29, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Podaj Port";
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(33, 80);
            this.maskedTextBox2.Mask = "00000";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(87, 20);
            this.maskedTextBox2.TabIndex = 7;
            this.maskedTextBox2.Text = "20000";
            this.maskedTextBox2.ValidatingType = typeof(int);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.Crimson;
            this.label4.Location = new System.Drawing.Point(216, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Wybierz Avatara";
            // 
            // FIREradioButton
            // 
            this.FIREradioButton.AutoSize = true;
            this.FIREradioButton.BackColor = System.Drawing.Color.Transparent;
            this.FIREradioButton.Checked = true;
            this.FIREradioButton.Location = new System.Drawing.Point(220, 170);
            this.FIREradioButton.Name = "FIREradioButton";
            this.FIREradioButton.Size = new System.Drawing.Size(42, 17);
            this.FIREradioButton.TabIndex = 9;
            this.FIREradioButton.TabStop = true;
            this.FIREradioButton.Text = "Fire";
            this.FIREradioButton.UseVisualStyleBackColor = false;
            // 
            // LIFEradioButton
            // 
            this.LIFEradioButton.AutoSize = true;
            this.LIFEradioButton.BackColor = System.Drawing.Color.Transparent;
            this.LIFEradioButton.Location = new System.Drawing.Point(220, 280);
            this.LIFEradioButton.Name = "LIFEradioButton";
            this.LIFEradioButton.Size = new System.Drawing.Size(49, 17);
            this.LIFEradioButton.TabIndex = 10;
            this.LIFEradioButton.TabStop = true;
            this.LIFEradioButton.Text = "Love";
            this.LIFEradioButton.UseVisualStyleBackColor = false;
            // 
            // ICEradioButton
            // 
            this.ICEradioButton.AutoSize = true;
            this.ICEradioButton.BackColor = System.Drawing.Color.Transparent;
            this.ICEradioButton.Location = new System.Drawing.Point(310, 170);
            this.ICEradioButton.Name = "ICEradioButton";
            this.ICEradioButton.Size = new System.Drawing.Size(40, 17);
            this.ICEradioButton.TabIndex = 11;
            this.ICEradioButton.TabStop = true;
            this.ICEradioButton.Text = "Ice";
            this.ICEradioButton.UseVisualStyleBackColor = false;
            // 
            // DEATHradioButton
            // 
            this.DEATHradioButton.AutoSize = true;
            this.DEATHradioButton.BackColor = System.Drawing.Color.Transparent;
            this.DEATHradioButton.Location = new System.Drawing.Point(310, 283);
            this.DEATHradioButton.Name = "DEATHradioButton";
            this.DEATHradioButton.Size = new System.Drawing.Size(54, 17);
            this.DEATHradioButton.TabIndex = 12;
            this.DEATHradioButton.TabStop = true;
            this.DEATHradioButton.Text = "Death";
            this.DEATHradioButton.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ImageKey = "(none)";
            this.button1.Location = new System.Drawing.Point(220, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 84);
            this.button1.TabIndex = 13;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.ImageKey = "(none)";
            this.button2.Location = new System.Drawing.Point(310, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 84);
            this.button2.TabIndex = 14;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.ImageKey = "(none)";
            this.button3.Location = new System.Drawing.Point(220, 190);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(65, 84);
            this.button3.TabIndex = 15;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.ImageKey = "(none)";
            this.button4.Location = new System.Drawing.Point(310, 193);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(65, 84);
            this.button4.TabIndex = 16;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(453, 330);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DEATHradioButton);
            this.Controls.Add(this.ICEradioButton);
            this.Controls.Add(this.LIFEradioButton);
            this.Controls.Add(this.FIREradioButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maskedTextBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.OK_button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.label1);
            this.Name = "ClientForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button OK_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton FIREradioButton;
        private System.Windows.Forms.RadioButton LIFEradioButton;
        private System.Windows.Forms.RadioButton ICEradioButton;
        private System.Windows.Forms.RadioButton DEATHradioButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}