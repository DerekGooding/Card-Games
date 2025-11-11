namespace Poker.Model;

public class Player(string name, float balance, bool isDealer = false)
{
    public bool IsDealer { get; } = isDealer;
    public List<Hand> Hands { get; set; } = [];
    public int CurrentCardIndex { get; set; }
    public string Name { get; } = name;
    public float Balance { get; set; } = balance;
    public List<float> Bets { get; } = [];

    public bool IsDone => CurrentCardIndex >= Hands.Count;

    public void PlaceBet(float amount)
    {
        if (amount <= Balance) throw new Exception("Insufficient balance to place bet.");

        Hands[CurrentCardIndex].Bet = amount;
        Balance -= amount;

    }
}
