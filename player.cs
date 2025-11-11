namespace Poker;

public class Player(string name, float balance)
{
    public string Name { get; set; } = name;
    public float Balance { get; set; } = balance;

    public List<float> Bets { get; set; } = new List<float>();
    public class Hand(List<Cards> karte, float bet)
    {
        public List<Cards> Cards { get; set; } = karte;
        public float bet { get; set; } = bet;
        public int getHandValue(bool isAceHigh = true)
        {
            int totalValue = 0;
            int aceCount = 0;
            foreach (var card in Cards)
            {
                if (card.Rank == "A")
                {
                    aceCount++;
                    totalValue += 11;
                }
                else
                {
                    totalValue += card.Value(isAceHigh);
                }
            }

            // Handle aces as 1 if needed to avoid busting
            while (totalValue > 21 && aceCount > 0)
            {
                totalValue -= 10;
                aceCount--;
            }
            return totalValue;
        }
    }

    public List<Hand> Hands { get; set; } = new List<Hand>();
    public int currentHand { get; set; } = 0;
    public bool IsDone()
    {
        return currentHand >= Hands.Count;//ne radi????
    }
    public void PlaceBet(float amount)
    {
        if (amount <= Balance)
        {
            Hands[currentHand].bet = amount;
            Balance -= amount;

        }
        else
        {
            throw new Exception("Insufficient balance to place bet.");
        }
    }
}

public class Cards(string cardRank, string cardSuit)
{
    public string Rank { get; set; } = cardRank;
    public string CardSuit { get; set; } = cardSuit;

    public int Value(bool isAceHigh)
    {
        return Rank == "A" ? isAceHigh ? 11 : 1 : Rank is "K" or "Q" or "J" ? 10 : int.Parse(Rank);
    }

}
public class Deck
{
    public int numberOfDecks { get; set; }
    public List<Cards> CardsAll { get; set; } = new List<Cards>();
    public Deck(int numberOfDecks)
    {
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        this.numberOfDecks = numberOfDecks;
        for (int i = 0; i< numberOfDecks; i++)
        {
            foreach (string rank in ranks)
            {
                foreach (string suit in suits)
                {
                    CardsAll.Add(new Cards(rank, suit));
                }
            }
        }
    }
    public List<Cards> UsedCards { get; set; } = new List<Cards>();
    public void Shuffle()
    {
        Random rand = new Random();
        for (int i = CardsAll.Count - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            var temp = CardsAll[i];
            CardsAll[i] = CardsAll[j];
            CardsAll[j] = temp;
        }
    }
    public void Deal(Player player)
    {
        if (CardsAll.Count < 15)
        {
            for (int i = 0; i < UsedCards.Count; i++)
            {
                CardsAll.Add(UsedCards[i]);
                UsedCards.RemoveAt(i);
            }
        }
        player.Hands[player.currentHand].Cards.Add(CardsAll[0]);
        UsedCards.Add(CardsAll[0]);
        CardsAll.RemoveAt(0);
    }
    public void DealBlackjack(Player player)
    {
        int handnum = player.currentHand;
        if (CardsAll.Count < 15)
        {
            for (int i = 0; i < UsedCards.Count; i++)
            {
                CardsAll.Add(UsedCards[i]);
                UsedCards.RemoveAt(i);
            }
        }
        player.Hands[handnum].Cards.Add(CardsAll[0]);
        UsedCards.Add(CardsAll[0]);
        CardsAll.RemoveAt(0);
    }
    public void clearPlayer(Player player)
    {
        if(player.Name=="Dealer")
        {
            for (int i = 0; i<player.Hands[player.currentHand].Cards.Count; i++)
            {
                UsedCards.Add(player.Hands[player.currentHand].Cards[i]);
            }
            player.Hands[player.currentHand].Cards.Clear();
            return;
        }
        else
        {
            for (int i = 0; i<player.Hands.Count; i++)
            {
                for (int j = 0; j<player.Hands[i].Cards.Count; j++)
                {
                    UsedCards.Add(player.Hands[i].Cards[j]);
                }
            }
            player.Hands.Clear();
        }

    }
}

public class Game
{
    public int currentPlayer { get; set; } = 0;
    public List<Player> Players { get; set; } = new List<Player>();
    public Deck GameDeck { get; set; }
    public int NumberOfDecks { get; set; }
    public Game(List<Player> players, int numberOfDecks)
    {
        this.Players=players;
        this.NumberOfDecks = numberOfDecks;
        GameDeck = new Deck(numberOfDecks);
        GameDeck.Shuffle();
        currentPlayer = 0;
    }

}
public class BlackjackGame(List<Player> players, int numberOfDecks, BlackjackDealer dealer) : Game(players, numberOfDecks)
{

    public List<Blackjackplayer> broke { get; set; } = new List<Blackjackplayer>();
    //create a secondary list of players that are not busted or have blackjack
    public BlackjackDealer dealer { get; set; } = dealer;

    //have to add function for starting a new round, dealer logic, busting, payout 

    public void start1()
    {
        currentPlayer = 0;
        foreach (var player in Players)
        {
            GameDeck.clearPlayer(player);
            player.currentHand = 0;
            Player.Hand newHand = new Player.Hand(new List<Cards>(), 0);
            player.Hands.Add(newHand);
        }
        GameDeck.Shuffle();
        dealer.Hands.Clear();
        DealDealerCards();

        currentPlayer = 0;
    }
    public void start2()
    {
        DealInitialCards();
        //foreach (var player in Players)
        //{
        //    if (checkblackjack(player as Blackjackplayer))
        //    {
        //        player.Balance += player.Hands[player.currentHand].bet * 2.5f;//fix
        //        player.Hands[player.currentHand].bet = 0;
        //        currentPlayer++;
        //    }
        //}
    }

