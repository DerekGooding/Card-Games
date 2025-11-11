namespace Poker;

public class Cards(string cardRank, string cardSuit)
{
    public string Rank { get; set; } = cardRank;
    public string CardSuit { get; set; } = cardSuit;

    public int Value(bool isAceHigh) => Rank == "A" ? isAceHigh ? 11 : 1 : Rank is "K" or "Q" or "J" ? 10 : int.Parse(Rank);

}




