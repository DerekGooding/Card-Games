namespace Poker;

public class Player(string name, float balance)
{
    public string Name { get; set; } = name;
    public float Balance { get; set; } = balance;

    public List<float> Bets { get; set; } = [];
    public class Hand(List<Cards> karte, float bet)
    {
        public List<Cards> Cards { get; set; } = karte;
        public float Bet { get; set; } = bet;
        public int GetHandValue(bool isAceHigh = true)
        {
            var totalValue = 0;
            var aceCount = 0;
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

    public List<Hand> Hands { get; set; } = [];
    public int CurrentHand { get; set; } = 0;
    public bool IsDone() => CurrentHand >= Hands.Count;//ne radi????
    public void PlaceBet(float amount)
    {
        if (amount <= Balance)
        {
            Hands[CurrentHand].Bet = amount;
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

    public int Value(bool isAceHigh) => Rank == "A" ? isAceHigh ? 11 : 1 : Rank is "K" or "Q" or "J" ? 10 : int.Parse(Rank);

}
public class Deck
{
    public int NumberOfDecks { get; set; }
    public List<Cards> CardsAll { get; set; } = [];
    public Deck(int numberOfDecks)
    {
        string[] ranks = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"];
        string[] suits = ["Hearts", "Diamonds", "Clubs", "Spades"];
        NumberOfDecks = numberOfDecks;
        for (var i = 0; i< numberOfDecks; i++)
        {
            foreach (var rank in ranks)
            {
                foreach (var suit in suits)
                {
                    CardsAll.Add(new Cards(rank, suit));
                }
            }
        }
    }
    public List<Cards> UsedCards { get; set; } = [];
    public void Shuffle()
    {
        var rand = new Random();
        for (var i = CardsAll.Count - 1; i > 0; i--)
        {
            var j = rand.Next(0, i + 1);
            (CardsAll[j], CardsAll[i]) = (CardsAll[i], CardsAll[j]);
        }
    }
    public void Deal(Player player)
    {
        if (CardsAll.Count < 15)
        {
            for (var i = 0; i < UsedCards.Count; i++)
            {
                CardsAll.Add(UsedCards[i]);
                UsedCards.RemoveAt(i);
            }
        }
        player.Hands[player.CurrentHand].Cards.Add(CardsAll[0]);
        UsedCards.Add(CardsAll[0]);
        CardsAll.RemoveAt(0);
    }
    public void DealBlackjack(Player player)
    {
        var handnum = player.CurrentHand;
        if (CardsAll.Count < 15)
        {
            for (var i = 0; i < UsedCards.Count; i++)
            {
                CardsAll.Add(UsedCards[i]);
                UsedCards.RemoveAt(i);
            }
        }
        player.Hands[handnum].Cards.Add(CardsAll[0]);
        UsedCards.Add(CardsAll[0]);
        CardsAll.RemoveAt(0);
    }
    public void ClearPlayer(Player player)
    {
        if(player.Name=="Dealer")
        {
            for (var i = 0; i<player.Hands[player.CurrentHand].Cards.Count; i++)
            {
                UsedCards.Add(player.Hands[player.CurrentHand].Cards[i]);
            }
            player.Hands[player.CurrentHand].Cards.Clear();
            return;
        }
        else
        {
            for (var i = 0; i<player.Hands.Count; i++)
            {
                for (var j = 0; j<player.Hands[i].Cards.Count; j++)
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
    public int CurrentPlayer { get; set; } = 0;
    public List<Player> Players { get; set; } = [];
    public Deck GameDeck { get; set; }
    public int NumberOfDecks { get; set; }
    public Game(List<Player> players, int numberOfDecks)
    {
        Players=players;
        NumberOfDecks = numberOfDecks;
        GameDeck = new Deck(numberOfDecks);
        GameDeck.Shuffle();
        CurrentPlayer = 0;
    }

}
public class BlackjackGame(List<Player> players, int numberOfDecks, BlackjackDealer dealer) : Game(players, numberOfDecks)
{

    public List<Blackjackplayer> Broke { get; set; } = [];
    //create a secondary list of players that are not busted or have blackjack
    public BlackjackDealer Dealer { get; set; } = dealer;

    //have to add function for starting a new round, dealer logic, busting, payout 

    public void Start1()
    {
        CurrentPlayer = 0;
        foreach (var player in Players)
        {
            GameDeck.ClearPlayer(player);
            player.CurrentHand = 0;
            var newHand = new Player.Hand([], 0);
            player.Hands.Add(newHand);
        }
        GameDeck.Shuffle();
        Dealer.Hands.Clear();
        DealDealerCards();

        CurrentPlayer = 0;
    }
    public void Start2() => DealInitialCards();//foreach (var player in Players)//{//    if (checkblackjack(player as Blackjackplayer))//    {//        player.Balance += player.Hands[player.currentHand].bet * 2.5f;//fix//        player.Hands[player.currentHand].bet = 0;//        currentPlayer++;//    }//}

    public bool IsRoundOver() => CurrentPlayer >= Players.Count;
    public void ResolveRound()//ne vodi svaki korak do resetovanja igre pa zbaga
    {
        DealerPlay();
        var dealerValue = Dealer.Hands[0].GetHandValue();
        foreach(var player in Players)
        {
            for(var i = 0; i< player.Hands.Count; i++)
            {
                var playerValue = (player as Blackjackplayer).Hands[i].GetHandValue();
                if (playerValue > 21)
                {
                    player.Hands[i].Bet = 0;
                    MessageBox.Show($"{player.Name} busted and lost their bet.");
                }
                else if (dealerValue>21)
                {
                    player.Balance += player.Hands[i].Bet*2;
                    player.Hands[i].Bet = 0;
                    MessageBox.Show($"{player.Name} won as the dealer busted and doubled their bet.");
                }
                else if(playerValue > dealerValue)
                {
                    player.Balance += player.Hands[i].Bet*2;
                    player.Hands[i].Bet = 0;
                    MessageBox.Show($"{player.Name} won against the dealer and doubled their bet.");
                }
                else if (playerValue == dealerValue)
                {
                    player.Balance += player.Hands[i].Bet;
                    player.Hands[i].Bet = 0;
                    MessageBox.Show($"{player.Name} pushed with the dealer and got their bet back.");
                }
                else
                {
                    //player loses bet
                    player.Hands[i].Bet = 0;
                    MessageBox.Show($"{player.Name} lost against the dealer and lost their bet.");
                }
                if (player.Balance <= 0)
                {
                    Broke.Add(player as Blackjackplayer);

                }
            }
            foreach(var hand in player.Hands)
            {
                hand.Bet = 0;
            }

        }
        foreach(var player in Broke)
        {
            Players.Remove(player);
        }

    }
    public bool Checkblackjack(Blackjackplayer player) => player.Hands[0].GetHandValue() == 21;

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
        var newHand = new Player.Hand([], 0);
        Dealer.Hands.Add(newHand);
        GameDeck.Deal(Dealer);
        GameDeck.Deal(Dealer);
    }
    public void DealerPlay()
    {
        while (Dealer.Hands[0].GetHandValue() < 17)
        {
            GameDeck.Deal(Dealer);
        }
    }
    public void Split()
    {
        var player = Players[CurrentPlayer ] as Blackjackplayer;
        var handNum = player.CurrentHand;
        if ((player.Hands[handNum].Cards.Count== 2)&&(player.Hands[handNum].Cards[0].Rank == player.Hands[handNum].Cards[1].Rank))
        {

            var temp = player.Hands[handNum].Cards[1];
            player.Hands[handNum].Cards.RemoveAt(1);
            List<Cards> temp2 = [temp]; //hopefully this works
            //umesto add pozvati constructor
            var newHand = new Player.Hand(temp2, player.Hands[player.CurrentHand].Bet);
            player.Balance -= player.Hands[player.CurrentHand].Bet; //deduct bet for new hand
            player.Hands.Add(newHand);

            GameDeck.DealBlackjack(player);
            player.CurrentHand++; //have to do this as the deal functions only affects current hand
            GameDeck.DealBlackjack(player);
            player.CurrentHand--;

        }
    }
    public void Hit()
    {
        var player = Players[CurrentPlayer ] as Blackjackplayer;//nakon betting runde currentplayer je na 1 a treba da je na 0
        GameDeck.DealBlackjack(player);
        if (player.Hands[player.CurrentHand].GetHandValue()>21)
        {
            Dealer.Balance += player.Hands[player.CurrentHand].Bet; //fix
            player.Hands[player.CurrentHand].Bet = 0;
            player.CurrentHand++;
            if (player.IsDone())
            {
                CurrentPlayer++;
                //if (isRoundOver())
                //{
                //    resolveRound();

                //}

            }

        }
    }
    public void Stand()
    {
        var player = Players[CurrentPlayer] as Blackjackplayer;
        player.CurrentHand++;
        if (player.IsDone())
        {
            CurrentPlayer++;
            //if (isRoundOver())
            //{
            //    resolveRound();
            //}

        }
       // player.currentHand--;
    }
    public void DoubleDown()
    {
        var player = Players[CurrentPlayer ] as Blackjackplayer;
        if (player.Balance >= player.Hands[player.CurrentHand].Bet)
        {
            player.Hands[player.CurrentHand].Bet *= 2;
            player.Balance -= player.Hands[player.CurrentHand].Bet/2;

            GameDeck.DealBlackjack(player);
            if (player.Hands[player.CurrentHand].GetHandValue()>21)
            {
                Dealer.Balance += player.Hands[player.CurrentHand].Bet;
                player.Hands[player.CurrentHand].Bet = 0;
                player.CurrentHand++;
                if (player.IsDone())//check for error
                {
                    CurrentPlayer++;
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




