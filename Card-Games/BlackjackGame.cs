//popraviti resolve da sve lepo resi
namespace Card_Games;

public class BlackjackGame(List<Player> players, int numberOfDecks, BlackjackDealer dealer) : Game(players, numberOfDecks)
{
    public List<Player> Broke { get; set; } = [];
    public BlackjackDealer Dealer { get; set; } = dealer;

    public void Start1()
    {
        CurrentPlayer = 0;
        foreach (var player in Players)
        {
            GameDeck.ClearPlayer(player);
            player.CurrentHand = 0;
            Player.Hand newHand = new Player.Hand([], 0);
            player.Hands.Add(newHand);
        }
        GameDeck.Shuffle();
        Dealer.Hands.Clear();
        DealDealerCards();

        CurrentPlayer = 0;
    }
    public bool IsRoundOver() => CurrentPlayer >= Players.Count;
    public void ResolveRound()
    {
        DealerPlay();
        int dealerValue = Dealer.Hands[0].GetHandValue();
        foreach(var player in Players)
        {
            for(int i = 0; i< player.Hands.Count; i++)
            {
                int playerValue = (player as Player).Hands[i].GetHandValue();
                if (playerValue > 21)
                {
                    player.Hands[i].Bet = 0;

                }
                else if (dealerValue>21)
                {
                    player.Balance += player.Hands[i].Bet*2;
                    player.Hands[i].Bet = 0;

                }
                else if(playerValue > dealerValue)
                {
                    player.Balance += player.Hands[i].Bet*2;
                    player.Hands[i].Bet = 0;

                }
                else if (playerValue == dealerValue)
                {
                    player.Balance += player.Hands[i].Bet;
                    player.Hands[i].Bet = 0;

                }
                else
                {
                    player.Hands[i].Bet = 0;

                }
                if (player.Balance <= 0)
                {
                    Broke.Add(player as Player);

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
    public bool CheckBlackJack(Player player) => player.Hands[0].GetHandValue() == 21;

    public void DealInitialCards()
    {
        foreach (var player in Players)
        {
            GameDeck.Deal(player);
            GameDeck.Deal(player);
        }
    }
    public void DealDealerCards()//ovo je nepotrebno skloniti
    {
        Player.Hand newHand = new Player.Hand([], 0);
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
        Player player = Players[CurrentPlayer ] as Player;
        int handNum = player.CurrentHand;
        if ((player.Hands[handNum].Cards.Count== 2)&&(player.Hands[handNum].Cards[0].Rank == player.Hands[handNum].Cards[1].Rank))
        {

            Cards temp = player.Hands[handNum].Cards[1];
            player.Hands[handNum].Cards.RemoveAt(1);
            List<Cards> temp2 = [temp];
            Player.Hand newHand = new Player.Hand(temp2, player.Hands[player.CurrentHand].Bet);
            player.Balance -= player.Hands[player.CurrentHand].Bet; //deduct bet for new hand
            player.Hands.Add(newHand);

            GameDeck.Deal(player);
            player.CurrentHand++; //have to do this as the deal functions only affects current hand
            GameDeck.Deal(player);
            player.CurrentHand--;

        }
    }
    public void Hit()
    {
        Player player = Players[CurrentPlayer ];
        GameDeck.Deal(player);
        if (player.Hands[player.CurrentHand].GetHandValue()>21)
        {
            Dealer.Balance += player.Hands[player.CurrentHand].Bet;
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
        Player player = Players[CurrentPlayer];
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
        Player player = Players[CurrentPlayer ];
        if (player.Balance >= player.Hands[player.CurrentHand].Bet)
        {
            player.Hands[player.CurrentHand].Bet *= 2;
            player.Balance -= player.Hands[player.CurrentHand].Bet/2;

            GameDeck.Deal(player);
            if (player.Hands[player.CurrentHand].GetHandValue()>21)
            {
                Dealer.Balance += player.Hands[player.CurrentHand].Bet;
                player.Hands[player.CurrentHand].Bet = 0;
                player.CurrentHand++;
                if (player.IsDone())
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

