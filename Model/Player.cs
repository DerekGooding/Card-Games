namespace Poker.Model;

public class Player(PlayerData playerData)
{
    public List<Hand> Hands { get; set; } = [];
    public int CurrentHandIndex { get; set; }
    public string Name { get; } = playerData.Name;
    public float Balance { get; set; } = playerData.Balance;
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
