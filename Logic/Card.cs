namespace Poker.Logic;

public readonly record struct Card(string Rank, string Suit)
{
    public int Value(bool isAceHigh) => Rank == "A" ? isAceHigh ? 11 : 1 : Rank is "K" or "Q" or "J" ? 10 : int.Parse(Rank);
}




