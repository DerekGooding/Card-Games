namespace Poker.Model;

public class Game
{
    public int CurrentPlayer { get; set; } = 0;
    public List<Player> Players { get; set; } = [];
    public Deck GameDeck { get; set; }
    public int NumberOfDecks { get; set; }
    public Game(List<Player> players, int numberOfDecks)
    {
        Players = players;
        NumberOfDecks = numberOfDecks;
        GameDeck = new Deck(numberOfDecks);
        GameDeck.Shuffle();
        CurrentPlayer = 0;
    }

}




