namespace Poker
{
    partial class Home
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
            this.Poker = new System.Windows.Forms.Button();
            this.Blackjack = new System.Windows.Forms.Button();
            this.Name = new System.Windows.Forms.TextBox();
            this.Balance = new System.Windows.Forms.TextBox();
            this.addplayer = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Poker
            // 
            this.Poker.Location = new System.Drawing.Point(133, 88);
            this.Poker.Name = "Poker";
            this.Poker.Size = new System.Drawing.Size(75, 23);
            this.Poker.TabIndex = 0;
            this.Poker.Text = "button1";
            this.Poker.UseVisualStyleBackColor = true;
            // 
            // Blackjack
            // 
            this.Blackjack.Location = new System.Drawing.Point(499, 88);
            this.Blackjack.Name = "Blackjack";
            this.Blackjack.Size = new System.Drawing.Size(75, 23);
            this.Blackjack.TabIndex = 1;
            this.Blackjack.Text = "Blackjack";
            this.Blackjack.UseVisualStyleBackColor = true;
            this.Blackjack.Click += new System.EventHandler(this.Blackjack_Click);
            // 
            // Name
            // 
            this.Name.Location = new System.Drawing.Point(324, 176);
            this.Name.Name = "Name";
            this.Name.Size = new System.Drawing.Size(155, 20);
            this.Name.TabIndex = 2;
            // 
            // Balance
            // 
            this.Balance.Location = new System.Drawing.Point(325, 218);
            this.Balance.Name = "Balance";
            this.Balance.Size = new System.Drawing.Size(153, 20);
            this.Balance.TabIndex = 3;
            // 
            // addplayer
            // 
            this.addplayer.Location = new System.Drawing.Point(542, 193);
            this.addplayer.Name = "addplayer";
            this.addplayer.Size = new System.Drawing.Size(75, 23);
            this.addplayer.TabIndex = 4;
            this.addplayer.Text = "Add";
            this.addplayer.UseVisualStyleBackColor = true;
            this.addplayer.Click += new System.EventHandler(this.Addplayer_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(163, 176);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox1.Size = new System.Drawing.Size(128, 62);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Type name in top box and balance in bottom, max 7 players";
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(335, 380);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 6;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.start);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.addplayer);
            this.Controls.Add(this.Balance);
            this.Controls.Add(this.Name);
            this.Controls.Add(this.Blackjack);
            this.Controls.Add(this.Poker);
            
            this.Text = "Home";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Poker;
        private System.Windows.Forms.Button Blackjack;
        private new System.Windows.Forms.TextBox Name;
        private System.Windows.Forms.TextBox Balance;
        private System.Windows.Forms.Button addplayer;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button start;
    }
}