namespace Card_Games.Forms;


//add counter to all actions to know when to reset game
public partial class Form1 : Form
{
    public List<string> Names = [];
    public List<int> Balances = [];
    public List<Player> players = [];
    public BlackjackGame game;
    List<Panel> playerpanels = [];
    private Image GetCardImage(Cards card)
    {
        string appBasePath = AppDomain.CurrentDomain.BaseDirectory;
        string imageName = $"{card.Rank}{card.CardSuit}.png";
        string relativePath = $"Resources\\{imageName}";
        string fullPath = Path.Combine(appBasePath, relativePath);
        return Image.FromFile(fullPath);
    }
    private void CheckIfRoundOver()//premestiti ovu funkciju, naci joj advekvatno mesto.
    {

        if (game.IsRoundOver())
        {
            game.CurrentPlayer--;
            game.Players[game.CurrentPlayer].CurrentHand--;
            string bet = game.Players[game.CurrentPlayer].Hands[game.Players[game.CurrentPlayer].CurrentHand].Bet.ToString();
            string ch = (game.Players[game.CurrentPlayer].CurrentHand + 1).ToString();
            int temp = game.CurrentPlayer;
            game.ResolveRound();
            string bal = game.Players[temp].Balance.ToString();
            dilerKarte.Controls.Clear();
            UpdatePlayerPanels(true,ch,bal);
            MessageBox.Show("Round over. Starting new round.");
            game.Start1();
            ShowButtons();
        }
    }
    private void UpdatePlayerPanels(bool isOver,string ch, string bal)
    {
        trenutniUlog.Text = "Current bet: " + 0;
        trenutnaRuka.Text = "Current hand number: " + ch;
        balance.Text = "Balance: " + bal;
        ruke.Controls.Clear();
        foreach (var ruka in game.Players[0].Hands)
        {
            FlowLayoutPanel ruka1 = new FlowLayoutPanel
            {
                AutoSize = true,
                BackColor = Color.Transparent
            };
            ruke.Controls.Add(ruka1);
            foreach (var card in ruka.Cards)
            {
                PictureBox cardPicture = new PictureBox
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
            PictureBox cardPicture = new PictureBox
            {
                Image = GetCardImage(card),
                Size = new Size(80, 120),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            dilerKarte.Controls.Add(cardPicture);
        }
    }
    private void ShowButtons()
    {
        betting_button.Show();
        bet_amount.Show();
        bet_show.Show();
        Split.Hide();
        Hit.Hide();
        Stand.Hide();
        DoubleDown.Hide();
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
            FlowLayoutPanel ruka1 = new FlowLayoutPanel
            {
                AutoSize = true,
                BackColor = Color.Transparent
            };
            ruke.Controls.Add(ruka1);
              foreach(var card in ruka.Cards)
              {
                PictureBox cardPicture = new PictureBox
                {
                    Size = new Size(80, 120),
                    Image = GetCardImage(card),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                ruka1.Controls.Add(cardPicture);
              }
        }
        dilerKarte.Controls.Clear();

        PictureBox dcardPicture = new PictureBox();
        string appBasePath = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = $"Resources\\{"putincardfinal.png"}";
        string fullPath = Path.Combine(appBasePath, relativePath);
        dcardPicture.Image = Image.FromFile(fullPath);
        dcardPicture.Size = new Size(80, 120);
        dcardPicture.SizeMode = PictureBoxSizeMode.StretchImage;
        dilerKarte.Controls.Add(dcardPicture);
        PictureBox dcardPicture2 = new PictureBox
        {
            Image = GetCardImage(game.Dealer.Hands[0].Cards[1]),
            Size = new Size(80, 120),
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        dilerKarte.Controls.Add(dcardPicture2);
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
    public Form1(List<string> names,List<int> balances)
    {
        InitializeComponent();
        HideButtons();
        Names = names;
        Balances = balances;
        for(int i = 0; i<Names.Count; i++)
        {
            Player player = new Player(Names[i], Balances[i]);
            players.Add(player);
        }
        BlackjackDealer dealer = new BlackjackDealer();
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

    private void Form1_Load(object sender, EventArgs e)
    {
        game.Start1();
        betting_button.Show();
        game.CurrentPlayer = 0;
        Start.Hide();
        balance.Hide();
        trenutnaRuka.Hide();
        trenutniUlog.Hide();
    }

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

            game.DealInitialCards();
            _currentBettingPlayer = 0;
            UpdatePlayerPanels();
            balance.Show();
            trenutnaRuka.Show();
            trenutniUlog.Show();
        }
    }

    private void Bet_amount_Scroll(object sender, EventArgs e) => bet_show.Text = bet_amount.Value.ToString();

    private void Start_Click(object sender, EventArgs e)
    {

    }

    private void DealerCards_TextChanged(object sender, EventArgs e)
    {

    }

    private void Balance_Click(object sender, EventArgs e)
    {

    }

    private void Exit_Click(object sender, EventArgs e) => Close();
}
