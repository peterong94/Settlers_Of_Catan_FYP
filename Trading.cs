using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trading : MonoBehaviour
{
    public Player humanPlayer;
    public Text announcement;
    public Resources resources;
    public Game game;
    public bool hasPort;

    public void tradeThreeToOne()
    {
        checkPort("threeToOne"); // Checks if a player has settled next to a port
        if (humanPlayer.checkResources(3) == true && hasPort == true) // Checks the player has enough resources 
        {
            announcement.text = "Pick a resoruce to trade away";
            humanPlayer.threeToOneTrade = true; // Setups up the player to trade
            resources.spritesOn(); // Changes the sprites to coloured ones to indicate the player can click them
            hasPort = false;
        }
    }

    public void tradeLumber()
    {
        checkPort("lumber");
        if (humanPlayer.lumber >= 2 && hasPort == true)
        {
            announcement.text = "Pick a resoruce to trade for two lumber";
            humanPlayer.lumber = humanPlayer.lumber - 2;
            humanPlayer.trading = true;
            resources.spritesOn();
            hasPort = false;
        }
    }

    public void tradeWool()
    {
        checkPort("wool");
        if (humanPlayer.wool >= 2 && hasPort == true)
        {
            announcement.text = "Pick a resoruce to trade for two wool";
            humanPlayer.wool = humanPlayer.wool - 2;
            humanPlayer.trading = true;
            resources.spritesOn();
            hasPort = false;
        }
    }

    public void tradeGrain()
    {
        checkPort("grain");
        if (humanPlayer.grain >= 2 && hasPort == true)
        {
            announcement.text = "Pick a resoruce to trade for two wool";
            humanPlayer.grain = humanPlayer.grain - 2;
            humanPlayer.trading = true;
            resources.spritesOn();
            hasPort = false;
        }
    }

    public void tradeBricks()
    {
        checkPort("bricks");
        if (humanPlayer.bricks >= 2 && hasPort == true)
        {
            announcement.text = "Pick a resoruce to trade for two wool";
            humanPlayer.bricks = humanPlayer.bricks - 2;
            humanPlayer.trading = true;
            resources.spritesOn();
            hasPort = false;
        }
    }

    public void tradeOre()
    {
        checkPort("ore");
        if (humanPlayer.ore >= 2 && hasPort == true)
        {
            announcement.text = "Pick a resoruce to trade for two wool";
            humanPlayer.ore = humanPlayer.ore - 2;
            humanPlayer.trading = true;
            resources.spritesOn();
            hasPort = false;
        }
    }

    public void checkPort(string type)
    {
        switch (type) //Depedning on the type of trading will check specific settlements to see if the player settled there for them to access trading
        {
            case "threeToOne":
                if (game.intersections[0].settlement == "playerSettlement" || game.intersections[0].settlement == "playerCity" || game.intersections[1].settlement == "playerSettlement" || game.intersections[1].settlement == "playerCity")
                {
                    hasPort = true;
                }
                else if (game.intersections[6].settlement == "playerSettlement" || game.intersections[6].settlement == "playerCity" || game.intersections[14].settlement == "playerSettlement" || game.intersections[14].settlement == "playerCity" || game.intersections[15].settlement == "playerSettlement" || game.intersections[15].settlement == "playerCity")
                {
                    hasPort = true;
                }
                else if (game.intersections[26].settlement == "playerSettlement" || game.intersections[26].settlement == "playerCity" || game.intersections[37].settlement == "playerSettlement" || game.intersections[37].settlement == "playerCity")
                {
                    hasPort = true;
                }
                else if (game.intersections[47].settlement == "playerSettlement" || game.intersections[47].settlement == "playerCity" || game.intersections[48].settlement == "playerSettlement" || game.intersections[48].settlement == "playerCity")
                {
                    hasPort = true;
                }
                break;

            case "lumber": 
                if (game.intersections[50].settlement == "playerSettlement" || game.intersections[50].settlement == "playerCity" || game.intersections[51].settlement == "playerSettlement" || game.intersections[51].settlement == "playerCity" || game.intersections[52].settlement == "playerSettlement" || game.intersections[52].settlement == "playerCity")
                {
                    hasPort = true;
                }
                break;

            case "wool":
                if (game.intersections[3].settlement == "playerSettlement" || game.intersections[3].settlement == "playerCity" || game.intersections[4].settlement == "playerSettlement" || game.intersections[4].settlement == "playerCity" || game.intersections[5].settlement == "playerSettlement" || game.intersections[5].settlement == "playerCity")
                {
                    hasPort = true;
                }
                break;

            case "grain":
                if (game.intersections[27].settlement == "playerSettlement" || game.intersections[27].settlement == "playerCity" || game.intersections[28].settlement == "playerSettlement" || game.intersections[28].settlement == "playerCity" || game.intersections[38].settlement == "playerSettlement" || game.intersections[38].settlement == "playerCity")
                {
                    hasPort = true;
                }
                break;

            case "bricks":
                if (game.intersections[45].settlement == "playerSettlement" || game.intersections[45].settlement == "playerCity" || game.intersections[46].settlement == "playerSettlement" || game.intersections[46].settlement == "playerCity" || game.intersections[53].settlement == "playerSettlement" || game.intersections[53].settlement == "playerCity")
                {
                    hasPort = true;
                }
                break;

            case "ore":
                if (game.intersections[7].settlement == "playerSettlement" || game.intersections[7].settlement == "playerCity" || game.intersections[16].settlement == "playerSettlement" || game.intersections[16].settlement == "playerCity" || game.intersections[17].settlement == "playerSettlement" || game.intersections[17].settlement == "playerCity")
                {
                    hasPort = true;
                }
                break;

            default:
                break;
        }
    }
}
