namespace Poker.Model;

public class Hand(List<Card> karte, float bet)
{
    public List<Card> Cards { get; set; } = karte;
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

        while (totalValue > 21 && aceCount > 0)
        {
            totalValue -= 10;
            aceCount--;
        }
        return totalValue;
    }
}
