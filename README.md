# PhotonBoltOnEventBugRepo
Simple Unity Project showcasing a potential bug in Bolt

# Which Bolt Version?
Bolt Free Debug v1.2.10

# Which Unity Version to use?
2019.2.6f1
[download here](https://unity3d.com/get-unity/download/archive)

# Reproduction steps
0. Clone repository.
1. Build Unity Project.
2. Run 2 clients.
3. Have Player 1 hit Host button.
4. Have Player 2 hit Join button.
5. Wait for Player 2 to connect to Player 1.
6. Have Player 1 hit Send Event button.
7. Observe Player 2 receives event via status text update.
8. Have Player 1 hit Shutdown.
9. Observe Player 1 and Player 2 disconnect.
10. Have Player 2 hit Host button.
11. Have Player 1 hit Join button.
12. Wait for Player 1 to connect to Player 2.
13. Have Player 2 hit Send Event button.
14. Observe Player 1 doesn't receive event.
