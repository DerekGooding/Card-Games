namespace Poker.Model;

public readonly record struct Card(string Rank, string Suit)
{
    public readonly string ImageName => $"{Rank}{Suit}.png";
}
