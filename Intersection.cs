using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intersection : MonoBehaviour
{
    public string settlement = "none";
    public bool hasNeighbour;
    public List<Intersection> neighbourIntersections = new List<Intersection>();
    public List<Edge> neighbourEdges = new List<Edge>();
    public List<int> neighbourHexes = new List<int>();
    public Game game;
    public Text annoucnements;
    public Player humanPlayer;
    public Player aiPlayer;
    public AIScript aiScript;
    public Button intersection;
    public Sprite playerSettlement, playerCity, aiSettlement, aiCity;
    public Button intersectionButton;

    public void checkNeighbour() // Function that loops through ajacent intersections to see if there is a settlment there
    {

        for (int i = 0; i < neighbourIntersections.Count; i++)
        {
            if (neighbourIntersections[i].settlement == "playerSettlement" || neighbourIntersections[i].settlement == "playerCity" || neighbourIntersections[i].settlement == "aiSettlement" || neighbourIntersections[i].settlement == "aiCity")
            {
                hasNeighbour = true;
            }
        }
    }

    public void playerClicked() // Function that is run when a player clicks an intersection 
    {
        checkNeighbour(); // Check for ajacent settlements
        switch (settlement)
        {
            case "none": // If no one owns a settlement here
                // If it is the players turn, they have the resoruces and there are no ajacent settlements
                if (game.gameTurn == "player" && humanPlayer.bricks >= 1 && humanPlayer.lumber >= 1 && humanPlayer.wool >= 1 && humanPlayer.grain >= 1 && hasNeighbour == false)
                {
                    createSettlement(humanPlayer);
                }
                else if (game.settlementSetup == true) // If it is the setup phase
                {
                    createSettlement(humanPlayer);
                }

                break;

            case "playerSettlement": // If the player already has a settlement
                if (game.gameTurn == "player" && humanPlayer.ore >= 3 && humanPlayer.grain >= 2 && game.setup == false) // If it is the players turn and they have the resoruces
                {
                    createCity(humanPlayer);
                }

                break;

            default:
                break;
        }

    }

    public void createSettlement(Player type) // Takes in the player through the argument
    {
        if (type.playerType == "player") // If it is the human player
        {
            settlement = "playerSettlement"; // Human player owns the settlement
            intersection.image.overrideSprite = playerSettlement;
            type.victoryPoints = type.victoryPoints + 1; //Add victory points

        }

        if (type.playerType == "ai") // If it is the AI
        {
            settlement = "aiSettlement";
            intersection.image.overrideSprite = aiSettlement;
            type.victoryPoints = type.victoryPoints + 1;
        }

        if (game.settlementSetup == false) // If it isnt the setup remove the resoruces
        {
            type.lumber = type.lumber - 1;
            type.bricks = type.bricks - 1;
            type.wool = type.wool - 1;
            type.grain = type.grain - 1;
        }

        if(game.settlementSetup == true && game.gameTurn == "player") 
        {
            game.setupIntersectionPointsChosen++; // Tracks the number of settlements placed
            game.gameTurn = "ai";
            aiScript.settle(); // Settle aI player
            game.gameTurn = "player";
            if (game.setupIntersectionPointsChosen == 2) // When two settlements have been placed
            {
                annoucnements.text = "Setup: Choose two starting roads";
                game.settlementSetup = false; // End settlement setuo
                game.roadSetup = true; // Start Road setup
            }
        }
    }

    public void createCity(Player type) // Takes in the player through the argument
    {
        if (type.playerType == "player") //If it is the human player
        {
            settlement = "playerCity";
            intersection.image.overrideSprite = playerCity;
        }

        if (type.playerType == "ai") //If iti is the AI player
        {
            settlement = "aiCity";
            intersection.image.overrideSprite = aiCity;
        }

        type.lumber = type.ore - 3; // Remove the resoruces
        type.grain = type.grain - 2;
        type.victoryPoints = type.victoryPoints + 1;
    }
}
