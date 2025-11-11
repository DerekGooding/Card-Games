using Poker.Model;

namespace Poker.Logic;

public class BlackjackGame(List<Player> players, int numberOfDecks, Dealer dealer)
{
    public int CurrentPlayerIndex { get; set; }
    public List<Player> Players { get; set; } = players;
    public Deck Deck { get; set; } = new Deck(numberOfDecks);
    public List<Player> BrokePlayers { get; set; } = [];
    public Dealer Dealer { get; set; } = dealer;
    public bool IsRoundOver => CurrentPlayerIndex >= Players.Count;

    private Player CurrentPlayer => Players[CurrentPlayerIndex];

    public void StartInitial()
    {
        CurrentPlayerIndex = 0;
        foreach (var player in Players)
        {
            Deck.ClearPlayer(player);
            player.CurrentHandIndex = 0;
            var newHand = new Hand([], 0);
            player.Hands.Add(newHand);
        }
        Deck.Shuffle();
        Dealer.Hands.Clear();
        DealDealerCards();

        CurrentPlayerIndex = 0;
    }
    public void StartAdditional() => DealInitialCards();

    public void ResolveRound()
    {
        DealerPlay();
        var dealerValue = Dealer.Hands[0].GetHandValue();
        foreach (var player in Players)
        {
            for (var i = 0; i < player.Hands.Count; i++)
            {
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
                    player.Hands[i].Bet = 0;
                    MessageBox.Show($"{player.Name} lost against the dealer and lost their bet.");
                }
                if (player.Balance <= 0)
                    BrokePlayers.Add(player);
            }
            foreach (var hand in player.Hands)
            {
                hand.Bet = 0;
            }

        }
        foreach (var player in BrokePlayers)
        {
            Players.Remove(player);
        }

    }

    public void DealInitialCards()
    {
        foreach (var player in Players)
        {
            Deck.DealCard(player);
            Deck.DealCard(player);
        }
    }

    public void DealDealerCards()
    {
        var newHand = new Hand([], 0);
        Dealer.Hands.Add(newHand);
        Deck.DealCard(Dealer);
        Deck.DealCard(Dealer);
    }

    public void DealerPlay()
    {
        while (Dealer.Hands[0].GetHandValue() < 17)
        {
            Deck.DealCard(Dealer);
        }
    }

    public void Split()
    {
        var player = CurrentPlayer;
        var handNum = player.CurrentHandIndex;
        if (player.Hands[handNum].Cards.Count == 2 && player.Hands[handNum].Cards[0].Rank == player.Hands[handNum].Cards[1].Rank)
        {

            var temp = player.Hands[handNum].Cards[1];
            player.Hands[handNum].Cards.RemoveAt(1);
            List<Card> temp2 = [temp]; //hopefully this works
            //umesto add pozvati constructor
            var newHand = new Hand(temp2, player.Hands[player.CurrentHandIndex].Bet);
            player.Balance -= player.Hands[player.CurrentHandIndex].Bet; //deduct bet for new hand
            player.Hands.Add(newHand);

            Deck.DealCard(player);
            player.CurrentHandIndex++; //have to do this as the deal functions only affects current hand
            Deck.DealCard(player);
            player.CurrentHandIndex--;

        }
    }
    public void Hit()
    {
        var player = CurrentPlayer;
        Deck.DealCard(player);
        if (player.Hands[player.CurrentHandIndex].GetHandValue() > 21)
        {
            Dealer.Balance += player.Hands[player.CurrentHandIndex].Bet; //fix
            player.Hands[player.CurrentHandIndex].Bet = 0;
            player.CurrentHandIndex++;
            if (player.IsDone)
                CurrentPlayerIndex++;

        }
    }
    public void Stand()
    {
        var player = CurrentPlayer;
        player.CurrentHandIndex++;
        if (player.IsDone)
            CurrentPlayerIndex++;
        // player.currentHand--;
    }
    public void DoubleDown()
    {
        var player = CurrentPlayer;
        if (player.Balance >= player.Hands[player.CurrentHandIndex].Bet)
        {
            player.Hands[player.CurrentHandIndex].Bet *= 2;
            player.Balance -= player.Hands[player.CurrentHandIndex].Bet / 2;

            Deck.DealCard(player);
            if (player.Hands[player.CurrentHandIndex].GetHandValue() > 21)
            {
                Dealer.Balance += player.Hands[player.CurrentHandIndex].Bet;
                player.Hands[player.CurrentHandIndex].Bet = 0;
                player.CurrentHandIndex++;
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




