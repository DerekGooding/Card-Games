
using System.Text.Json;
using Poker;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


namespace Poker
{
    internal class mainstart
    {
        public static void RunSim(string[] args)
        {

            
            
            
            Dictionary<string, int> cases = new Dictionary<string, int>();
            int trials = 100000;
            List<StrategyEntry> strategyEntries = new List<StrategyEntry>();
            Strategy strategy = new Strategy(trials, strategyEntries);
            StrategyManager manager = new StrategyManager();
            
            for (int i = 19; i > 4; i--) {
                
                for(int dealerCard = 2; dealerCard < 12; dealerCard++)
                {
                    StrategyEntry entry = new StrategyEntry("hard", i, dealerCard);
                    Simulation simulation = new Simulation(i, dealerCard, "stand",strategy,"hard");
                    simulation.runSimulation();
                    entry.WinsStand = simulation.wins;
                    entry.LosesStand = simulation.losses;
                    entry.DrawStand = simulation.draws;

                    simulation.Dispose();
                    
                    Simulation simulation1 = new Simulation(i, dealerCard, "double", strategy, "hard");
                    simulation1.runSimulation();
                    entry.WinsDouble = simulation1.wins;
                    entry.LosesDouble = simulation1.losses;
                    entry.DrawDouble = simulation1.draws;
                    simulation1.Dispose();

                    Simulation simulation2 = new Simulation(i, dealerCard, "hit", strategy, "hard");
                    simulation2.runSimulation();
                    entry.WinsHit = simulation2.wins;
                    entry.LosesHit = simulation2.losses;
                    entry.DrawHit = simulation2.draws;
                    strategy.AddEntry(entry, "hard" + i +"vs"+ dealerCard);
                    simulation2.Dispose();
                    entry.setBestAction();
                }

            }
            for (int i = 2; i < 11; i++) {
                
                for (int dealerCard = 2; dealerCard < 12; dealerCard++)
                {
                    StrategyEntry entry = new StrategyEntry("soft", i, dealerCard);
                    Simulation simulation = new Simulation(i+11, dealerCard, "stand", strategy, "soft");
                    simulation.runSimulation();
                    entry.WinsStand = simulation.wins;
                    entry.LosesStand = simulation.losses;
                    entry.DrawStand = simulation.draws;
                    simulation.Dispose();

                    Simulation simulation1 = new Simulation(i+11, dealerCard, "double", strategy, "soft");
                    simulation1.runSimulation();
                    entry.WinsDouble = simulation1.wins;
                    entry.LosesDouble = simulation1.losses;
                    entry.DrawDouble = simulation1.draws;
                    simulation1.Dispose();

                    Simulation simulation2 = new Simulation(i+11, dealerCard, "hit", strategy, "soft");
                    simulation2.runSimulation();
                    entry.WinsHit = simulation2.wins;
                    entry.LosesHit = simulation2.losses;
                    entry.DrawHit = simulation2.draws;
                    strategy.AddEntry(entry, "soft" + i + "vs" + dealerCard);
                    simulation2.Dispose();
                    entry.setBestAction();
                }
            }
            for (int i = 4; i < 22; i +=2) {
                
                for (int dealerCard = 2; dealerCard < 12; dealerCard++)
                {
                    StrategyEntry entry = new StrategyEntry("pair", i, dealerCard);
                    Simulation simulation = new Simulation(i , dealerCard, "stand", strategy, "pair");
                    simulation.runSimulation();
                    entry.WinsStand = simulation.wins;
                    entry.LosesStand = simulation.losses;
                    entry.DrawStand = simulation.draws;
                    simulation.Dispose();

                    Simulation simulation1 = new Simulation(i, dealerCard, "double", strategy, "pair");
                    simulation1.runSimulation();
                    entry.WinsDouble = simulation1.wins;
                    entry.LosesDouble = simulation1.losses;
                    entry.DrawDouble = simulation1.draws;
                    simulation1.Dispose();

                    Simulation simulation2 = new Simulation(i, dealerCard, "hit", strategy, "pair");
                    simulation2.runSimulation();
                    entry.WinsHit = simulation2.wins;
                    entry.LosesHit = simulation2.losses;
                    entry.DrawHit = simulation2.draws;
                    simulation2.Dispose();

                    Simulation simulation3 = new Simulation(i, dealerCard, "split", strategy, "pair");
                    simulation3.runSimulation();
                    entry.WinsSplit = simulation3.wins;
                    entry.LosesSplit = simulation3.losses;
                    entry.DrawSplit = simulation3.draws;
                    strategy.AddEntry(entry, "pair" + i + "vs" + dealerCard);
                    simulation2.Dispose();
                    entry.setBestAction();
                    
                }
            }
            StrategyManager.SaveStrategyToJson(strategy, "./Data/models/blackjack_strategy.json");



            //ne radi double daje premalo pusheva i takodje izracunati best action pre cuvanja, pokusati implementirati brzi random



        }
    }
}
