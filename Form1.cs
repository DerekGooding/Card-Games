using Poker.Logic;
using Poker.Model;
using Poker.Services;

namespace Poker;

public partial class Form1 : Form
{
    private readonly List<string> _names = [];
    private readonly List<int> _balances = [];
    private readonly List<Player> _players = [];
    private readonly BlackjackGame _game;
    private readonly List<Panel> _playerpanels = [];
    private int _currentBettingPlayer;

    public Form1(List<string> names, List<int> balances)
    {
        InitializeComponent();
        HideButtons();
        _names = names;
        _balances = balances;
        for (var i = 0; i < _names.Count; i++)
        {
            var player = new Player(_names[i], _balances[i]);
            _players.Add(player);
        }
        var dealer = new Player("Dealer", 10000) { IsDealer = true };
        _game = new BlackjackGame(_players, 6, dealer);
    }

    private void CheckIfRoundOver()
    {
        if (_game.IsRoundOver())
        {
            _game.ResolveRound();
            dilerKarte.Controls.Clear();
            foreach (var card in _game.Dealer.Hands[0].Cards)
            {
                var cardPicture = new PictureBox
                {
                    Image = ImageCacheService.Get(card),
                    Size = new Size(80, 120),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                dilerKarte.Controls.Add(cardPicture);
            }
            MessageBox.Show("Round over. Starting new round.");
            _game.Start1();
            betting_button.Show();
            bet_amount.Show();
            bet_show.Show();
            Split.Hide();
            Hit.Hide();
            Stand.Hide();
            DoubleDown.Hide();
        }
    }
    private void UpdatePlayerPanels()
    {
        if (_game.CurrentPlayerIndex >= _game.Players.Count)
        {
            _game.CurrentPlayerIndex--;
            _game.Players[_game.CurrentPlayerIndex].CurrentHand--;
            trenutniUlog.Text = "Current bet: " + _game.Players[_game.CurrentPlayerIndex].Hands[_game.Players[_game.CurrentPlayerIndex].CurrentHand].Bet.ToString();
            trenutnaRuka.Text = "Current hand number: " + (_game.Players[_game.CurrentPlayerIndex].CurrentHand + 1).ToString();
            balance.Text = "Balance: " + _game.Players[_game.CurrentPlayerIndex].Balance.ToString();
            _game.Players[_game.CurrentPlayerIndex].CurrentHand++;
            _game.CurrentPlayerIndex++;

        }
        else
        {
            trenutniUlog.Text = "Current bet: " + _game.Players[_game.CurrentPlayerIndex].Hands[_game.Players[_game.CurrentPlayerIndex].CurrentHand].Bet.ToString();
            trenutnaRuka.Text = "Current hand number: " + (_game.Players[_game.CurrentPlayerIndex].CurrentHand + 1).ToString();
            balance.Text = "Balance: " + _game.Players[_game.CurrentPlayerIndex].Balance.ToString();
        }
        ruke.Controls.Clear();
        foreach (var ruka in _game.Players[0].Hands)
        {
            var ruka1 = new FlowLayoutPanel
            {
                AutoSize = true,
                BackColor = Color.Transparent
            };
            ruke.Controls.Add(ruka1);
            foreach (var card in ruka.Cards)
            {
                var cardPicture = new PictureBox
                {
                    Size = new Size(80, 120),
                    Image = ImageCacheService.Get(card),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                ruka1.Controls.Add(cardPicture);
            }
        }
        dilerKarte.Controls.Clear();
        foreach (var card in _game.Dealer.Hands[0].Cards)
        {
            var cardPicture = new PictureBox
            {
                Image = ImageCacheService.Get(card),
                Size = new Size(80, 120),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            dilerKarte.Controls.Add(cardPicture);
        }
        CheckIfRoundOver();
    }
    private void HideButtons()
    {
        Hit.Hide();
        Stand.Hide();
        DoubleDown.Hide();
        Split.Hide();
        betting_button.Hide();
    }

    private void Stand_Click(object sender, EventArgs e)
    {
        _game.Stand();
        UpdatePlayerPanels();

    }

    private void Hit_Click(object sender, EventArgs e)
    {
        _game.Hit();
        UpdatePlayerPanels();

    }

    private void DoubleDown_Click(object sender, EventArgs e)
    {
        _game.DoubleDown();
        UpdatePlayerPanels();
    }

    private void Split_Click(object sender, EventArgs e)
    {
        _game.Split();
        UpdatePlayerPanels();
    }

    private void Form1_Load(object sender, EventArgs e) { }

    private void Betting_button_Click(object sender, EventArgs e)
    {
        float bet = bet_amount.Value;


        _game.Players[_currentBettingPlayer].PlaceBet(bet);

        _currentBettingPlayer++;
        if (_currentBettingPlayer >= _game.Players.Count)
        {
            betting_button.Hide();
            bet_amount.Hide();
            bet_show.Hide();

            Hit.Show();
            Stand.Show();
            DoubleDown.Show();
            Split.Show();
            //game.start1();
            _game.Start2();
            _currentBettingPlayer = 0;
            UpdatePlayerPanels();

        }

    }

    private void Bet_amount_Scroll(object sender, EventArgs e) => bet_show.Text = bet_amount.Value.ToString();

    private void Start_Click(object sender, EventArgs e)
    {
        _game.Start1();
        betting_button.Show();
        _game.CurrentPlayerIndex = 0;


        Start.Hide();
    }
}
