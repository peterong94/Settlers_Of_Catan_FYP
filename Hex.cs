using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hex : MonoBehaviour
{
    public string tileType;
    public int diceNumber;
    public int hexPosition;
    public bool hasRobber;
    public Image token;
    public Sprite robberToken, noRobberToken;
    public Text announcement;
    public Player humanPlayer;
    public Robber robber;
    public Game game;
    public List<Intersection> neighbourIntersections = new List<Intersection>();

    void Update()
    {
        if(hasRobber == false) // If there is no robber on the hex this will reset the token image
        {
            token.overrideSprite = noRobberToken;
        }
    }

    public void moveRobber() // Function that deals with moving the robber when clicked
    {
        if(humanPlayer.moveRobber == true) // If the player has rolled a 7 and needs to move the robber
        {
            clearRobber(); // Clear the robber
            hasRobber = true; //Set the robber to this hex
            token.overrideSprite = robberToken; // Change the token image to the robber
            robber.moveRobber(); //Deal with resoruce stealing
            humanPlayer.moveRobber = false; 
            announcement.text = " ";
        }
    }

    public void clearRobber() // Clears the robber
    {
        for (int i = 0; i < game.hexes.Count; i++) //Loops through all hexes
        {
            game.hexes[i].hasRobber = false; // Removes the robber 
        }
    }
}
