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
            name = new TextBox();
            balance = new TextBox();
            addplayer = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            start = new Button();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            textBox4 = new TextBox();
            SuspendLayout();
            // 
            // name
            // 
            name.Location = new Point(12, 46);
            name.Margin = new Padding(4, 3, 4, 3);
            name.Name = "name";
            name.Size = new Size(180, 23);
            name.TabIndex = 1;
            // 
            // balance
            // 
            balance.Location = new Point(12, 115);
            balance.Margin = new Padding(4, 3, 4, 3);
            balance.Name = "balance";
            balance.Size = new Size(178, 23);
            balance.TabIndex = 3;
            // 
            // addplayer
            // 
            addplayer.Location = new Point(12, 185);
            addplayer.Margin = new Padding(4, 3, 4, 3);
            addplayer.Name = "addplayer";
            addplayer.Size = new Size(88, 27);
            addplayer.TabIndex = 5;
            addplayer.Text = "Add";
            addplayer.UseVisualStyleBackColor = true;
            addplayer.Click += Addplayer_Click;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 12F);
            textBox1.Location = new Point(12, 150);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.RightToLeft = RightToLeft.No;
            textBox1.Size = new Size(149, 22);
            textBox1.TabIndex = 4;
            textBox1.Text = "Max 7 players";
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 12F);
            textBox2.Location = new Point(12, 12);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.RightToLeft = RightToLeft.No;
            textBox2.Size = new Size(149, 22);
            textBox2.TabIndex = 0;
            textBox2.Text = "Name";
            // 
            // textBox3
            // 
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 12F);
            textBox3.Location = new Point(12, 81);
            textBox3.Margin = new Padding(4, 3, 4, 3);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.RightToLeft = RightToLeft.No;
            textBox3.Size = new Size(149, 22);
            textBox3.TabIndex = 2;
            textBox3.Text = "Starting Balance";
            // 
            // start
            // 
            start.Location = new Point(12, 423);
            start.Margin = new Padding(4, 3, 4, 3);
            start.Name = "start";
            start.Size = new Size(88, 27);
            start.TabIndex = 7;
            start.Text = "Start";
            start.UseVisualStyleBackColor = true;
            start.Click += Start_Click;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listView1.Location = new Point(12, 258);
            listView1.Name = "listView1";
            listView1.Size = new Size(300, 159);
            listView1.TabIndex = 8;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Balance";
            columnHeader2.Width = 150;
            // 
            // textBox4
            // 
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Font = new Font("Segoe UI", 12F);
            textBox4.Location = new Point(12, 230);
            textBox4.Margin = new Padding(4, 3, 4, 3);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.RightToLeft = RightToLeft.No;
            textBox4.Size = new Size(149, 22);
            textBox4.TabIndex = 9;
            textBox4.Text = "Player List";
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 462);
            Controls.Add(textBox4);
            Controls.Add(listView1);
            Controls.Add(start);
            Controls.Add(textBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox3);
            Controls.Add(addplayer);
            Controls.Add(balance);
            Controls.Add(name);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Home";
            Text = "Blackjack Player";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox balance;
        private System.Windows.Forms.Button addplayer;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.ListView playerList;
        private ListView listView1;
        private TextBox textBox4;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
    }
}