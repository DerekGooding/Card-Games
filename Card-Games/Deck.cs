//popraviti resolve da sve lepo resi
namespace Card_Games;

public class Deck
{
    public int NumberOfDecks { get; set; }
    public List<Cards> CardsAll { get; set; } = [];
    public Deck(int numberOfDecks)
    {
        string[] ranks = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"];
        string[] suits = ["Hearts", "Diamonds", "Clubs", "Spades"];
        this.NumberOfDecks = numberOfDecks;
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
    public List<Cards> UsedCards { get; set; } = [];
    public void Shuffle()
    {
        for (int i = CardsAll.Count - 1; i > 0; i--)
        {
            int j = Random.Shared.Next(0, i + 1);
            (CardsAll[j], CardsAll[i]) = (CardsAll[i], CardsAll[j]);
        }
    }//fiser yates algoritam
    public void Deal(Player player)
    {
        if (CardsAll.Count < 51)
        {
            for (int i = 0; i < UsedCards.Count; i++)
            {
                CardsAll.Add(UsedCards[i]);
                UsedCards.RemoveAt(i);
            }
        }
        player.Hands[player.CurrentHand].Cards.Add(CardsAll[0]);
        UsedCards.Add(CardsAll[0]);
        CardsAll.RemoveAt(0);
    }

    public void ClearPlayer(Player player)
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

