using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this class realizes the data strucuture needed for the card object
// instead of binding it to a prefab,
// it instantiates the prefab for the game
namespace Sol
{
	public class Card
	{

		// a card object consists of a game object
		public GameObject gameobject;
		// we have a front and a back view
		Texture2D front;
		Texture2D back;

		// which pile is the card on
		Pile pile;
		// is the front or back of card visible
		bool frontVisible = false;


		// Type of card
		public enum Suit
		{
			Diamond = 0,
			Heart = 1,
			Spade = 2,
			Club = 3,
			back = 4,
			leer = 5,
		};

		public Suit suit;

		public Suit hasSuit
		{
			get { return suit; }
		}

		bool cardHasScored;


		// Rang of card
		public enum Rang
		{
			As = 1,
			Zwei = 2,
			Drei = 3,
			Vier = 4,
			Fuenf = 5,
			Sechs = 6,
			Sieben = 7,
			Acht = 8,
			Neun = 9,
			Zehn = 10,
			Bube = 11,
			Dame = 12,
			Koenig = 13,
		};

		public Rang rang;

		public Rang hasRang
		{
			get { return rang; }
		}



		//
		// public bool isRed()
		//
		// this method checks if the card is red.
		// ruleset for the game:
		// a card has to lay on a card which is the other color and one rang greater
		//

		public bool isRed()
		{
			return (suit == Suit.Heart || suit == Suit.Diamond);
		}



		//
		// public Card
		//
		// Constructor of class card
		//

		public Card(int suit, int rank, GameScript gameScript)
		{
			// load the Prefab into gameObject reference
			// using GameObject.Instantiate instead of Instantiate, 
			// script doesn't have to inherit from Monobehaviour 
			gameobject = (GameObject.Instantiate(Resources.Load("Prefabs/Card")) as GameObject);
			// set the objects name
			gameobject.name = (string.Format("{0}_{1}", suit.ToString(), rank.ToString()));

			//has has not score yet
			cardHasScored = false;

			// set rand and suit
			this.suit = (Suit)suit;
			this.rang = (Rang)rank;

			// load the front and backside graphics
			string text = string.Format("Textures/Set2/{0}", this.gameobject.name);
			front = Resources.Load(text) as Texture2D;
			back = Resources.Load("Textures/hide5") as Texture2D;

			// the prefab contains a script called DragCard
			// here, a reference from this script is set to DragCard
			gameobject.GetComponent<DragCard>().setCardScript(this);
			// here, a reference from the main script is set to DragCard
			gameobject.GetComponent<DragCard>().setGameScript(gameScript);
			// set the texture
			setTexture();
		}

		
		public void setDeck(int deck)
		{
			string text = string.Format("Textures/Set{0}/{1}", deck, this.gameobject.name);
			front = Resources.Load(text) as Texture2D;
			string textBack = string.Format("Textures/hide{0}", deck);
			back = Resources.Load(textBack) as Texture2D;
			setTexture();
		}


		//
		// public void setTexture()
		//
		// If a card is flipped, the texture has to be changed
		//

		public void setTexture()
		{
			// set the corresponding graphic
			if (frontVisible)
				gameobject.GetComponent<Renderer>().material.SetTexture("_MainTex", front);
			else
				gameobject.GetComponent<Renderer>().material.SetTexture("_MainTex", back);
		}

		public void hasScored(bool score)
		{
			cardHasScored = score;
		}

		public bool didCardScore()
		{
			return cardHasScored;
		}

		//
		// public bool isVisible()
		//
		// check if card is visible
		//

		public bool isVisible()
		{
			return frontVisible;
		}



		//
		// public void setPile()
		//
		// set card to a certain pile
		//

		public void setPile(Pile pile)
		{
			this.pile = pile;
		}



		//
		// public Pile getPile()
		//
		// get current Pile and return it to caller
		//

		public Pile getPile()
		{
			return pile;
		}



		//
		// public void flip()
		//
		// turn the card from front to back
		// (or the other way around)
		//

		public void flip()
		{
			frontVisible = !frontVisible;
			setTexture();
		}
	}

}