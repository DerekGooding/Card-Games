namespace Card_Games;

internal class TabelAbj
{
    public class StrategyEntry(string handType, int handValue, int dealerUpcard, string theAction)
    {
        public string HandType { get; set; } = handType;
        public int HandValue { get; set; } = handValue;
        public int DealerUpCard { get; set; } = dealerUpcard;
        public string Action { get; set; } = theAction;
        public double Wins { get; set; } = 0;
        public double Loses { get; set; } = 0;
        public double Draw { get; set; } = 0;
        public int Trials { get; set; } = 0;
    }


    public class Strategy(int trials, List<TabelAbj.StrategyEntry> tabela)
    {
        public int TrialsPerCell { get; set; } = trials;
        public List<StrategyEntry> Tabela { get; set; } = tabela;
    }
}