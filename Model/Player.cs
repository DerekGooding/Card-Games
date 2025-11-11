namespace Poker.Model;

public class Player(string name, float balance)
{
    public List<Hand> Hands { get; set; } = [];
    public int CurrentHandIndex { get; set; }
    public string Name { get; } = name;
    public float Balance { get; set; } = balance;
    public List<float> Bets { get; } = [];

    public Hand CurrentHand => Hands[CurrentHandIndex];
    public bool IsDone => CurrentHandIndex >= Hands.Count;

    public void PlaceBet(float amount)
    {
        if (amount <= Balance) throw new Exception("Insufficient balance to place bet.");

        Hands[CurrentHandIndex].Bet = amount;
        Balance -= amount;

    }
}
