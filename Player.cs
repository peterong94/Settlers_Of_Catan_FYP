using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player class is mainly used to store the players resoruces and information about the player
    public string playerType;
    public int victoryPoints;
    public int knightsPlayed;
    public int roadsOwned;
    public bool mostRoads, largestArmy;
    public bool yearOfPlenty, roadBuilding, monopoly, devleopInProgress, moveRobber, trading, threeToOneTrade;
    public int chosenResources, roadBuildingCount;
    public int lumber, wool, grain, bricks, ore, knights, yearOfPlentys, roadBuildings, monopolies;
    public bool hasRolled;

    public bool checkResources(int count) // Checks if the player has resources equal to the number passed through argument
    {
        if(lumber >= count)
        {
            return true;
        }

        else if (wool >= count)
        {
            return true;
        }

        else if (grain >= count)
        {
            return true;
        }

        else if (bricks >= count)
        {
            return true;
        }

        else if (ore >= count)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

}
