namespace Poker.Model;

public class Hand(List<Card> karte, float bet)
{
    public List<Card> Cards { get; set; } = karte;
    public float Bet { get; set; } = bet;
    public int GetHandValue()
    {
        var totalValue = Cards.Sum(CardValue);
        var aceCount = Cards.Count(x => x.Rank == "A");

        while (totalValue > 21 && aceCount > 0)
        {
            totalValue -= 10;
            aceCount--;
        }
        return totalValue;
    }

    private static int CardValue(Card card) => card.Rank switch
    {
        "A" => 11,
        "K" or "Q" or "J" => 10,
        _ => int.Parse(card.Rank)
    };
}
