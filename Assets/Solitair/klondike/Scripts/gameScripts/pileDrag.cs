using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this class inherits from class Pile and
// represents the deck for dragging cards around
namespace Sol
{
	public class pileDrag : Pile
	{

		// where are the card dragged from?
		// if mouse is released in inproper position,
		// the cards must be returned
		public Pile sourcePile;

		public void setSourcePile(Pile pile)
		{
			sourcePile = pile;
		}

		public Pile getSourcePile()
		{
			return sourcePile;
		}



		//
		// public pileDrag
		//
		// Constructor method
		//

		public pileDrag(Vector3 position, string name) : base(position, name)
		{
			// inherits from class Pile
			// add a component Turndeck to this pile
			// Turndeck is used to determine whether a pile is empty or not.
			// and: don't draw it
			gameobject.GetComponent<Renderer>().enabled = false;
		}



		//
		// public override void drawDeck()
		//
		// this method realizes the apperance of the pile
		// change (override) this method if you want to change
		// the game apperance
		//

		public override void drawDeck()
		{
			float fX = gameobject.transform.position.x;
			float fY = gameobject.transform.position.y;
			float fZ = gameobject.transform.position.z;

			using (List<Card>.Enumerator enumerator = cards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					fZ += -0.001f;
					Card current = enumerator.Current;
					current.gameobject.transform.position = new Vector3(fX, fY, fZ);
					if (current.isVisible())
						fY += -0.26f;
					else
						fY += -0.09f;
				}
			}
		}


	}
}