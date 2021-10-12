using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this class inherits from class Pile and
// represents the deck in the upper right corner
//
// at the beginning of the game the generated cards 
// are transported here and shuffled
namespace Sol
{
	public class pileDeck : Pile
	{

		//
		// public pileDeck
		//
		// Constructor method
		//

		public pileDeck(Vector3 position, string name) : base(position, name)
		{
			// inherits from class Pile
			// add a component Turndeck to this pile
			// Turndeck is used to determine whether a pile is empty or not.
			TurnDeck turnDeck = (TurnDeck)this.gameobject.AddComponent<TurnDeck>();
		}

		//
		// public void shuffle()
		//
		// this method shuffles the cards
		//

		public void shuffle()
		{
			// for a hundred times
			for (int i = 0; i < 100; i++)
			{
				// get two random cards
				int IndexA = Random.Range(0, cards.Count);
				int IndexB = Random.Range(0, cards.Count);
				// perform a swap (Dreieckstausch)
				Card temp = cards[IndexA];
				cards[IndexA] = cards[IndexB];
				cards[IndexB] = temp;
			}
		}
	}
}