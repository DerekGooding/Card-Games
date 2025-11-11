namespace Poker.Model;

public readonly record struct Card(string Rank, string Suit)
{
    public int Value(bool isAceHigh) => Rank == "A" ? isAceHigh ? 11 : 1 : Rank is "K" or "Q" or "J" ? 10 : int.Parse(Rank);
    public readonly string ImageName => $"{Rank}{Suit}.png";
}
