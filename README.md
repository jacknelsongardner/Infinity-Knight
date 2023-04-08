# InfinityKnight
a scrolling chess puzzle game made in unity for the cougHacks Hackathon at WSU. In the game, you "pilot" a knight around an infinitely scrolling chessboard
you do so, wilst avoiding bishops and rooks, as well as red spikes following your tail, and a mysterious red fog up ahead (don't touch either or you'll die!)
the farther you make it, the more points you score. The high score is stored in the backend, and updates whenever the player beats it.

The application is mainly built up of the following scripts:

EnemyScripts, which tell enemy gameObjects to destroy themselves if they are on the same space as the player, and (in scripts extending them) how to attack the player
KnightScript, which tests if the player can move to a certain space, tests if it is falling behind or gotten too far ahead (also serves as a target for enemies)
TileScripts, which communicate with the backend boardObject when they are clicked, and if they are in range of the player, the backend tells the player to go there. They are automatically generated as you get further and further into the game.
MovingBoard, which constantly scrolls. It's made up of lots of tiles that constantly respawn (and destroy themselves when they vere off the edge). It spawns the player in the beginning, and spawns enemies on upcoming tiles.
GameBrain, which determines if the player has lost or not, whether the game pauses, and whether we switch scenes.
