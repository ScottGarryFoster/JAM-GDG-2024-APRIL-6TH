# Game Jam - Game Maker Simulator
This is a game jam repo for a Jam over 48 Hours for the GDG jam in April 2024.
[Play here](https://scottgarryfoster.itch.io/game-maker-simulator).

This was a solo game jam and the goal was simply to attempt to create something I had not created before. In previous Jam projects I make what I know, platformers, top-down action, or puzzle games. For this project I decided to just try something I had never and went with an incremental style game in which you attempt to create a successful video game.

It uses a few stats for the game and a hidden stat 'bugs' which determines how good your game ends up being. Events then drive how these stats increase and based on your previous actions and an energy bar (also affected by previous actions). Events had a binary choice and message at the start, success and failure for each option. The gameplay loop came in with a limited number of events (days) to complete the game. Upon completing the game, it compared the stats to determine how 'good' your game was.

## What went well
1. There were no bugs to speak of that I could find. The game was simple enough to ensure that provided I gave enough events of particular types that bugs could not occur.
2. The styling looks pretty nice given it is entirely Unity UI.
3. The implementation of reactions for the bars so that you could see what was happening was a good addition and allowed the user to see what was happening.

## What could have gone better
1. The game is too easy. On the normal difficulty you can just spam an option and probably get a good game.
   * This is because of not having enough time to balance the game. I kept going from too hard, to too easy and not having enough events to play with made this a little difficult.
 2. Not visual enough. Although styled well the styling can only go so far in terms of what the player is looking at. In the mock up designs there was a viewport for what the game looked like. The idea being as the art increased it would look better, gameplay maybe a character would play better - maybe bugs would not be hidden then but seen in this viewport.
 3. I did not have a complete loop - start play end - until lunch time on the second day. This did not give me enough time to truly play test.

## Conclusions
1. Data heavy games are to be avoided - this is likely why procedural generation is used in Jams
2. Get a gameplay loop early for play testing and get others to play test it
3. Consider the actions a player is doing and are they fun. 
