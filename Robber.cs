using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robber : MonoBehaviour
{
    public Player humanPlayer;
    public Player aiPlayer;
    public Text announcement;
    public Game game;
    public int test;

    public void moveRobber() //Loops through every hex and intersection ajacent to that hex then steals resources if a settlement is there
    {
        for (int i = 0; i < game.hexes.Count; i++)
        {
            if (game.hexes[i].hasRobber == true)
            {
                for (int j = 0; j < game.hexes[i].neighbourIntersections.Count; j++)
                {
                    if(game.hexes[i].neighbourIntersections[j].settlement == "aiSettlement" || game.hexes[i].neighbourIntersections[j].settlement == "aiCity")
                    {
                        stealResource(humanPlayer, aiPlayer);
                        break;
                    }
                }

                break;
            }
        }
    }

    public void stealResource(Player initiator, Player victim)
    {
        if (victim.checkResources(1) == true) // If the player being stolen from has at least one resource
        {
            bool stolen = false;
            System.Random r = new System.Random();
            while (stolen == false) // While a resource hasnt been stolen
            {
                int chosenResource;
                chosenResource = r.Next(1, 5);
                switch (chosenResource) // Steals a random resource
                {
                    case 1:
                        if (victim.lumber > 0)
                        {
                            victim.lumber--;
                            initiator.lumber++;
                            stolen = true;
                        }
                        break;

                    case 2:
                        if (aiPlayer.wool > 0)
                        {
                            victim.wool--;
                            initiator.wool++;
                            stolen = true;
                        }
                        break;

                    case 3:
                        if (aiPlayer.grain > 0)
                        {
                            victim.grain--;
                            initiator.grain++;
                            stolen = true;
                        }
                        break;

                    case 4:
                        if (aiPlayer.bricks > 0)
                        {
                            victim.bricks--;
                            initiator.bricks++;
                            stolen = true;
                        }
                        break;

                    case 5:
                        if (aiPlayer.ore > 0)
                        {
                            victim.ore--;
                            initiator.ore++;
                            stolen = true;
                        }
                        break;

                   default:
                       break;
                }
            }           
        }
    }

}
