# Blackjack Simulator & Game

A C# WinForms blackjack game that also doubles as a Monte Carlo simulation engine for figuring out optimal play strategy. Started as a game, turned into something a bit more interesting.

The long-term goal is a model that plays blackjack perfectly, and eventually a Texas Hold'em bot that can at least beat beginners. Poker isn't there yet — blackjack is the current focus.

---

## What it does

**Play mode** — a functional blackjack game with a Windows Forms UI. Supports up to 7 players, standard actions (Hit, Stand, Double Down, Split), betting via a slider, and a dealer that plays by the book. Cards are rendered from a local PNG resource folder.

**Simulation mode** — run from the command line with `--sim` and it fires off Monte Carlo simulations across every meaningful hand combination (hard totals, soft totals, pairs) against every possible dealer upcard. 100,000 trials per cell. Results are saved to a JSON strategy file that the game can then reference for optimal play decisions.

The strategy engine calculates expected value for each action and picks the best one — so over time it converges on something close to basic strategy, derived from simulation rather than hardcoded tables.

---

## Getting started

**Requirements:** Visual Studio, .NET Framework 4.7.2

Open `Poker.sln` in Visual Studio and hit Run for the UI, or build and run from the command line:

```bash
# Run the game normally
Poker.exe

# Run the simulation and generate a strategy file
Poker.exe --sim
```

The strategy output lands at `./Data/models/blackjack_strategy.json`. Once generated, the game uses it for decision-making in simulation mode.

---

## Project structure

```
├── Form1.cs / Form1.Designer.cs   — main game window
├── Home.cs / Home.Designer.cs     — player setup screen (names + balances)
├── player.cs                      — Player, Cards, Deck, BlackjackGame, Simulation classes
├── tabela.cs                      — StrategyEntry and Strategy (the lookup table)
├── tabelabj.cs                    — older strategy table (kept around, not currently used)
├── StrategyManager.cs             — JSON save/load for strategy files
├── mainstart.cs                   — CLI entry point for running simulations
└── Resources/                     — card PNG images (52 cards + card back)
```

---

## Known issues / notes

- There's a memory leak in the card image loading — `Image.FromFile()` is called every time a card renders and the images aren't being disposed properly. It's noted in the code, just not fixed yet.
- The `tabelabj.cs` file is an older version of the strategy table that's been superseded by `tabela.cs`. It's still in the project but doesn't do anything.
- Double Down EV is multiplied by 2 in the calculation to account for the doubled stake, but the simulation doesn't currently produce enough "push" results — known issue flagged in comments.
- The random number generator in the simulation was switched to a static shared instance to improve performance, but very high trial counts might still be slow.
- Poker is not implemented yet. The project is named "Poker" for historical reasons.

---

## License

MIT — do whatever you want with it.
