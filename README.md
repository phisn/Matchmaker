# Matchmaker
Matchmaking and ratings for small teams. Automatically balances
players between two teams based on rating. Ratings can be semi-automatically
updated based on the trueskill algorithm.

Player data is saved in a SQLite database. There is currently no winning
history, only name and rating are stored.

## Use Case
Did you ever want to play a two Team based game with your friends but unable
to create new fair team constellation after each round?

This software does track a rating for each player and does create random 
teams to be as fair as possible. This should result in each player winning 
around 50% of their games.

## Usage
Abbreviation can be used. For example `m r` instead of `matchmaking reload`

- **add participant <name> [score]**  
     adds new participant to database
- **add results <winner: blue/red>**  
     asks for results and updates scores for both teams
- **matchmaking quick [participants...]**  
     removes all current participants, adds new and shuffels
- **matchmaking reload**  
     automatically shuffels fair teams for matches
- **remove participant <name/id>**  
     removes participant from team
- **search participant <name/id/*>**  
     search participant from database
- **show score <participant/id>**  
     shows score for a specific participant
- **show teams**  
     prints current teams
- **team move <participant/id> <blue/red>**  
     moves or adds a participant to a team
- **team remove <participant/id>**  
     removes a participant from a team

## Example
Add Players  
1.a `a p player1`  
1.b `a p player2`  
1.c `a p player3`  
1.d `a p player4`  
1.e `a p player5`  

Show all players  
2. `s p *`  

Setup match  
3. `m q player1 player2 player2 player3 player4 player5`  

Show teams and ratings  
4. `s t`  

If blue team wins, add win for blue  
5. `a r blue`  

Show new ratings  
6. `s t`  

Generate new teams  
7. `m r`  
