# TripeTriadUnity
A simple card game project intended to help me learn some fundamental Unity scripting

# Rules
The rules for Triple Triad come from the Final Fantasy 8 minigame.
 ## Board
   - The board consists of a 3x3 grid (hence the name) where each space can fit a card. A board space can only contain one card and cards once played cannot be removed.
 ## Player Hand
   - There are two players, and each has 5 cards.
   - Once the player hand is populated, the cards in it cannot be added to or changed, only played onto the board.
   - Players place cards in alternating turns.
   - The player playing second will end the game with one card remaining in their hand.
 ## Cards
   - Each card has 4 values, top, right, bottom and left.
   - Each card is owned by a player.
   - When a card is placed on the board, the card's values are compared with the neighbouring values of the opponent player's cards on adjacent occupied spaces. Top/bottom, right/left, bottom/top, left/right. If the placed card's value is higher than the adjacent card's value, the adjacent card changes ownership to the player placing the card.
 ## Scoring
   - The player's score is equal to the number of cards that they control, *including the cards in their hand*.
   - Player scores always start at 5 each and always total to 10. The maximum winning margin is 9-1.
   - Ties are possible.


# Milestones
 ## MVP
  - Cards render and can be clicked, dragged and dropped onto board spaces.
  - Single game gameplay is possible using the basic rules of the game.
  - Basic AI Opponent
 ## Milestone 1
  - Selection stage at the start of each game where player can select the cards they want to play with.
  - Visualisation of AI turn, more complex AI that can have playstyle adjusted.
  - After finishing a game, player has the option to continue into a new game.
  - Post-Game screen that can allow player to pick the cards they want to take forward into the selection stage of the next game.
  - Basic UI to show player turn and score in-game.
 ## Milestone 2
  - Add handling for additional rulesets.
  - Increase AI difficulty on player wins.
 ## Milestone 3
  - Implement within an overworld where the player can move around and challenge AI opponents with different playstyles.
  - Attach Rules to cards, potentially with upgrade paths.
