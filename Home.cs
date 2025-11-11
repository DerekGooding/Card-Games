using Poker.Model;

namespace Poker;

public partial class Home : Form
{
    private readonly List<PlayerData> _playerDatas = [];

    public Home() => InitializeComponent();

    private void Addplayer_Click(object sender, EventArgs e)
    {
        if (name.Text.Length == 0 || balance.Text.Length == 0)
        {
            MessageBox.Show("Please enter a name and balance");
            return;
        }
        if (!int.TryParse(balance.Text, out var n) || n < 0)
        {
            MessageBox.Show("Please enter a valid balance");
            return;
        }
        if (_playerDatas.Any(x => x.Name == name.Text))
        {
            MessageBox.Show("Player already exists");
            return;
        }
        if (_playerDatas.Count >= 7)
        {
            MessageBox.Show("Maximum number of players reached");
            return;
        }
        _playerDatas.Add(new(name.Text, n));
        var item = new ListViewItem(name.Text);
        item.SubItems.Add(balance.Text);
        listView1.Items.Add(item);
        name.Clear();
        balance.Clear();
    }

    private void Start_Click(object sender, EventArgs e)
    {
        var form1 = new Form1(_playerDatas);
        form1.Show();
        Hide();
    }
}
