//popraviti resolve da sve lepo resi
namespace Card_Games;

public class Simulation(int playerValue, int dealerValue, string action, Strategy strats, string handType)
{
    public void Dispose() =>
        // Očisti resurse
        GC.Collect();  // Prinudi garbage collector
    public int pV = playerValue;
    public int dV = dealerValue;
    public string Action = action;

    public int wins = 0;
    public int losses = 0;
    public int draws = 0;
    public StrategyManager manager = new();
    public Strategy strat = strats;
    public string mHandType = handType;
    int trials = 100000;
    private static Random _rnd = new();
    public void Simulate(int playerValue, int dealerValue, int thisaces, string action, string HandType)
    {
        int aces = thisaces;
        int acesDealer = 0;
        acesDealer = (dealerValue == 11) ? 1 : 0;

        int randomValue = _rnd.Next(2, 15);//kada imamo soft hand kec se sam dodaje pa dobijemo infinite loop
        void stand()
        {
            randomValue = 0;
            while (dealerValue < 17)
            {
                randomValue = _rnd.Next(2,15);
                if (randomValue == 11)
                {

                    randomValue = 11;
                    acesDealer++;
                }
                else if (randomValue >10 )
                {
                    randomValue = 10;
                }
                dealerValue += randomValue;
                if(dealerValue > 21 && acesDealer > 0)
                {
                    while (dealerValue > 21 && acesDealer > 0)
                    {
                        dealerValue -= 10;
                        acesDealer--;
                    }
                }

            }
            if (dealerValue > 21 || playerValue > dealerValue)
            {
                wins++;

            }
            else if (playerValue == dealerValue)
            {
                draws++;

            }
            else
            {
                losses++;

            }
        }
        randomValue = _rnd.Next(2,15);
        if (randomValue == 11)
        {

            randomValue = 11;
            aces++;
        }
        else if (randomValue >10)
        {
            randomValue = 10;
        }
        if (HandType == "hard")
        {
            if (action == "stand")
            {
                stand();
            }
            else if (action == "double")
            {
                playerValue += randomValue;
                if (playerValue > 21 && aces == 0)
                {
                    losses++;
                }
                else if (playerValue > 21 && aces > 0)
                {
                    while (playerValue > 21 && aces > 0)
                    {
                        playerValue -= 10;
                        aces--;
                    }
                    stand();
                }
                else
                {
                    stand();
                }

            }//paziti na zagradu
                else if (action == "hit")
                {
                    playerValue += randomValue;
                    if (playerValue > 21 && aces == 0)
                    {
                        losses++;
                    }
                    else if (playerValue > 21 && aces > 0)
                    {
                        while (playerValue > 21 && aces > 0)
                        {
                            playerValue -= 10;
                            aces--;
                        }
                        if(playerValue > 17)
                        {
                            stand();
                        }
                        else if (strat.GetBestAction("hard", playerValue, dealerValue) == "double" || strat.GetBestAction("hard", playerValue, dealerValue) == "stand" || strat.GetBestAction("hard", playerValue, dealerValue) == "split")
                        {
                            stand();
                        }
                        else { Simulate(playerValue, dealerValue, aces,action,HandType); }
                    }
                    else if (playerValue > 17)
                    {
                        stand();
                    }
                    else
                    {
                        if (strat.GetBestAction("hard", playerValue, dealerValue) == "double" || strat.GetBestAction("hard", playerValue, dealerValue) == "stand")
                        {
                            stand();
                        }
                        else { Simulate(playerValue, dealerValue, aces,action,HandType); }
                    }
                }


        }
        else if (HandType == "soft")
        {
            aces++;
            if (action == "stand")
            {
                stand();
            }
            else if (action == "double")
            {
                playerValue += randomValue;
                if (playerValue > 21 && aces == 0)
                {
                    losses++;
                }
                else if (playerValue > 21 && aces > 0)
                {
                    while (playerValue > 21 && aces > 0)
                    {
                        playerValue -= 10;
                        aces--;
                    }
                    stand();
                }
                else
                {
                    stand();
                }
            }
            else if (action == "hit")
            {
                playerValue += randomValue;
                if (playerValue > 21 && aces == 0)
                {
                    losses++;
                }
                else if (playerValue > 21 && aces > 0)
                {
                    while (playerValue > 21 && aces > 0)
                    {
                        playerValue -= 10;
                        aces--;
                    }
                    if (aces == 0) {
                        HandType = "hard";
                    }
                    if(playerValue > 17)
                    {
                        stand();
                    }
                    else if (strat.GetBestAction("hard", playerValue, dealerValue) == "double" || strat.GetBestAction("hard", playerValue, dealerValue) == "stand" || strat.GetBestAction("hard", playerValue, dealerValue) == "split")
                    {
                        stand();
                    }
                    else { Simulate(playerValue, dealerValue, aces, action, HandType); }
                }
                else if (playerValue > 17)
                {
                    stand();
                }
                else
                {
                    if (strat.GetBestAction("hard", playerValue, dealerValue) == "double" || strat.GetBestAction("hard", playerValue, dealerValue) == "stand")
                    {
                        stand();
                    }
                    else { Simulate(playerValue, dealerValue, aces, action, HandType); }

                }
            }


        }
        else if (HandType == "pair")
        {
            if(playerValue == 22)
            {
                playerValue = 12;
                aces++;
            }
            if (action == "stand")
            {
                stand();
            }
            else if (action == "double")
            {
                playerValue += randomValue;
                if (playerValue > 21 && aces == 0)
                {
                    losses++;
                }
                else if (playerValue > 21 && aces > 0)
                {
                    while (playerValue > 21 && aces > 0)
                    {
                        playerValue -= 10;
                        aces--;
                    }
                    stand();
                }
                else
                {
                    stand();
                }
            }
            else if (action == "hit")
            {
                playerValue += randomValue;
                if (playerValue > 21 && aces == 0)
                {
                    losses++;
                }
                else if (playerValue > 21 && aces > 0)
                {
                    while (playerValue > 21 && aces > 0)
                    {
                        playerValue -= 10;
                        aces--;
                    }
                    if(playerValue > 17)
                    {
                        stand();
                    }
                    else if (strat.GetBestAction("hard", playerValue, dealerValue) == "double" || strat.GetBestAction("hard", playerValue, dealerValue) == "stand" || strat.GetBestAction("hard", playerValue, dealerValue) == "split")
                    {
                        stand();
                    }
                    else { Simulate(playerValue, dealerValue, aces, action, HandType); }
                }
                else if (playerValue > 17)
                {
                    stand();
                }
                else
                {
                    if (strat.GetBestAction("hard", playerValue, dealerValue) == "double" || strat.GetBestAction("hard", playerValue, dealerValue) == "stand")
                    {
                        stand();
                    }
                    else { Simulate(playerValue, dealerValue, aces, action, HandType); }

                }
            }
            else if (action == "split")
            {
                playerValue /= 2;

                randomValue = _rnd.Next(2,15);
                int aceTemp = aces;
                if (randomValue == 11)
                {
                    randomValue = 11;
                    aces++;

                }
                else if (randomValue > 10)
                {

                    randomValue = 10;
                }
                HandType = randomValue == playerValue ? "pair" : "hard";

                int a = playerValue;
                playerValue += randomValue;
                string bestAction = strat.GetBestAction("hard", playerValue, dealerValue);

                string temp = action;
                action = bestAction;

                Simulate(playerValue, dealerValue, aces, action, HandType);
                playerValue = a;
                aces = aceTemp;
                randomValue = _rnd.Next(2, 15);
                if (randomValue == 11)
                {

                    randomValue = 11;
                    aces++;

                }

                else if (randomValue >10)
                {
                    randomValue = 10;
                }
                HandType = randomValue == playerValue ? "pair" : "hard";
                playerValue += randomValue;
                bestAction = strat.GetBestAction("hard", playerValue, dealerValue);
                action = bestAction;
                Simulate(playerValue, dealerValue,aces, action, HandType);
                action = temp;
                HandType = "pair";
            }
        }


    }

    public void RunSimulation()
    {
        int aces = 0;
        if(mHandType == "soft")
        {
            aces = 1;
        }
        for (int i = 0; i < trials; i++)
        {
            Simulate(pV, dV, aces,Action,mHandType);
        }
    }

}

