using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Development : MonoBehaviour
{
    public Text announcement;
    public Player humanPlayer;
    public Resources resources;

    public void yearOfPlenty() // Function called when the year of plenty button is clicked
    {
        if (humanPlayer.devleopInProgress == false && humanPlayer.yearOfPlentys > 0) // Checks to see if another devleopment card has been played and if the plyaer owns a year of plenty card
        {
            humanPlayer.yearOfPlenty = true; // Enables resoruce buttons to be pressed for the purpose of this devleopment card
            humanPlayer.devleopInProgress = true; // Enables the boolean to make sure other actions cannot be taken while development is in progress
            humanPlayer.yearOfPlentys--; // Removes the development card from the player when played
            announcement.text = "Choose any Two Resoruces"; // Updates the player using the annocument text of what to do
            resources.spritesOn(); // Makes the resoruce sprites colourful to indicate they can now be clicked
        }
    }

    public void endYearOfPlenty() // Fucntion called to end the development card effects
    {
        humanPlayer.yearOfPlenty = false;
        humanPlayer.devleopInProgress = false;
        announcement.text = " ";
    }

    public void roadBuilding() // Function called when the road building button is clicked
    {
        if (humanPlayer.devleopInProgress == false && humanPlayer.roadBuildings > 0)
        {
            humanPlayer.roadBuilding = true;
            humanPlayer.devleopInProgress = true;
            humanPlayer.roadBuildings--;
            announcement.text = "Choose two roads to place for free";
        }
    }

    public void endRoadBuilding() // Fucntion called to end the development card effects
    {
        humanPlayer.roadBuilding = false;
        humanPlayer.devleopInProgress = false;
        announcement.text = " ";
    }

    public void monopoly() // Function called when the monopoly button is clicked
    {
        if (humanPlayer.devleopInProgress == false && humanPlayer.monopolies > 0) 
        {
            humanPlayer.monopoly = true;
            humanPlayer.devleopInProgress = true;
            humanPlayer.monopolies--;
            announcement.text = "Choose a type of resource to steal";
            resources.spritesOn();
        }
    }

    public void endMonopoly() // Fucntion called to end the development card effects
    {
        humanPlayer.monopoly = false;
        humanPlayer.devleopInProgress = false;
        announcement.text = " ";
    }

    public void knight() // Function called when the knight button is clicked
    {
        if (humanPlayer.devleopInProgress == false && humanPlayer.knights > 0)
        {
            humanPlayer.knightsPlayed++;
            humanPlayer.knights--;
            humanPlayer.moveRobber = true; // Enables the boolean to let the player move the robber
            announcement.text = "Click a hex to move the robber";
        }
    }
}
