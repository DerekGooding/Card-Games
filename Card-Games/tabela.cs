namespace Card_Games;

public class StrategyEntry
{
    public StrategyEntry() { }
    public string HandType { get; set; }
    public int HandValue { get; set; }
    public int DealerUpcard { get; set; }
    public double WinsHit = 0;
    public double LosesHit = 0;
    public double DrawHit = 0;
    public double hitEv = 0;
    public double WinsStand = 0;
    public double LosesStand = 0;
    public double DrawStand = 0;
    public double standEv = 0;
    public double WinsSplit = 0;
    public double LosesSplit = 0;
    public double DrawSplit = 0;
    public double splitEv = 0;
    public double WinsDouble = 0;
    public double LosesDouble = 0;
    public double DrawDouble = 0;
    public double doubleEv = 0;
    public int Trials { get; set; }
    public string BestAction { get; set; }

    public StrategyEntry(string handType, int handValue, int dealerUpcard)
    {
        HandType = handType;
        HandValue = handValue;
        DealerUpcard = dealerUpcard;
        BestAction = "stand";

        Trials = 0;
        //umesto gledanja action staviti sve u jedan entry i onda uzeti max na kraju pa to staviti kao best action

    }
    public void SetEv()
    {
        hitEv = (WinsHit - LosesHit) / 100000;
        standEv = (WinsStand - LosesStand) / 100000;
        splitEv = (WinsSplit - LosesSplit) / 100000;
        if(splitEv == 0)
        {
            splitEv = -100;
        }
        doubleEv = 2*(WinsDouble - LosesDouble) / 100000;
    }
    public void SetBestAction()
    {
        SetEv();
        Dictionary<string, double> actionEvs = new Dictionary<string, double>
        {
            { "hit", hitEv },
            { "stand", standEv },
            { "split", splitEv },
            { "double", doubleEv }
        };
        BestAction = "stand";
        double maxEv = double.NegativeInfinity;
        foreach (var action in actionEvs)
        {
            if (action.Value > maxEv)
            {
                maxEv = action.Value;
                BestAction = action.Key;
            }
        }
    }
}
