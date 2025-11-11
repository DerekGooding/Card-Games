namespace Poker;

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




