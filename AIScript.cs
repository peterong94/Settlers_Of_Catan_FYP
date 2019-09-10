using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIScript : MonoBehaviour
{
    public Game game;
    public Diceroll roll;
    public Player aiPlayer;
    public Player humanPlayer;
    public Robber robber;
    public bool settlementSpaceAvailable = true, roadSpaceAvailable = true;

    public void aiTurn() //Main AI behvaiour function, called when the player clicks end turn button           
    {
        game.gameTurn = "ai"; //Sets the turn to AI in the game class, making some functions effect the AI rather than the player and making sure the player cannot take actions
        roll.roll(); //Rolls the dice, produces resources
        checkSettlementSpace(); 
        settle(); 
        checkRoadSpace(); 
        createRoad(); 
        humanPlayer.hasRolled = false; //Resets player rolled boolean allowing player to roll again
        game.gameTurn = "player"; //Sets the the turn to Player in the game class
    }

    // function to check for a valid settlement placement 
    public void checkSettlementSpace() 
    {
        for(int i = 0; i < game.intersections.Count; i++) //Loops through every intersection
        {
            game.intersections[i].checkNeighbour(); //Checks the intersection to see if it has any settlements ajacent to it
            if (game.intersections[i].hasNeighbour == false || game.intersections[i].settlement == "aiSettlement") //If the spce has no neighbour or if the settlement can be made acity
            {
                settlementSpaceAvailable = true;
                break; //One valid move found so looping through the res is unnecessary
            }

            if(i == game.intersections.Count) //If looped through every intersection without finding a valid placment 
            {
                settlementSpaceAvailable = false;
            }
        }
    }

    // function to check for a valid road placement 
    public void checkRoadSpace()
    {
        for (int i = 0; i < game.edges.Count; i++) //loops through every edge
        {
            game.edges[i].checkNeighbour(aiPlayer); //Checks if there is a settlement ajacent to the edge owned by the AI
            if (game.edges[i].hasAINeighbour == true && game.edges[i].road == "none") //If there is no road build and there and there is an ajacent AI settlement 
            {
                settlementSpaceAvailable = true; 
                break;
            }

            if (i == game.intersections.Count) //If looped through every intersection without finding a valid placment 
            {
                settlementSpaceAvailable = false; 
            }
        }
    }

    // Makes a settlement for the AI or upgrades an AI settlement to a city 
    public void settle()
    {
        // This if statment checks either if the player has enough resoruces to place a city and there is a valid move or if the game is in the setup phase
        if ((aiPlayer.bricks >= 1 && aiPlayer.lumber >= 1 && aiPlayer.wool >= 1 && aiPlayer.grain >= 1 && settlementSpaceAvailable == true) || game.setup == true)
        {
            bool intersectionChosen = false; // Boolean to check if the function has made a valid move

            while (intersectionChosen == false) // While loop ends once a settlement location is chosen or an upgrade is made 
            {
                 System.Random r = new System.Random();
                 int chosenIntersection = r.Next(0, game.intersections.Count); // Chooses a random intersection
                 game.intersections[chosenIntersection].checkNeighbour(); //Checks to see if it has any ajacent settlements
                 if (game.intersections[chosenIntersection].hasNeighbour == false) //If there are no ajacent settlemnts 
                 {
                    switch (game.intersections[chosenIntersection].settlement) // Switch checks to see the status of the intersections ownership
                    {
                       case "none": // No settlement is present
                           game.intersections[chosenIntersection].createSettlement(aiPlayer); // Create a settlement at the current intersection
                           intersectionChosen = true; //Ends the loop
                           break;
                       case "aiSettlement": // Already a settlement
                           game.intersections[chosenIntersection].createCity(aiPlayer); // Upgrade the settlement to a city
                           intersectionChosen = true; // Ends the loop
                           break;
                       default:
                           break;
                    }
                }
            }
        }
    }

    // Function to place a road
    public void createRoad()
    {
        // If statement to check if the AI has enough resources to place a road that a valid space is vaiable or if the game is in the setup phase
        if ((aiPlayer.lumber >= 1 && aiPlayer.bricks >= 1 && roadSpaceAvailable == true) || game.setup == true)
        {
            bool edgeChosen = false; // Boolean to check if the function has made a valid move
            System.Random r = new System.Random();
            while (edgeChosen == false) // Boolean to check if the function has made a valid move
            {
                int chosenEdge = r.Next(0, game.edges.Count);  // Choses a random edge
                game.edges[chosenEdge].checkNeighbour(aiPlayer); // Checks if there is an ajacent AI owned settlement  
                if (game.edges[chosenEdge].hasAINeighbour == true && game.edges[chosenEdge].road == "none") // If there is no road built and there is an ajacent AI settlement
                {
                    game.edges[chosenEdge].buildRoad(aiPlayer); // Build a road on this edge 
                    edgeChosen = true; // Ends the loop
                }

            }
        }
    }

    // Function to move the robber if a 7 is rolled on the AIs turn
    public void moveRobber()
    {
        System.Random r = new System.Random();
        int chosenHex = r.Next(0, game.hexes.Count); // choose a random hex
        if(game.hexes[chosenHex].hasRobber == false) // If there is no robber on the hex
        {
            game.hexes[chosenHex].clearRobber(); // Clear the robber from its current hex
            game.hexes[chosenHex].hasRobber = true; // Put the robber on the chosen hex
            game.hexes[chosenHex].token.overrideSprite = game.hexes[chosenHex].robberToken; // Change the UI sprite to show the robber
            for (int i = 0; 0 < game.hexes[chosenHex].neighbourIntersections.Count; i++) // Loop through all interesctions of the chosen hex
            {
                if (game.hexes[chosenHex].neighbourIntersections[i].settlement == "playerSettlement" || game.hexes[chosenHex].neighbourIntersections[i].settlement == "playerCity") // If there is a player controlled settlement resent 
                {
                    robber.stealResource(aiPlayer, humanPlayer); // Steal a resoruce from the human player
                    break;
                }
            }
        }

    }
}
