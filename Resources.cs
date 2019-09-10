using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    public Player humanPlayer;
    public Player aiPlayer;
    public Text announcements;
    public Development dev;
    public Sprite lumberSprite, woolSprite, grainSprite, oreSprite, bricksSprite, noLumberSprite, noWoolSprite, noGrainSprite, noOreSprite, noBricksSprite;
    public Button lumberButton, woolButton, grainButton, bricksButton, oreButton;
    public bool stealing;

    // Set of functions that deal with when a resoruce button is pressed
    public void lumber()
    {
        humanPlayer.lumber = alterResources(humanPlayer.lumber, aiPlayer.lumber); // Changes the players resoruces 
        if(stealing == true)
        {
            aiPlayer.lumber = 0;
            stealing = false;
        }
    }

    public void wool()
    {
        humanPlayer.wool = alterResources(humanPlayer.wool, aiPlayer.wool);
        if (stealing == true)
        {
            aiPlayer.wool = 0;
            stealing = false;
        }
    }

    public void grain()
    {
        humanPlayer.grain = alterResources(humanPlayer.grain, aiPlayer.grain);
        if (stealing == true)
        {
            aiPlayer.grain = 0;
            stealing = false;
        }
    }

    public void bricks()
    {
        humanPlayer.bricks = alterResources(humanPlayer.bricks, aiPlayer.bricks);
        if (stealing == true)
        {
            aiPlayer.bricks = 0;
            stealing = false;
        }
    }

    public void ore()
    {
        humanPlayer.ore = alterResources(humanPlayer.ore, aiPlayer.ore);
        if (stealing == true)
        {
            aiPlayer.ore = 0;
            stealing = false;
        }
    }

    public int alterResources(int humanResource, int aiResource) //
    {
        if (humanPlayer.yearOfPlenty == true) // If year of plenty is played
        {
            humanResource++;
            humanPlayer.chosenResources++; // Tracks how many resoruces generated
            if (humanPlayer.chosenResources == 2)
            {
                dev.endYearOfPlenty();
                spritesOff();
                humanPlayer.chosenResources = 0;
                return humanResource;
            }
        }

        if (humanPlayer.monopoly == true) //If monopoly is played
        {
            if (aiResource > 0)
            {
                humanResource = humanResource + aiResource;
                aiResource = 0;
                stealing = true;
                return humanResource;
            }
            dev.endMonopoly();
            spritesOff();

        }

        if (humanPlayer.trading == true) // If trading 
        {
            humanResource++;
            humanPlayer.trading = false;
            announcements.text = " ";
            spritesOff();
            return humanResource;

        }

        if (humanPlayer.threeToOneTrade == true) // If ttrading three to one
        {
            humanResource = humanResource - 3;
            humanPlayer.trading = true;
            announcements.text = "Choose a resource to gain";
            return humanResource;
        }

        return humanResource;
    }

    public void spritesOn() // Changes the sprites to colour to show that player can click them
    {
        lumberButton.image.overrideSprite = lumberSprite;
        woolButton.image.overrideSprite = woolSprite;
        grainButton.image.overrideSprite = grainSprite;
        bricksButton.image.overrideSprite = bricksSprite;
        oreButton.image.overrideSprite = oreSprite;
    }

    public void spritesOff() // Cahnges the sprites back to greyscale
    {
        lumberButton.image.overrideSprite = noLumberSprite;
        woolButton.image.overrideSprite = noWoolSprite;
        grainButton.image.overrideSprite = noGrainSprite;
        bricksButton.image.overrideSprite = noBricksSprite;
        oreButton.image.overrideSprite = noOreSprite;
    }

}
