namespace Poker
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Hit = new System.Windows.Forms.Button();
            this.Stand = new System.Windows.Forms.Button();
            this.DoubleDown = new System.Windows.Forms.Button();
            this.Split = new System.Windows.Forms.Button();
            this.bet_amount = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bet_show = new System.Windows.Forms.TextBox();
            this.betting_button = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.dilerKarte = new System.Windows.Forms.FlowLayoutPanel();
            this.ruke = new System.Windows.Forms.FlowLayoutPanel();
            this.balance = new System.Windows.Forms.Label();
            this.trenutnaRuka = new System.Windows.Forms.Label();
            this.trenutniUlog = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bet_amount)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Hit
            // 
            this.Hit.Location = new System.Drawing.Point(249, 22);
            this.Hit.Name = "Hit";
            this.Hit.Size = new System.Drawing.Size(89, 32);
            this.Hit.TabIndex = 1;
            this.Hit.Text = "Hit";
            this.Hit.UseVisualStyleBackColor = true;
            this.Hit.Click += new System.EventHandler(this.Hit_Click);
            // 
            // Stand
            // 
            this.Stand.Location = new System.Drawing.Point(357, 22);
            this.Stand.Name = "Stand";
            this.Stand.Size = new System.Drawing.Size(84, 32);
            this.Stand.TabIndex = 2;
            this.Stand.Text = "Stand";
            this.Stand.UseVisualStyleBackColor = true;
            this.Stand.Click += new System.EventHandler(this.Stand_Click);
            // 
            // DoubleDown
            // 
            this.DoubleDown.Location = new System.Drawing.Point(472, 22);
            this.DoubleDown.Name = "DoubleDown";
            this.DoubleDown.Size = new System.Drawing.Size(86, 32);
            this.DoubleDown.TabIndex = 3;
            this.DoubleDown.Text = "Double Down";
            this.DoubleDown.UseVisualStyleBackColor = true;
            this.DoubleDown.Click += new System.EventHandler(this.DoubleDown_Click);
            // 
            // Split
            // 
            this.Split.Location = new System.Drawing.Point(588, 24);
            this.Split.Name = "Split";
            this.Split.Size = new System.Drawing.Size(105, 29);
            this.Split.TabIndex = 4;
            this.Split.Text = "Split";
            this.Split.UseVisualStyleBackColor = true;
            this.Split.Click += new System.EventHandler(this.Split_Click);
            // 
            // bet_amount
            // 
            this.bet_amount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bet_amount.BackColor = System.Drawing.Color.Beige;
            this.bet_amount.Location = new System.Drawing.Point(12, 24);
            this.bet_amount.Maximum = 1000;
            this.bet_amount.Name = "bet_amount";
            this.bet_amount.Size = new System.Drawing.Size(117, 45);
            this.bet_amount.TabIndex = 5;
            this.bet_amount.Scroll += new System.EventHandler(this.Bet_amount_Scroll);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.bet_show);
            this.panel1.Controls.Add(this.betting_button);
            this.panel1.Controls.Add(this.Hit);
            this.panel1.Controls.Add(this.Stand);
            this.panel1.Controls.Add(this.bet_amount);
            this.panel1.Controls.Add(this.DoubleDown);
            this.panel1.Controls.Add(this.Split);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 561);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1119, 95);
            this.panel1.TabIndex = 7;
            // 
            // bet_show
            // 
            this.bet_show.Location = new System.Drawing.Point(12, 72);
            this.bet_show.Name = "bet_show";
            this.bet_show.Size = new System.Drawing.Size(117, 20);
            this.bet_show.TabIndex = 7;
            // 
            // betting_button
            // 
            this.betting_button.Location = new System.Drawing.Point(153, 22);
            this.betting_button.Name = "betting_button";
            this.betting_button.Size = new System.Drawing.Size(75, 23);
            this.betting_button.TabIndex = 6;
            this.betting_button.Text = "Bet";
            this.betting_button.UseVisualStyleBackColor = true;
            this.betting_button.Click += new System.EventHandler(this.Betting_button_Click);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(25, 505);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 16;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // dilerKarte
            // 
            this.dilerKarte.BackColor = System.Drawing.Color.Transparent;
            this.dilerKarte.Location = new System.Drawing.Point(318, 12);
            this.dilerKarte.Name = "dilerKarte";
            this.dilerKarte.Size = new System.Drawing.Size(466, 216);
            this.dilerKarte.TabIndex = 8;
            // 
            // ruke
            // 
            this.ruke.AutoSize = true;
            this.ruke.BackColor = System.Drawing.Color.Transparent;
            this.ruke.Location = new System.Drawing.Point(187, 350);
            this.ruke.Name = "ruke";
            this.ruke.Size = new System.Drawing.Size(908, 178);
            this.ruke.TabIndex = 17;
            // 
            // balance
            // 
            this.balance.AutoSize = true;
            this.balance.Location = new System.Drawing.Point(22, 367);
            this.balance.Name = "balance";
            this.balance.Size = new System.Drawing.Size(35, 13);
            this.balance.TabIndex = 18;
            this.balance.Text = "label1";
            // 
            // trenutnaRuka
            // 
            this.trenutnaRuka.AutoSize = true;
            this.trenutnaRuka.Location = new System.Drawing.Point(22, 408);
            this.trenutnaRuka.Name = "trenutnaRuka";
            this.trenutnaRuka.Size = new System.Drawing.Size(35, 13);
            this.trenutnaRuka.TabIndex = 19;
            this.trenutnaRuka.Text = "label2";
            // 
            // trenutniUlog
            // 
            this.trenutniUlog.AutoSize = true;
            this.trenutniUlog.Location = new System.Drawing.Point(22, 447);
            this.trenutniUlog.Name = "trenutniUlog";
            this.trenutniUlog.Size = new System.Drawing.Size(35, 13);
            this.trenutniUlog.TabIndex = 20;
            this.trenutniUlog.Text = "label3";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1119, 656);
            this.Controls.Add(this.trenutniUlog);
            this.Controls.Add(this.trenutnaRuka);
            this.Controls.Add(this.balance);
            this.Controls.Add(this.ruke);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.dilerKarte);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bet_amount)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Hit;
        private System.Windows.Forms.Button Stand;
        private System.Windows.Forms.Button DoubleDown;
        private System.Windows.Forms.Button Split;
        private System.Windows.Forms.TrackBar bet_amount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button betting_button;
        private System.Windows.Forms.TextBox bet_show;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.FlowLayoutPanel dilerKarte;
        private System.Windows.Forms.FlowLayoutPanel ruke;
        private System.Windows.Forms.Label balance;
        private System.Windows.Forms.Label trenutnaRuka;
        private System.Windows.Forms.Label trenutniUlog;
        private System.Windows.Forms.Timer timer1;
    }
}

