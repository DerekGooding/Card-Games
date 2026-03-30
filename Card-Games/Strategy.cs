namespace Card_Games;

public class Strategy
{
    public Strategy() { }
    public Strategy(int trials, List<StrategyEntry> tabela)
    {
        TrialsPerCell = trials;
        this.Tabela = tabela;
    }
    public int TrialsPerCell { get; set; }

    public string GetBestAction(string handType, int handValue, int dealerUpcard)
    {

        if (handValue >= 21) return "stand";
        if (handValue < 8) return "hit";

        string kljuc = handType + handValue + "vs" + dealerUpcard;

        return strategija.TryGetValue(kljuc, out var entry) ? entry.BestAction : (handValue < 17) ? "hit" : "stand";
    }

    public List<StrategyEntry> Tabela { get; set; }
    public Dictionary<string, StrategyEntry> strategija = [];
    public void AddEntry(StrategyEntry entry, string name) => strategija.TryAdd(name, entry);
}