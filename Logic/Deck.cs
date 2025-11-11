using Poker.Model;

namespace Poker.Logic;

public class Deck(int numberOfDecks)
{
    private readonly Random _random = Random.Shared;
    private static readonly string[] _ranks = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"];
    private static readonly string[] _suits = ["Hearts", "Diamonds", "Clubs", "Spades"];

    public List<Card> CardsAll { get; } =
        [.. _ranks.SelectMany(r => _suits.SelectMany(s => Enumerable.Repeat(new Card(r, s), numberOfDecks)))];
    public List<Card> UsedCards { get; } = [];

    public void Shuffle()
    {
        for (var i = CardsAll.Count - 1; i > 0; i--)
        {
            var j = _random.Next(0, i + 1);
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
        player.Hands[player.CurrentHandIndex].Cards.Add(CardsAll[0]);
        UsedCards.Add(CardsAll[0]);
        CardsAll.RemoveAt(0);
    }

    public void DealBlackjack(Player player)
    {
        var handnum = player.CurrentHandIndex;
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
        if (player.IsDealer)
        {
            UsedCards.AddRange(player.CurrentHand.Cards);
            player.CurrentHand.Cards.Clear();
        }
        else
        {
            for (var i = 0; i < player.Hands.Count; i++)
            {
                for (var j = 0; j < player.Hands[i].Cards.Count; j++)
                {
                    UsedCards.Add(player.Hands[i].Cards[j]);
                }
            }
            player.Hands.Clear();
        }

    }
}




