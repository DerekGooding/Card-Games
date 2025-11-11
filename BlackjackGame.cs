namespace Poker;

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
        foreach (var player in Players)
        {
            for (var i = 0; i < player.Hands.Count; i++)
            {
                if (player is not Blackjackplayer blackjackplayer)
                    continue;
                var playerValue = blackjackplayer.Hands[i].GetHandValue();
                if (playerValue > 21)
                {
                    player.Hands[i].Bet = 0;
                    MessageBox.Show($"{player.Name} busted and lost their bet.");
                }
                else if (dealerValue > 21)
                {
                    player.Balance += player.Hands[i].Bet * 2;
                    player.Hands[i].Bet = 0;
                    MessageBox.Show($"{player.Name} won as the dealer busted and doubled their bet.");
                }
                else if (playerValue > dealerValue)
                {
                    player.Balance += player.Hands[i].Bet * 2;
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
                    Broke.Add(blackjackplayer);

                }
            }
            foreach (var hand in player.Hands)
            {
                hand.Bet = 0;
            }

        }
        foreach (var player in Broke)
        {
            Players.Remove(player);
        }

    }
    //public bool Checkblackjack(Blackjackplayer player) => player.Hands[0].GetHandValue() == 21;

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
        if (Players[CurrentPlayer] is not Blackjackplayer player)
            return;
        var handNum = player.CurrentHand;
        if ((player.Hands[handNum].Cards.Count == 2) && (player.Hands[handNum].Cards[0].Rank == player.Hands[handNum].Cards[1].Rank))
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
        if (Players[CurrentPlayer] is not Blackjackplayer player)
            return;
        GameDeck.DealBlackjack(player);
        if (player.Hands[player.CurrentHand].GetHandValue() > 21)
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
        if (Players[CurrentPlayer] is not Blackjackplayer player)
            return;
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
        if (Players[CurrentPlayer] is not Blackjackplayer player)
            return;
        if (player.Balance >= player.Hands[player.CurrentHand].Bet)
        {
            player.Hands[player.CurrentHand].Bet *= 2;
            player.Balance -= player.Hands[player.CurrentHand].Bet / 2;

            GameDeck.DealBlackjack(player);
            if (player.Hands[player.CurrentHand].GetHandValue() > 21)
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




