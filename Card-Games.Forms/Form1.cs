//aplikacija ima memory leak, slike se ucitavaju neefikasno i ostaju u memoriji
using Card_Games;

namespace Card_Games.Forms;


//add counter to all actions to know when to reset game
public partial class Form1 : Form
{
    public List<string> Names = new List<string>();
    public List<int> Balances = new List<int>();
    public List<Player> players = new List<Player>();
    public BlackjackGame game;
    List<Panel> playerpanels = new List<Panel>();
    private Image GetCardImage(Cards card)
    {
        string appBasePath = AppDomain.CurrentDomain.BaseDirectory;
        string imageName = $"{card.Rank}{card.CardSuit}.png";
        string relativePath = $"Resources\\{imageName}";
        string fullPath = Path.Combine(appBasePath, relativePath);
        return Image.FromFile(fullPath);
    }
    private void checkIfRoundOver()//premestiti ovu funkciju, naci joj advekvatno mesto.
    {

        if (game.isRoundOver())
        {
            game.currentPlayer--;
            game.Players[game.currentPlayer].currentHand--;
            string bet = game.Players[game.currentPlayer].Hands[game.Players[game.currentPlayer].currentHand].bet.ToString();
            string ch = (game.Players[game.currentPlayer].currentHand + 1).ToString();
            int temp = game.currentPlayer;
            game.resolveRound();
            string bal = game.Players[temp].Balance.ToString();
            dilerKarte.Controls.Clear();
            UpdatePlayerPanels(true,ch,bal);
            MessageBox.Show("Round over. Starting new round.");
            game.start1();
            showButtons();
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
            FlowLayoutPanel ruka1 = new FlowLayoutPanel();
            ruka1.AutoSize = true;
            ruka1.BackColor = Color.Transparent;
            ruke.Controls.Add(ruka1);
            foreach (var card in ruka.Cards)
            {
                PictureBox cardPicture = new PictureBox();
                cardPicture.Size = new Size(80, 120);
                cardPicture.Image = GetCardImage(card);
                cardPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                ruka1.Controls.Add(cardPicture);
            }
        }
        dilerKarte.Controls.Clear();
        foreach (var card in game.dealer.Hands[0].Cards)
        {
            PictureBox cardPicture = new PictureBox();
            cardPicture.Image = GetCardImage(card);
            cardPicture.Size = new Size(80, 120);
            cardPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            dilerKarte.Controls.Add(cardPicture);
        }
    }
    private void showButtons()
    {
        this.betting_button.Show();
        this.bet_amount.Show();
        this.bet_show.Show();
        this.Split.Hide();
        this.Hit.Hide();
        this.Stand.Hide();
        this.DoubleDown.Hide();
    }
    private void UpdatePlayerPanels()
    {
        if (game.currentPlayer >= game.Players.Count)
        {
            game.currentPlayer--;
            game.Players[game.currentPlayer].currentHand--;
            trenutniUlog.Text = "Current bet: " + game.Players[game.currentPlayer].Hands[game.Players[game.currentPlayer].currentHand].bet.ToString();
            trenutnaRuka.Text = "Current hand number: " + (game.Players[game.currentPlayer].currentHand + 1).ToString();
            balance.Text = "Balance: " + game.Players[game.currentPlayer].Balance.ToString();
            game.Players[game.currentPlayer].currentHand++;
            game.currentPlayer++;

        }
        else
        {
            trenutniUlog.Text = "Current bet: " + game.Players[game.currentPlayer].Hands[game.Players[game.currentPlayer].currentHand].bet.ToString();
            trenutnaRuka.Text = "Current hand number: " + (game.Players[game.currentPlayer].currentHand + 1).ToString();
            balance.Text = "Balance: " + game.Players[game.currentPlayer].Balance.ToString();
        }
        ruke.Controls.Clear();
        foreach (var ruka in game.Players[0].Hands)
        {
              FlowLayoutPanel ruka1 = new FlowLayoutPanel();
            ruka1.AutoSize = true;
            ruka1.BackColor = Color.Transparent;
            ruke.Controls.Add(ruka1);
              foreach(var card in ruka.Cards)
              {
                  PictureBox cardPicture = new PictureBox();
                 cardPicture.Size = new Size(80, 120);
                cardPicture.Image = GetCardImage(card);
                  cardPicture.SizeMode = PictureBoxSizeMode.StretchImage;
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
        PictureBox dcardPicture2 = new PictureBox();
        dcardPicture2.Image = GetCardImage(game.dealer.Hands[0].Cards[1]);
        dcardPicture2.Size = new Size(80, 120);
        dcardPicture2.SizeMode = PictureBoxSizeMode.StretchImage;
        dilerKarte.Controls.Add(dcardPicture2);
        checkIfRoundOver();
    }
    public void hideButtons()
    {
        this.Hit.Hide();
        this.Stand.Hide();
        this.DoubleDown.Hide();
        this.Split.Hide();
        this.betting_button.Hide();
    }
    public Form1(List<string> names,List<int> balances)
    {
        InitializeComponent();
        hideButtons();
        this.Names = names;
        this.Balances = balances;
        for(int i = 0; i<Names.Count; i++)
        {
            Player player = new Player(Names[i], Balances[i]);
            players.Add(player);
        }
        BlackjackDealer dealer = new BlackjackDealer();
        game = new BlackjackGame(players, 6, dealer);


    }
    private int currentBettingPlayer = 0;

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
        game.start1();
        this.betting_button.Show();
        game.currentPlayer = 0;
        this.Start.Hide();
        balance.Hide();
        trenutnaRuka.Hide();
        trenutniUlog.Hide();
    }

    private void betting_button_Click(object sender, EventArgs e)
    {
        float bet = bet_amount.Value;
        game.Players[currentBettingPlayer].PlaceBet(bet);
        currentBettingPlayer++;
        if (currentBettingPlayer >= game.Players.Count)
        {
            this.betting_button.Hide();
            this.bet_amount.Hide();
            this.bet_show.Hide();

            this.Hit.Show();
            this.Stand.Show();
            this.DoubleDown.Show();
            this.Split.Show();

            game.DealInitialCards();
            currentBettingPlayer = 0;
            UpdatePlayerPanels();
            balance.Show();
            trenutnaRuka.Show();
            trenutniUlog.Show();
        }
    }

    private void bet_amount_Scroll(object sender, EventArgs e)
    {
        this.bet_show.Text = bet_amount.Value.ToString();

    }

    private void Start_Click(object sender, EventArgs e)
    {

    }

    private void dealerCards_TextChanged(object sender, EventArgs e)
    {

    }

    private void balance_Click(object sender, EventArgs e)
    {

    }

    private void exit_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}
