using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public string gameTurn;
    public bool setup, settlementSetup, roadSetup;
    public Player humanPlayer;
    public Player aiPlayer;
    public Image playerLongestRoad, playerLargestArmy, aiLongestRoad, aiLargestArmy;
    public Sprite tick;
    public Text announcements, playerVictoryPoints, playerStandingArmy, playerNoOfRoads, playerLumber, playerWool, playerGrain, playerBricks, playerOre, aiVictoryPoints, aiStandingArmy, aiNoOfRoads;
    public int endgame, setupIntersectionPointsChosen, setupEdgePointsChosen;
    public List<Hex> hexes = new List<Hex>();
    public List<Intersection> intersections = new List<Intersection>();
    public List<Edge> edges = new List<Edge>();

    // Start is called before the first frame update
    void Start()
    {
        setup = true; //Starts the setup phase
        settlementSetup = true;
        gameTurn = "player"; //Sets the turn to the players turn
        announcements.text = "Steup: Choose two starting settlements"; 
    }

    // Update is called once per frame
    void Update()
    {
        // These check for vicotry point coniditions and update the UI when they change
        if (humanPlayer.knightsPlayed >= 3 && aiPlayer.largestArmy == false && humanPlayer.largestArmy == false)
        {
            humanPlayer.largestArmy = true;
            humanPlayer.victoryPoints = humanPlayer.victoryPoints + 2;
            playerLargestArmy.overrideSprite = tick;
        }

        if (aiPlayer.knightsPlayed >= 3 && humanPlayer.largestArmy == false && aiPlayer.largestArmy == false)
        {
            aiPlayer.largestArmy = true;
            aiPlayer.victoryPoints = aiPlayer.victoryPoints + 2;
            aiLargestArmy.overrideSprite = tick;
        }

        if (humanPlayer.roadsOwned >= 5 && aiPlayer.mostRoads == false && humanPlayer.mostRoads == false)
        {
            humanPlayer.mostRoads = true;
            humanPlayer.victoryPoints = humanPlayer.victoryPoints + 2;
            playerLongestRoad.overrideSprite = tick;
        }

        if (aiPlayer.roadsOwned >= 5 && aiPlayer.mostRoads == false && humanPlayer.mostRoads == false)
        {
            aiPlayer.mostRoads = true;
            aiPlayer.victoryPoints = aiPlayer.victoryPoints + 2;
            aiLongestRoad.overrideSprite = tick;
        }

        if (humanPlayer.victoryPoints == 10)
        {
            announcements.text = "You are the Winner!";
        }

        if (aiPlayer.victoryPoints == 10)
        {
            announcements.text = "You have Lost!";
        }

        // These change the UI text dependant on the values of differnt game elements
        playerVictoryPoints.text = humanPlayer.victoryPoints.ToString();
        playerStandingArmy.text = humanPlayer.knightsPlayed.ToString();
        playerNoOfRoads.text = humanPlayer.roadsOwned.ToString();
        playerLumber.text = humanPlayer.lumber.ToString();
        playerWool.text = humanPlayer.wool.ToString();
        playerGrain.text = humanPlayer.grain.ToString();
        playerBricks.text = humanPlayer.bricks.ToString();
        playerOre.text = humanPlayer.ore.ToString();
        aiVictoryPoints.text = aiPlayer.victoryPoints.ToString();
        aiStandingArmy.text = aiPlayer.knightsPlayed.ToString();
        aiNoOfRoads.text = aiPlayer.roadsOwned.ToString();
    }
}
