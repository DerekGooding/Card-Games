using Poker.Model;

namespace Poker.Logic;

public class BlackjackGame
{
    public int CurrentPlayerIndex { get; set; }
    public List<Player> Players { get; set; } = [];
    public Deck GameDeck { get; set; }
    public int NumberOfDecks { get; set; }
    public List<Player> Broke { get; set; } = [];
    public Player Dealer { get; set; }
    private Player CurrentPlayer => Players[CurrentPlayerIndex].IsDealer
        ? throw new Exception("Dealer cannot be current player")
        : Players[CurrentPlayerIndex];

    public BlackjackGame(List<Player> players, int numberOfDecks, Player dealer)
    {
        Dealer = dealer;
        Players = players;
        NumberOfDecks = numberOfDecks;
        GameDeck = new Deck(numberOfDecks);
        GameDeck.Shuffle();
        CurrentPlayerIndex = 0;
    }

    public void Start1()
    {
        CurrentPlayerIndex = 0;
        foreach (var player in Players)
        {
            GameDeck.ClearPlayer(player);
            player.CurrentCardIndex = 0;
            var newHand = new Hand([], 0);
            player.Hands.Add(newHand);
        }
        GameDeck.Shuffle();
        Dealer.Hands.Clear();
        DealDealerCards();

        CurrentPlayerIndex = 0;
    }
    public void Start2() => DealInitialCards();
    public bool IsRoundOver() => CurrentPlayerIndex >= Players.Count;

    public void ResolveRound()
    {
        DealerPlay();
        var dealerValue = Dealer.Hands[0].GetHandValue();
        foreach (var player in Players)
        {
            for (var i = 0; i < player.Hands.Count; i++)
            {
                if (player.IsDealer)
                    continue;
                var playerValue = player.Hands[i].GetHandValue();
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
                    Broke.Add(player);
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
        var newHand = new Hand([], 0);
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
        var player = CurrentPlayer;
        var handNum = player.CurrentCardIndex;
        if (player.Hands[handNum].Cards.Count == 2 && player.Hands[handNum].Cards[0].Rank == player.Hands[handNum].Cards[1].Rank)
        {

            var temp = player.Hands[handNum].Cards[1];
            player.Hands[handNum].Cards.RemoveAt(1);
            List<Card> temp2 = [temp]; //hopefully this works
            //umesto add pozvati constructor
            var newHand = new Hand(temp2, player.Hands[player.CurrentCardIndex].Bet);
            player.Balance -= player.Hands[player.CurrentCardIndex].Bet; //deduct bet for new hand
            player.Hands.Add(newHand);

            GameDeck.DealBlackjack(player);
            player.CurrentCardIndex++; //have to do this as the deal functions only affects current hand
            GameDeck.DealBlackjack(player);
            player.CurrentCardIndex--;

        }
    }
    public void Hit()
    {
        var player = CurrentPlayer;
        GameDeck.DealBlackjack(player);
        if (player.Hands[player.CurrentCardIndex].GetHandValue() > 21)
        {
            Dealer.Balance += player.Hands[player.CurrentCardIndex].Bet; //fix
            player.Hands[player.CurrentCardIndex].Bet = 0;
            player.CurrentCardIndex++;
            if (player.IsDone)
                CurrentPlayerIndex++;

        }
    }
    public void Stand()
    {
        var player = CurrentPlayer;
        player.CurrentCardIndex++;
        if (player.IsDone)
            CurrentPlayerIndex++;
        // player.currentHand--;
    }
    public void DoubleDown()
    {
        var player = CurrentPlayer;
        if (player.Balance >= player.Hands[player.CurrentCardIndex].Bet)
        {
            player.Hands[player.CurrentCardIndex].Bet *= 2;
            player.Balance -= player.Hands[player.CurrentCardIndex].Bet / 2;

            GameDeck.DealBlackjack(player);
            if (player.Hands[player.CurrentCardIndex].GetHandValue() > 21)
            {
                Dealer.Balance += player.Hands[player.CurrentCardIndex].Bet;
                player.Hands[player.CurrentCardIndex].Bet = 0;
                player.CurrentCardIndex++;
                if (player.IsDone)
                    CurrentPlayerIndex++;
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




