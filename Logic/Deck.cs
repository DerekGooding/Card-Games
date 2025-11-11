using Poker.Model;

namespace Poker.Logic;

public class Deck
{
    private readonly Random _random = Random.Shared;
    private static readonly string[] _ranks = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"];
    private static readonly string[] _suits = ["Hearts", "Diamonds", "Clubs", "Spades"];

    public Deck(int numberOfDecks)
    {
        CardsAll =
        [.. _ranks.SelectMany(r => _suits.SelectMany(s => Enumerable.Repeat(new Card(r, s), numberOfDecks)))];
        Shuffle();
    }

    public List<Card> CardsAll { get; }
    public List<Card> UsedCards { get; } = [];

    //Fisher-Yates
    public void Shuffle()
    {
        for (var i = CardsAll.Count - 1; i > 0; i--)
        {
            var j = _random.Next(0, i + 1);
            (CardsAll[j], CardsAll[i]) = (CardsAll[i], CardsAll[j]);
        }
    }

    public void DealCard(Dealer dealer)
    {
        if (CardsAll.Count < 15)
        {
            for (var i = 0; i < UsedCards.Count; i++)
            {
                CardsAll.Add(UsedCards[i]);
                UsedCards.RemoveAt(i);
            }
        }
        dealer.CurrentHand.Cards.Add(CardsAll[0]);
        UsedCards.Add(CardsAll[0]);
        CardsAll.RemoveAt(0);
    }

    public void DealCard(Player player)
    {
        if (CardsAll.Count < 15)
        {
            for (var i = 0; i < UsedCards.Count; i++)
            {
                CardsAll.Add(UsedCards[i]);
                UsedCards.RemoveAt(i);
            }
        }
        player.CurrentHand.Cards.Add(CardsAll[0]);
        UsedCards.Add(CardsAll[0]);
        CardsAll.RemoveAt(0);
    }

    public void ClearPlayer(Player player)
    {
        UsedCards.AddRange(player.Hands.SelectMany(x => x.Cards));
        player.Hands.Clear();
    }

    public void ClearDealer(Dealer dealer)
    {
        UsedCards.AddRange(dealer.CurrentHand.Cards);
        dealer.CurrentHand.Cards.Clear();
    }
}
