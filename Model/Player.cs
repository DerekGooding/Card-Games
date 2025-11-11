namespace Poker.Model;

public class Player(string name, float balance)
{
    public List<Hand> Hands { get; set; } = [];
    public int CurrentHand { get; set; }
    public string Name { get; set; } = name;
    public float Balance { get; set; } = balance;
    public List<float> Bets { get; set; } = [];

    public bool IsDone() => CurrentHand >= Hands.Count;

    public void PlaceBet(float amount)
    {
        if (amount <= Balance)
        {
            Hands[CurrentHand].Bet = amount;
            Balance -= amount;
        }
        else
        {
            throw new Exception("Insufficient balance to place bet.");
        }
    }
}
