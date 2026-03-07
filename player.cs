using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//popraviti resolve da sve lepo resi
namespace Poker
{
   
    public class Player
    {
        public string Name { get; set; }
        public float Balance { get ; set;   }//enkapsulirano
        
        public class Hand
        {
            public Hand(List<Cards> karte, float bet)
            {
                this.Cards = karte;
                this.bet = bet;
            }//konstruktor
            public float bet { get; set; } = 0;
            public List<Cards> Cards { get; set; } = new List<Cards>();
            
            public int getHandValue(bool isAceHigh = true)
            {
                int totalValue = 0;
                int aceCount = 0;
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
        public Player(string name, float balance)
        {
            this.Name = name;
            this.Balance = balance;
        }//konstruktor
        public List<Hand> Hands { get; set; } = new List<Hand>();
        public int currentHand { get; set; } = 0;
        public bool IsDone()
        {
            return currentHand >= Hands.Count;
        }
        public void PlaceBet(float amount)
        {
            if (amount <= Balance)
            {
                Hands[currentHand].bet = amount;
                Balance -= amount;

            }
            else
            {
                throw new Exception("Insufficient balance to place bet.");
            }
        }
    }

    public class Cards
    {
        public string Rank { get; set; }
        public string CardSuit { get; set; }
        public Cards(string cardRank, string cardSuit)
        {
            this.Rank = cardRank;
            this.CardSuit = cardSuit;
        }
        public int Value(bool isAceHigh)
        {
            if (Rank == "A")
            {
                return isAceHigh ? 11 : 1;
            }
            else if (Rank == "K" || Rank == "Q" || Rank == "J")
            {
                return 10;
            }
            else
            {
                return int.Parse(Rank);
            }
        }

    }
    public class Deck
    {
        public int numberOfDecks { get; set; }
        public List<Cards> CardsAll { get; set; } = new List<Cards>();
        public Deck(int numberOfDecks)
        {
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            this.numberOfDecks = numberOfDecks;
            for (int i = 0; i< numberOfDecks; i++)
            {
                foreach (string rank in ranks)
                {
                    foreach (string suit in suits)
                    {
                        CardsAll.Add(new Cards(rank, suit));
                    }
                }
            }
        }
        public List<Cards> UsedCards { get; set; } = new List<Cards>();
        public void Shuffle()
        {
            Random rand = new Random();
            for (int i = CardsAll.Count - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                var temp = CardsAll[i];
                CardsAll[i] = CardsAll[j];
                CardsAll[j] = temp;
            }
        }//fiser yates algoritam
        public void Deal(Player player)
        {
            if (CardsAll.Count < 51)
            {
                for (int i = 0; i < UsedCards.Count; i++)
                {
                    CardsAll.Add(UsedCards[i]);
                    UsedCards.RemoveAt(i);
                }
            }
            player.Hands[player.currentHand].Cards.Add(CardsAll[0]);
            UsedCards.Add(CardsAll[0]);
            CardsAll.RemoveAt(0);
        }
        
        
        public void clearPlayer(Player player)
        {
            
                for (int i = 0; i<player.Hands.Count; i++)
                {
                    for (int j = 0; j<player.Hands[i].Cards.Count; j++)
                    {
                        UsedCards.Add(player.Hands[i].Cards[j]);
                    }
                }
                player.Hands.Clear();
            
                
        }
    }

