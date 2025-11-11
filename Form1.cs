using Poker.Logic;
using Poker.Model;

namespace Poker;

public partial class Form1 : Form
{
    public List<string> Names = [];
    public List<int> Balances = [];
    public List<Player> players = [];
    public BlackjackGame game;
    readonly List<Panel> _playerpanels = [];
    private static Image GetCardImage(Card card)
    {
        var manager = Properties.Resources.ResourceManager;
        return (Image)manager.GetObject(card.ImageName);
    }
    private void CheckIfRoundOver()//premestiti ovu funkciju, naci joj advekvatno mesto.
    {

        if (game.IsRoundOver())
        {
            game.ResolveRound();
            dilerKarte.Controls.Clear();
            foreach (var card in game.Dealer.Hands[0].Cards)
            {
                var cardPicture = new PictureBox
                {
                    Image = GetCardImage(card),
                    Size = new Size(80, 120),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                dilerKarte.Controls.Add(cardPicture);
            }
            MessageBox.Show("Round over. Starting new round.");
            game.Start1();
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
        if (game.CurrentPlayer >= game.Players.Count)
        {
            game.CurrentPlayer--;
            game.Players[game.CurrentPlayer].CurrentHand--;
            trenutniUlog.Text = "Current bet: " + game.Players[game.CurrentPlayer].Hands[game.Players[game.CurrentPlayer].CurrentHand].Bet.ToString();
            trenutnaRuka.Text = "Current hand number: " + (game.Players[game.CurrentPlayer].CurrentHand + 1).ToString();
            balance.Text = "Balance: " + game.Players[game.CurrentPlayer].Balance.ToString();
            game.Players[game.CurrentPlayer].CurrentHand++;
            game.CurrentPlayer++;

        }
        else
        {
            trenutniUlog.Text = "Current bet: " + game.Players[game.CurrentPlayer].Hands[game.Players[game.CurrentPlayer].CurrentHand].Bet.ToString();
            trenutnaRuka.Text = "Current hand number: " + (game.Players[game.CurrentPlayer].CurrentHand + 1).ToString();
            balance.Text = "Balance: " + game.Players[game.CurrentPlayer].Balance.ToString();
        }
        ruke.Controls.Clear();
        foreach (var ruka in game.Players[0].Hands)
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
                    Image = GetCardImage(card),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                ruka1.Controls.Add(cardPicture);
            }
        }
        dilerKarte.Controls.Clear();
        foreach (var card in game.Dealer.Hands[0].Cards)
        {
            var cardPicture = new PictureBox
            {
                Image = GetCardImage(card),
                Size = new Size(80, 120),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            dilerKarte.Controls.Add(cardPicture);
        }
        CheckIfRoundOver();
    }
    public void HideButtons()
    {
        Hit.Hide();
        Stand.Hide();
        DoubleDown.Hide();
        Split.Hide();
        betting_button.Hide();
    }
    public Form1(List<string> names, List<int> balances)
    {
        InitializeComponent();
        HideButtons();
        Names = names;
        Balances = balances;
        for (var i = 0; i < Names.Count; i++)
        {
            var player = new Blackjackplayer(Names[i], Balances[i]);
            players.Add(player);
        }
        var dealer = new BlackjackDealer();
        game = new BlackjackGame(players, 6, dealer);


    }
    private int _currentBettingPlayer = 0;

    private void Stand_Click(object sender, EventArgs e)
    {
        game.Stand();
        UpdatePlayerPanels();

    }

    private void Hit_Click(object sender, EventArgs e)
    {
        game.Hit();
        UpdatePlayerPanels();

    }

    private void DoubleDown_Click(object sender, EventArgs e)
    {
        game.DoubleDown();
        UpdatePlayerPanels();
    }

    private void Split_Click(object sender, EventArgs e)
    {
        game.Split();
        UpdatePlayerPanels();
    }

    private void Form1_Load(object sender, EventArgs e) { }

    private void Betting_button_Click(object sender, EventArgs e)
    {
        float bet = bet_amount.Value;


        game.Players[_currentBettingPlayer].PlaceBet(bet);

        _currentBettingPlayer++;
        if (_currentBettingPlayer >= game.Players.Count)
        {
            betting_button.Hide();
            bet_amount.Hide();
            bet_show.Hide();

            Hit.Show();
            Stand.Show();
            DoubleDown.Show();
            Split.Show();
            //game.start1();
            game.Start2();
            _currentBettingPlayer = 0;
            UpdatePlayerPanels();

        }

    }

    private void Bet_amount_Scroll(object sender, EventArgs e) => bet_show.Text = bet_amount.Value.ToString();

    private void Start_Click(object sender, EventArgs e)
    {
        game.Start1();
        betting_button.Show();
        game.CurrentPlayer = 0;


        Start.Hide();
    }

    private void DealerCards_TextChanged(object sender, EventArgs e)
    {

    }
}
