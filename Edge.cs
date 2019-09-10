using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Edge : MonoBehaviour
{
    public string road = "none";
    public bool hasPlayerNeighbour, hasAINeighbour;
    public List<Intersection> neighbourIntersections = new List<Intersection>();
    public Player humanPlayer;
    public Player aiPlayer;
    public Text announcements;
    public AIScript aiScript;
    public Development dev;
    public Game game;
    public Diceroll diceroll;
    public Button edge;

    public void checkNeighbour(Player type) //Checks if the player has an ajacent settlement
    {
        if (type.playerType == "player") //If the hedge belongs to the player
        {
            for (int i = 0; i < neighbourIntersections.Count; i++) //Loop through the ajacent intersections
            {
                if (neighbourIntersections[i].settlement == "playerSettlement" || neighbourIntersections[i].settlement == "playerCity") //If it is owned by a player
                {
                    hasPlayerNeighbour = true; 
                }
            }
        }

        if (type.playerType == "ai") //Checks if the AI has an ajacent settlement
        {
            for (int i = 0; i < neighbourIntersections.Count; i++) //If the hedge belongs to the AI
            {
                if (neighbourIntersections[i].settlement == "aiSettlement" || neighbourIntersections[i].settlement == "aiCity") //If it is owned by an AI
                {
                    hasAINeighbour = true;
                }
            }
        }
    }

    public void clicked() // Function called when player clicks the edge
    {
        checkNeighbour(humanPlayer); //Checks if the player has an ajacent settlement
        if (road == "none") // If no one owns a road on this edge
        {
            if (game.gameTurn == "player" && humanPlayer.lumber >= 1 && humanPlayer.bricks >= 1 && hasPlayerNeighbour == true) // If it is the players turn and they have the resources
            {
                 buildRoad(humanPlayer); // Build a road
            }

            else if (game.roadSetup == true || humanPlayer.roadBuilding == true) // Else if it is the setup phase or if the plaeyr has played road building
            {
                 buildRoad(humanPlayer); // Build a road
            }

        }
    }

    public void roadBuilding() // Function that deals with the road building development card
    {
        humanPlayer.roadBuildingCount++; // Tracks how many roads placed
        if(humanPlayer.roadBuildingCount == 2) // If the player has placed two raods
        {
            humanPlayer.roadBuilding = false; //Disable road building
        }
    }

    public void buildRoad(Player type) // Function the build a road, passes the plyaer through the argument
    {
        if (type.playerType == "player") // If a player is building
        {
            road = "player"; // Set the road ownership to player
            edge.image.color = Color.green; //Change the road colour to the player's colour     
        }

        else if (type.playerType == "ai") // If the ai is building
        {
            road = "ai"; // Set the ownership to the AI
            edge.image.color = Color.red; // Change the road colour to rhe AI's colour
        }

        if (type.roadBuilding == false && game.roadSetup == false) //If it is not setup or roadbuilding charge the player resoruces to build
        {
            type.lumber = type.lumber - 1;
            type.bricks = type.bricks - 1;
        }

        else if (humanPlayer.roadBuilding == true) //If the road building development card was played 
        {
            humanPlayer.roadBuildingCount++; // Counts how many roads were placed
            if (humanPlayer.roadBuildingCount == 2) //If two roads have been placed
            {
                dev.endRoadBuilding(); // End the development card effect
            }
        }

        else if (game.roadSetup == true && game.gameTurn == "player") // If it is the setup phase and it is the plaeyrs turn
        {
            game.setupEdgePointsChosen++; // Tracks the number of roads palced
            game.gameTurn = "ai"; // Sets the turn to the AI
            aiScript.createRoad(); // AI places its setup road
            game.gameTurn = "player"; // Sets the game turn to player
            if (game.setupEdgePointsChosen == 2) // If two raods have been placed
            {
                diceroll.produce(); //Produce resoruces for the start of the game
                announcements.text = "Your Turn"; // Tells the player its their turn
                game.roadSetup = false; 
                game.setup = false; // Ends setup
            }
        }

    }
}