    public bool isRoundOver()
    {
        return currentPlayer >= Players.Count;
    }
    public void resolveRound()//ne vodi svaki korak do resetovanja igre pa zbaga
    {
        DealerPlay();
        int dealerValue = dealer.Hands[0].getHandValue();
        foreach(var player in Players)
        {
            for(int i = 0; i< player.Hands.Count; i++)
            {
                int playerValue = (player as Blackjackplayer).Hands[i].getHandValue();
                if (playerValue > 21)
                {
                    player.Hands[i].bet = 0;
                    MessageBox.Show($"{player.Name} busted and lost their bet.");
                }
                else if (dealerValue>21)
                {
                    player.Balance += player.Hands[i].bet*2;
                    player.Hands[i].bet = 0;
                    MessageBox.Show($"{player.Name} won as the dealer busted and doubled their bet.");
                }
                else if(playerValue > dealerValue)
                {
                    player.Balance += player.Hands[i].bet*2;
                    player.Hands[i].bet = 0;
                    MessageBox.Show($"{player.Name} won against the dealer and doubled their bet.");
                }
                else if (playerValue == dealerValue)
                {
                    player.Balance += player.Hands[i].bet;
                    player.Hands[i].bet = 0;
                    MessageBox.Show($"{player.Name} pushed with the dealer and got their bet back.");
                }
                else
                {
                    //player loses bet
                    player.Hands[i].bet = 0;
                    MessageBox.Show($"{player.Name} lost against the dealer and lost their bet.");
                }
                if (player.Balance <= 0)
                {
                    broke.Add(player as Blackjackplayer);

                }
            }
            foreach(var hand in player.Hands)
            {
                hand.bet = 0;
            }

        }
        foreach(var player in broke)
        {
            Players.Remove(player);
        }

    }
    public bool checkblackjack(Blackjackplayer player)
    {
        return player.Hands[0].getHandValue() == 21;
    }

    public void DealInitialCards()
    {
        foreach (var player in Players)
        {

            GameDeck.DealBlackjack(player);
            GameDeck.DealBlackjack(player);


        }
    }
    public void DealDealerCards()

    {
        Player.Hand newHand = new Player.Hand(new List<Cards>(), 0);
        dealer.Hands.Add(newHand);
        GameDeck.Deal(dealer);
        GameDeck.Deal(dealer);
    }
    public void DealerPlay()
    {
        while (dealer.Hands[0].getHandValue() < 17)
        {
            GameDeck.Deal(dealer);
        }
    }
    public void Split()
    {
        Blackjackplayer player = Players[currentPlayer ] as Blackjackplayer;
        int handNum = player.currentHand;
        if ((player.Hands[handNum].Cards.Count== 2)&&(player.Hands[handNum].Cards[0].Rank == player.Hands[handNum].Cards[1].Rank))
        {

            Cards temp = player.Hands[handNum].Cards[1];
            player.Hands[handNum].Cards.RemoveAt(1);
            List<Cards> temp2 = new List<Cards> { temp }; //hopefully this works
            //umesto add pozvati constructor
            Player.Hand newHand = new Player.Hand(temp2, player.Hands[player.currentHand].bet);
            player.Balance -= player.Hands[player.currentHand].bet; //deduct bet for new hand
            player.Hands.Add(newHand);

            GameDeck.DealBlackjack(player);
            player.currentHand++; //have to do this as the deal functions only affects current hand
            GameDeck.DealBlackjack(player);
            player.currentHand--;

        }
    }
    public void Hit()
    {
        Blackjackplayer player = Players[currentPlayer ] as Blackjackplayer;//nakon betting runde currentplayer je na 1 a treba da je na 0
        GameDeck.DealBlackjack(player);
        if (player.Hands[player.currentHand].getHandValue()>21)
        {
            dealer.Balance += player.Hands[player.currentHand].bet; //fix
            player.Hands[player.currentHand].bet = 0;
            player.currentHand++;
            if (player.IsDone())
            {
                currentPlayer++;
                //if (isRoundOver())
                //{
                //    resolveRound();

                //}

            }

        }
    }
    public void Stand()
    {
        Blackjackplayer player = Players[currentPlayer] as Blackjackplayer;
        player.currentHand++;
        if (player.IsDone())
        {
            currentPlayer++;
            //if (isRoundOver())
            //{
            //    resolveRound();
            //}

        }
       // player.currentHand--;
    }
    public void DoubleDown()
    {
        Blackjackplayer player = Players[currentPlayer ] as Blackjackplayer;
        if (player.Balance >= player.Hands[player.currentHand].bet)
        {
            player.Hands[player.currentHand].bet *= 2;
            player.Balance -= player.Hands[player.currentHand].bet/2;

            GameDeck.DealBlackjack(player);
            if (player.Hands[player.currentHand].getHandValue()>21)
            {
                dealer.Balance += player.Hands[player.currentHand].bet;
                player.Hands[player.currentHand].bet = 0;
                player.currentHand++;
                if (player.IsDone())//check for error
                {
                    currentPlayer++;
                }
            }
            else
            {
                Stand();
            }

        }
        else
        {
            throw new Exception("Insufficient balance to double down.");
        }

    }
}
public class Pokerplayer
{



}
public class Blackjackplayer(string name, float balance) : Player(name, balance)
{
}
public class BlackjackDealer : Blackjackplayer
{
    public BlackjackDealer() : base("Dealer", 10000)
    {
    }



}




