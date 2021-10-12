using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// This Class represents the Pile
// every other pile class inherits this class
namespace Sol
{
	public class Pile
	{

		// an objecto to access the pile
		public GameObject gameobject;
		// a pile may have a texture object
		public Texture2D texture;

		// Cards are stored in a List	  
		protected List<Card> cards = new List<Card>();


		//
		// public Pile(Vector3, string)
		//
		// Constructor method of this class
		// 
		// Parameters: position and name
		//

		public Pile(Vector3 position, string name)
		{
			// Instantiate the pile object
			gameobject = (GameObject.Instantiate(Resources.Load("Prefabs/Pile")) as GameObject);
			// set the objects name
			gameobject.name = name;
			// load the front and backside graphics
			texture = Resources.Load("Textures/frame2") as Texture2D;
			// set the texture
			gameobject.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
			// set the position
			gameobject.transform.position = position;
		}



		// 
		// public void addCard(Card)
		//
		// Add new Card to the Pile
		//

		public void addCard(Card card)
		{
			cards.Add(card);
			card.setPile(this);
		}



		//
		// public void addCards(List<Card>)
		//
		// Add a list of cards to the pile
		//

		public void addCards(List<Card> addList)
		{
			foreach (Card card in addList)
			{
				cards.Add(card);
				card.setPile(this);
			}
		}



		//
		// public Card getCard()
		//
		// get the last card of the list
		// card will be removed from list
		// and returned to caller
		//

		public Card getCard()
		{
			// does pile contain at least one card
			// then return this card
			if (cards.Count != 0)
			{
				Card card = cards[cards.Count - 1];
				cards.RemoveAt(cards.Count - 1);
				return card;
			}
			// else return null
			else
				return null;
		}



		//
		// public Card getTopCard()
		//
		// get the first card of the list
		// card will be removed from list
		// and returned to caller
		//

		public Card getTopCard()
		{
			Card card = cards[0];
			cards.RemoveAt(0);
			return card;
		}



		//
		// public void removeCards()
		//
		// remove all cards from a certain position 
		// to the end of the list
		//

		public void removeCards()
		{
			while (cards.Count > 0)
			{
				cards.RemoveAt(cards.Count - 1);
			}
		}



		//
		// public void removeCard()
		//
		// remove a certain card from the list
		//

		public void removeCard(Card card)
		{
			cards.Remove(card);
		}


		//
		// public List<Card> removeToEnd()
		//
		// remove all cards from a certain position 
		// to the end of the list
		// return the list to the caller
		//

		public List<Card> removeToEnd(Card card)
		{
			// this list will conatins all cards to remove
			List<Card> remove = new List<Card>();

			// set starting- and end-index
			int lastIndex = cards.Count - 1;
			int startIndex = cards.IndexOf(card);

			// copy all elements to remove in a new list
			for (int x = startIndex; x <= lastIndex; x++)
			{
				remove.Add(cards[x]);
			}

			// remove the cards from the original list
			foreach (Card c in remove)
			{
				cards.Remove(c);
			}
			// return list to the caller
			return remove;
		}



		//
		// public int count()
		//
		// get number of cards in List
		//

		public int count()
		{
			return cards.Count;
		}



		//
		// public Card firstCard()
		//
		// get the first card of pile
		// (if present)
		//

		public Card firstCard()
		{
			if (cards.Count != 0)
			{
				return cards[0];
			}
			else
				return null;
		}



		//
		// public Card lastCard()
		//
		// get the last card of pile
		// (if present)
		//

		public Card lastCard()
		{
			if (cards.Count != 0)
			{
				return cards[cards.Count - 1];
			}
			else
				return null;
		}



		//
		// public List<Card> getCardList()
		// 
		// deliver all cards to the caller
		//

		public List<Card> getCardList()
		{
			return cards;
		}



		//
		// public void Clear()
		//
		// remove all cards from the list
		//

		public void Clear()
		{
			cards.Clear();
		}



		//
		// public virtual void drawDeck()
		//
		// this method realizes the apperance of the pile
		// change (override) this method if you want to change
		// the game apperance
		//

		public virtual void drawDeck()
		{
			float fX = gameobject.transform.position.x;
			float fY = gameobject.transform.position.y;
			float fZ = gameobject.transform.position.z;

			using (List<Card>.Enumerator enumerator = cards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Card current = enumerator.Current;
					fZ += -0.001f;
					current.gameobject.transform.position = new Vector3(fX, fY, fZ);
				}
			}
		}



		//
		// public string getPileName()
		//
		// each pile has a name (necessary if a pile is clicked)
		//

		public string getPileName()
		{
			return gameobject.name.ToString();
		}
	}
}