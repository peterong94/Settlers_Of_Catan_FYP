using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diceroll : MonoBehaviour
{
    public int die1Result, die2Result, result;
    public Sprite side1, side2, side3, side4, side5, side6;
    public Image firstDie, secondDie;
    public Text announcement;
    public Player humanPlayer;
    public Player aiPlayer;
    public Robber robber;
    public AIScript aiScript;
    public Game game;
    public int playerResources, aiResources;

    // Function that rolls the dice when the player clicks the roll button or when it is called by the AI
    public void roll()
    {
        if (humanPlayer.hasRolled == false || game.gameTurn == "ai") // Checks if the player hasnt rolled this turn or if it is the AIs turn
        {
            System.Random r = new System.Random();
            die1Result = r.Next(1, 7); // Result for the first die
            rollDie(die1Result, firstDie); // Sets the UI Image to show the result
            die2Result = r.Next(1, 7); //Result for the second die
            rollDie(die2Result, secondDie);
            result = die1Result + die2Result; // Gets the final result
            if (result != 7) 
            {
                produce(); // Produces resoruces dependant on settlement placement
            }
            else if (game.gameTurn == "player") // If the player rolls a 7
            {
                humanPlayer.moveRobber = true; // Allow the player to place the robber
                announcement.text = "Click a hex to move the robber";
                humanPlayer.hasRolled = true; // Sets the player to having rolled this turn
            }
            else if (game.gameTurn == "ai") // If the AI rolls a 7
            {
                aiScript.moveRobber(); // Call the fucntion to move the robber
            }
        }
    }

    // Function that changes the die image to reprsent the number rolled using a switch statement
    public void rollDie(int randomnumber, Image image)
    {
        switch (randomnumber)
        {
            case 1:
                image.sprite = side1;
                break;
            case 2:
                image.sprite = side2;
                break;
            case 3:
                image.sprite = side3;
                break;
            case 4:
                image.sprite = side4;
                break;
            case 5:
                image.sprite = side5;
                break;
            case 6:
                image.sprite = side6;
                break;
            default:
                break;
        }
    }

    // function that produces resources dependant on the result of the roll
    public void produce()
    {
        for (int j = 0; j < 18; j++) // Loops through equal to the number of hexes
        {
            if((game.hexes[j].diceNumber == result && game.hexes[j].hasRobber == false) || game.setup == true) // If the result equals the number of the token on the hex and there is no robber present
            {
                for (int i = 0; i < 5; i++) // Loops through each intersection 
                {
                    switch (game.hexes[j].neighbourIntersections[i].settlement) // Checks to see if the intersection has a settlement or city
                    {
                        case "none":
                            break;
                        case "playerSettlement": // A settlement gives one of the resoruce ajacent to it
                            playerResources++;
                            break;
                        case "playerCity": // A city gives two of the resoruce ajacent to it
                            playerResources = playerResources + 2; 
                            break;
                        case "aiSettlement":
                            aiResources = aiResources + 1;
                            break;
                        case "aiCity":
                            aiResources = aiResources + 2;
                            break;
                        default:
                            break;
                    }
                }

                switch (game.hexes[j].tileType) // Checks the tile type and gives resoruces dependant on the type of tile 
                {
                    case "lumber":
                        humanPlayer.lumber = humanPlayer.lumber + playerResources;
                        aiPlayer.lumber = aiPlayer.lumber + aiResources;
                        break;
                    case "wool":
                        humanPlayer.wool = humanPlayer.wool + playerResources;
                        aiPlayer.wool = aiPlayer.wool + aiResources;
                        break;
                    case "grain":
                        humanPlayer.grain = humanPlayer.grain + playerResources;
                        aiPlayer.grain = aiPlayer.grain + aiResources;
                        break;
                    case "bricks":
                        humanPlayer.bricks = humanPlayer.bricks + playerResources;
                        aiPlayer.bricks = aiPlayer.bricks + aiResources;
                        break;
                    case "ore":
                        humanPlayer.ore = humanPlayer.ore + playerResources;
                        aiPlayer.ore = aiPlayer.ore + aiResources;
                        break;
                }

                playerResources = 0; // Resets the values of the respruces beign kept track of
                aiResources = 0;
            }
        }
        
    }
}