    public class Game
    {
        public int currentPlayer { get; set; } = 0;
        public List<Player> Players { get; set; } = new List<Player>();
        public Deck GameDeck { get; set; }
        public int NumberOfDecks { get; set; }
        public Game(List<Player> players, int numberOfDecks)
        {
            this.Players=players;
            this.NumberOfDecks = numberOfDecks;
            GameDeck = new Deck(numberOfDecks);
            GameDeck.Shuffle();
            currentPlayer = 0;
        }
    }
    public class BlackjackGame : Game
    {
        public List<Player> broke { get; set; } = new List<Player>();
        public BlackjackDealer dealer { get; set; }
        public BlackjackGame(List<Player> players, int numberOfDecks, BlackjackDealer dealer) : base(players, numberOfDecks)
        {
            this.dealer = dealer;
        }
        public void start1()
        {
            currentPlayer = 0;
            foreach (var player in Players)
            {
                GameDeck.clearPlayer(player);
                player.currentHand = 0;
                Player.Hand newHand = new Player.Hand(new List<Cards>(), 0);
                player.Hands.Add(newHand);
            }
            GameDeck.Shuffle();
            dealer.Hands.Clear();
            DealDealerCards();

            currentPlayer = 0;
        }
        public bool isRoundOver()
        {
            return currentPlayer >= Players.Count;
        }
        public void resolveRound()
        {
            DealerPlay();
            int dealerValue = dealer.Hands[0].getHandValue();
            foreach(var player in Players)
            {
                for(int i = 0; i< player.Hands.Count; i++)
                {
                    int playerValue = (player as Player).Hands[i].getHandValue();
                    if (playerValue > 21)
                    {
                        player.Hands[i].bet = 0;
                        
                    }
                    else if (dealerValue>21)
                    {
                        player.Balance += player.Hands[i].bet*2;
                        player.Hands[i].bet = 0;
                        
                    }
                    else if(playerValue > dealerValue)
                    {
                        player.Balance += player.Hands[i].bet*2;
                        player.Hands[i].bet = 0;
                        
                    }
                    else if (playerValue == dealerValue)
                    {
                        player.Balance += player.Hands[i].bet;
                        player.Hands[i].bet = 0;
                        
                    }
                    else
                    {                      
                        player.Hands[i].bet = 0;
                        
                    }
                    if (player.Balance <= 0)
                    {
                        broke.Add(player as Player);
                       
                    }
                }
                foreach(var hand in player.Hands)
                {
                    hand.bet = 0;
                }
               
            }
            foreach(var player in broke)
            {
                Players.Remove(player);
            }
           
            
        }
        public bool checkblackjack(Player player)
        {
            if (player.Hands[0].getHandValue() == 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        public void DealInitialCards()
        {
            foreach (var player in Players)
            {
                GameDeck.Deal(player);
                GameDeck.Deal(player);
            }
        }
        public void DealDealerCards()//ovo je nepotrebno skloniti
        {
            Player.Hand newHand = new Player.Hand(new List<Cards>(), 0);
            dealer.Hands.Add(newHand);
            GameDeck.Deal(dealer);
            GameDeck.Deal(dealer);
        }
        public void DealerPlay()
        {
            while (dealer.Hands[0].getHandValue() < 17)
            {
                GameDeck.Deal(dealer);
            }
        }
        public void Split()
        {
            Player player = Players[currentPlayer ] as Player;
            int handNum = player.currentHand;
            if ((player.Hands[handNum].Cards.Count== 2)&&(player.Hands[handNum].Cards[0].Rank == player.Hands[handNum].Cards[1].Rank))
            {

                Cards temp = player.Hands[handNum].Cards[1];
                player.Hands[handNum].Cards.RemoveAt(1);
                List<Cards> temp2 = new List<Cards> { temp }; 
                Player.Hand newHand = new Player.Hand(temp2, player.Hands[player.currentHand].bet);
                player.Balance -= player.Hands[player.currentHand].bet; //deduct bet for new hand
                player.Hands.Add(newHand);
                
                GameDeck.Deal(player);
                player.currentHand++; //have to do this as the deal functions only affects current hand
                GameDeck.Deal(player);
                player.currentHand--;
                
            }
            else { }
        }
        public void Hit()
        {
            Player player = Players[currentPlayer ];
            GameDeck.Deal(player);
            if (player.Hands[player.currentHand].getHandValue()>21)
            {
                dealer.Balance += player.Hands[player.currentHand].bet; 
                player.Hands[player.currentHand].bet = 0;
                player.currentHand++;
                if (player.IsDone())
                {
                    currentPlayer++;
                    //if (isRoundOver())
                    //{
                    //    resolveRound();
                        
                    //}
                    
                }

            }
        }
        public void Stand()
        {
            Player player = Players[currentPlayer];
            player.currentHand++;
            if (player.IsDone())
            {
                currentPlayer++;
                //if (isRoundOver())
                //{
                //    resolveRound();
                //}
                
            }
           // player.currentHand--;
        }
        public void DoubleDown()
        {
            Player player = Players[currentPlayer ];
            if (player.Balance >= player.Hands[player.currentHand].bet)
            {
                player.Hands[player.currentHand].bet *= 2;
                player.Balance -= player.Hands[player.currentHand].bet/2;
                
                GameDeck.Deal(player);
                if (player.Hands[player.currentHand].getHandValue()>21)
                {
                    dealer.Balance += player.Hands[player.currentHand].bet;
                    player.Hands[player.currentHand].bet = 0;
                    player.currentHand++;
                    if (player.IsDone())
                    {
                        currentPlayer++;
                    }
                }
                else
                {
                    Stand();
                }
                    
            }
            else
            {
                throw new Exception("Insufficient balance to double down.");
            }

        }
    }
    
    public class BlackjackDealer : Player
    {
        public BlackjackDealer() : base("Dealer", 1000000)
        {
        }
            


    }

    public class Simulation
    {
        public void Dispose()
        {
            // Očisti resurse
            GC.Collect();  // Prinudi garbage collector
        }
        public int pV = 0;
        public int dV = 0;
        public string Action = "";
        
        public int wins = 0;
        public int losses = 0;
        public int draws = 0;
        public StrategyManager manager = new StrategyManager();
        public Strategy strat;
        public string mHandType = "";
        int trials = 100000;
        
        public Simulation(int playerValue, int dealerValue, string action, Strategy strats, string handType)
        {
            this.pV = playerValue;
            this.dV = dealerValue;
            this.Action = action;
            this.strat = strats;
            mHandType = handType;
        }

        private static Random _rnd = new Random();
        public void simulate(int playerValue, int dealerValue,int thisaces, string action, string HandType)
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
                            else if (strat.getBestAction("hard", playerValue, dealerValue) == "double" || strat.getBestAction("hard", playerValue, dealerValue) == "stand" || strat.getBestAction("hard", playerValue, dealerValue) == "split")
                            {
                                stand();
                            }
                            else { simulate(playerValue, dealerValue, aces,action,HandType); }
                        }
                        else if (playerValue > 17)
                        {
                            stand();
                        }
                        else
                        {
                            if (strat.getBestAction("hard", playerValue, dealerValue) == "double" || strat.getBestAction("hard", playerValue, dealerValue) == "stand")
                            {
                                stand();
                            }
                            else { simulate(playerValue, dealerValue, aces,action,HandType); }
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
                        else if (strat.getBestAction("hard", playerValue, dealerValue) == "double" || strat.getBestAction("hard", playerValue, dealerValue) == "stand" || strat.getBestAction("hard", playerValue, dealerValue) == "split")
                        {
                            stand();
                        }
                        else { simulate(playerValue, dealerValue, aces, action, HandType); }
                    }
                    else if (playerValue > 17)
                    {
                        stand();
                    }
                    else
                    {
                        if (strat.getBestAction("hard", playerValue, dealerValue) == "double" || strat.getBestAction("hard", playerValue, dealerValue) == "stand")
                        {
                            stand();
                        }
                        else { simulate(playerValue, dealerValue, aces, action, HandType); }

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
                        else if (strat.getBestAction("hard", playerValue, dealerValue) == "double" || strat.getBestAction("hard", playerValue, dealerValue) == "stand" || strat.getBestAction("hard", playerValue, dealerValue) == "split")
                        {
                            stand();
                        }
                        else { simulate(playerValue, dealerValue, aces, action, HandType); }
                    }
                    else if (playerValue > 17)
                    {
                        stand();
                    }
                    else
                    {
                        if (strat.getBestAction("hard", playerValue, dealerValue) == "double" || strat.getBestAction("hard", playerValue, dealerValue) == "stand")
                        {
                            stand();
                        }
                        else { simulate(playerValue, dealerValue, aces, action, HandType); }

                    }
                }
                else if (action == "split")
                {
                    playerValue = playerValue/2;

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
                    if (randomValue == playerValue)
                    {
                        HandType = "pair";
                    }
                    else { HandType = "hard"; }
                    
                    int a = playerValue;
                    playerValue = playerValue + randomValue;
                    string bestAction = strat.getBestAction("hard", playerValue, dealerValue);
                    
                    string temp = action;
                    action = bestAction;
                    
                    simulate(playerValue, dealerValue, aces, action, HandType);
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
                    if (randomValue == playerValue)
                    {
                        HandType = "pair";
                    }
                    else { HandType = "hard"; }
                    playerValue = playerValue + randomValue;
                    bestAction = strat.getBestAction("hard", playerValue, dealerValue);
                    action = bestAction;
                    simulate(playerValue, dealerValue,aces, action, HandType);
                    action = temp;
                    HandType = "pair";
                }
            }


        }

        public void runSimulation()
        {
            int aces = 0;
            if(mHandType == "soft")
            {
                aces = 1;
            }
            for (int i = 0; i < trials; i++)
            {
                simulate(pV, dV, aces,Action,mHandType);
            }
        }

    }



}

