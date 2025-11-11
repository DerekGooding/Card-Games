namespace Poker;

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

        string imageName = $"{card.Rank}{card.CardSuit}.png";
        return Image.FromFile($"E:\\projekti\\Poker\\Resources\\{imageName}");
    }
    private void checkIfRoundOver()//premestiti ovu funkciju, naci joj advekvatno mesto.
    {

        if (game.isRoundOver())
        {
            game.resolveRound();
            dilerKarte.Controls.Clear();
            foreach (var card in game.dealer.Hands[0].Cards)
            {
                PictureBox cardPicture = new PictureBox();
                cardPicture.Image = GetCardImage(card);
                cardPicture.Size = new Size(80, 120);
                cardPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                dilerKarte.Controls.Add(cardPicture);
            }
            MessageBox.Show("Round over. Starting new round.");
            game.start1();
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
        foreach (var card in game.dealer.Hands[0].Cards)
        {
            PictureBox cardPicture = new PictureBox();
            cardPicture.Image = GetCardImage(card);
            cardPicture.Size = new Size(80, 120);
            cardPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            dilerKarte.Controls.Add(cardPicture);
        }
        checkIfRoundOver();
    }
    public void hideButtons()
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
        hideButtons();
        Names = names;
        Balances = balances;
        for(int i = 0; i<Names.Count; i++)
        {
            Blackjackplayer player = new Blackjackplayer(Names[i], Balances[i]);
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

    }

    private void betting_button_Click(object sender, EventArgs e)
    {
        float bet = bet_amount.Value;


        game.Players[currentBettingPlayer].PlaceBet(bet);

        currentBettingPlayer++;
        if (currentBettingPlayer >= game.Players.Count)
        {
            betting_button.Hide();
            bet_amount.Hide();
            bet_show.Hide();

            Hit.Show();
            Stand.Show();
            DoubleDown.Show();
            Split.Show();
            //game.start1();
            game.start2();
            currentBettingPlayer = 0;
            UpdatePlayerPanels();

        }

    }

    private void bet_amount_Scroll(object sender, EventArgs e)
    {
        bet_show.Text = bet_amount.Value.ToString();

    }

    private void Start_Click(object sender, EventArgs e)
    {
        game.start1();
        betting_button.Show();
        game.currentPlayer = 0;


        Start.Hide();
    }

    private void dealerCards_TextChanged(object sender, EventArgs e)
    {

    }
}
