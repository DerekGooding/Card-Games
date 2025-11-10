using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        bool blackjack = false;
        bool poker = false;
        private void Blackjack_Click(object sender, EventArgs e)
        {
            poker = false;
            blackjack = true;
        }
        public List<string> listnames = new List<string>();
        public List<int> listbalances = new List<int>();

        private void addplayer_Click(object sender, EventArgs e)
        {
            if (Name.Text == "" || Balance.Text == "")
            {
                MessageBox.Show("Please enter a name and balance");
                return;
            }
            if (!int.TryParse(Balance.Text, out int n) || Convert.ToInt32(Balance.Text) < 0)
            {
                MessageBox.Show("Please enter a valid balance");
                return;
            }
            if(listnames.Contains(Name.Text))
            {
                MessageBox.Show("Player already exists");
                return;
            }
            if(listnames.Count >= 7)
            {
                MessageBox.Show("Maximum number of players reached");
                return;
            }
            listnames.Add(Name.Text);
            listbalances.Add(Convert.ToInt32(Balance.Text));
            Name.Clear();
            Balance.Clear();
        }

        private void start_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(listnames, listbalances);
            form1.Show();
            this.Hide();
        }
    }
}
