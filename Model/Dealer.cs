namespace Poker.Model;

public class Dealer
{
    public List<Hand> Hands { get; set; } = [];
    public int CurrentHandIndex { get; set; }
    public string Name { get; } = "Dealer";
    public float Balance { get; set; } = 10000;
    public List<float> Bets { get; } = [];

    public Hand CurrentHand => Hands[CurrentHandIndex];
    public bool IsDone => CurrentHandIndex >= Hands.Count;
}
