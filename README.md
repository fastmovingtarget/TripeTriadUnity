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
