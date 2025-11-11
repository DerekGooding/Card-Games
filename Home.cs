using Poker.Model;

namespace Poker;

public partial class Home : Form
{
    private readonly List<PlayerData> _playerDatas = [];

    public Home() => InitializeComponent();

    private void Addplayer_Click(object sender, EventArgs e)
    {
        if (Name.Text.Length == 0 || Balance.Text.Length == 0)
        {
            MessageBox.Show("Please enter a name and balance");
            return;
        }
        if (!int.TryParse(Balance.Text, out var n) || n < 0)
        {
            MessageBox.Show("Please enter a valid balance");
            return;
        }
        if (_playerDatas.Any(x => x.Name == Name.Text))
        {
            MessageBox.Show("Player already exists");
            return;
        }
        if (_playerDatas.Count >= 7)
        {
            MessageBox.Show("Maximum number of players reached");
            return;
        }
        _playerDatas.Add(new(Name.Text, n));
        Name.Clear();
        Balance.Clear();
    }

    private void Start_Click(object sender, EventArgs e)
    {
        var form1 = new Form1(_playerDatas);
        form1.Show();
        Hide();
    }
}
