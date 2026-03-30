namespace Card_Games;

internal class Tabelabj
{
    public class StrategyEntry
    {
        public string HandType { get; set; }      // "Hard", "Soft", "Pair"
        public int HandValue { get; set; }        // 3-19 za hard, 2-9 za soft, 1-10 za pair
        public int DealerUpcard { get; set; }     // 2-11 (11 = Ace)
        public string Action { get; set; }    // "Stand", "Hit", "Double", "Split"
        public double Wins { get; set; }       // Statistika 
        public double Loses { get; set; }
        public double Draw { get; set; }
        public int Trials { get; set; }           // Broj simulacija

        public StrategyEntry(string handType, int handValue, int dealerUpcard, string theAction)
        {
            HandType = handType;
            HandValue = handValue;
            DealerUpcard = dealerUpcard;
            Action = theAction;
            Wins = 0;
            Loses = 0;
            Draw = 0;
            Trials = 0;
        }
    }


    public class Strategy
    {
        public Strategy(int trials, List<StrategyEntry> tabela)
        {
            TrialsPerCell = trials;
            this.tabela = tabela;
        }
        public int TrialsPerCell { get; set; }
        public List<StrategyEntry> tabela { get; set; }
    }
}