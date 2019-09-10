using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public List<string> deck = new List<string>(); //Original deck
    public List<string> shuffledDeck = new List<string>(); //Temporary deck used for shuffling 
    public Button knight, yearOfPlenty, monopoly, roadBuilding; //Buttons declared so that their sprites can be changed
    public Sprite gotKnight, gotYearOfPlenty, gotMonopoly, gotRoadBuilding, noKnight, noYearOfPlenty, noMonopoly, noRoadBuilding; //Both the sprites for when cards are available and when not
    public Player humanPlayer; //Instance of the player class, assigned in Unity as the human player

    // Start is called before the first frame update
    void Start()
    {
        populateDeck(); //Begins the game by populating the deck with cards
        shuffle(); //And then shuffling the deck
    }

    // Update is called once per frame
    void Update()
    {
        //These if statments check if the player has a particular type of development card left, if they have not it changes the sprite to grey to indicate this
        if (humanPlayer.knights == 0)
        {
            knight.image.overrideSprite = noKnight;
        }

        if (humanPlayer.yearOfPlentys == 0)
        {
            yearOfPlenty.image.overrideSprite = noYearOfPlenty;
        }

        if (humanPlayer.monopolies == 0)
        {
            monopoly.image.overrideSprite = noMonopoly;
        }

        if (humanPlayer.roadBuildings == 0)
        {
            roadBuilding.image.overrideSprite = noRoadBuilding;
        }
    }

    void shuffle()
    {
        System.Random r = new System.Random();
        while (deck.Count > 0)
        {
            int randomIndex;
            randomIndex = r.Next(0, deck.Count); //Choose a random card in the deck
            shuffledDeck.Add(deck[randomIndex]); //Add card to new deck
            deck.RemoveAt(randomIndex); //Remove the card from the original deck
        }

        while(shuffledDeck.Count > 0) //Puts all the cards from the shuffled deck back into the original deck
        {
            int deckIndex = 0;
            deck.Add(shuffledDeck[deckIndex]);
            shuffledDeck.Remove(shuffledDeck[deckIndex]);
            deckIndex++;
        }
    }

    public void draw()
    {
        //Repopulates and shuffles the deck if there are no cards left
        if (deck.Count == 0)
        {
            populateDeck();
            shuffle();
        }

        //Checks if the player can afford a development card
        if (humanPlayer.ore > 0 && humanPlayer.wool > 0 && humanPlayer.grain > 0)
        {

            //Removes the cost of a devleopment card from the players resources
            humanPlayer.ore--; 
            humanPlayer.wool--;
            humanPlayer.grain--;

            //Switch checks the first element for what type of card it is by checking the string 
            switch (deck[0])
            {
                case "knight":
                    knight.image.overrideSprite = gotKnight; //changes the button showing that the devleopment card is playable
                    humanPlayer.knights++; //Adds to that type of devleopment card to the players total in the player class
                    deck.Remove(deck[0]); //Removes the first card in the deck
                    break;
                case "Road Building":
                    roadBuilding.image.overrideSprite = gotRoadBuilding;
                    humanPlayer.roadBuildings++;
                    deck.Remove(deck[0]);
                    break;
                case "Year of Plenty":
                    yearOfPlenty.image.overrideSprite = gotYearOfPlenty;
                    humanPlayer.yearOfPlentys++;
                    deck.Remove(deck[0]);
                    break;
                case "Monopoly":
                    monopoly.image.overrideSprite = gotMonopoly;
                    humanPlayer.monopolies++;
                    deck.Remove(deck[0]);
                    break;
                default:
                    break;
            }
        }

    }

    void populateDeck()
    {
        //Adds 14 Knight cards to the deck
        for (int i = 0; i < 13; i++)
        {
            deck.Add("knight");
        }

        //Adds 2 Road building, Year of Plenty and monopoly cards to the deck
        for (int i = 0; i < 2; i++)
        {
            deck.Add("Road Building");
            deck.Add("Year of Plenty");
            deck.Add("Monopoly");
        }
    }
}
