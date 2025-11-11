namespace Poker;

public class Player(string name, float balance)
{
    public string Name { get; set; } = name;
    public float Balance { get; set; } = balance;

    public List<float> Bets { get; set; } = [];
    public class Hand(List<Cards> karte, float bet)
    {
        public List<Cards> Cards { get; set; } = karte;
        public float Bet { get; set; } = bet;
        public int GetHandValue(bool isAceHigh = true)
        {
            var totalValue = 0;
            var aceCount = 0;
            foreach (var card in Cards)
            {
                if (card.Rank == "A")
                {
                    aceCount++;
                    totalValue += 11;
                }
                else
                {
                    totalValue += card.Value(isAceHigh);
                }
            }

            // Handle aces as 1 if needed to avoid busting
            while (totalValue > 21 && aceCount > 0)
            {
                totalValue -= 10;
                aceCount--;
            }
            return totalValue;
        }
    }

    public List<Hand> Hands { get; set; } = [];
    public int CurrentHand { get; set; } = 0;
    public bool IsDone() => CurrentHand >= Hands.Count;//ne radi????
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
