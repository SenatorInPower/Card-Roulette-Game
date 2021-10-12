using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this class inherits from class Pile and
// represents the each of the seven decks on the table
//
// at the beginning of the game the generated cards 
// are transported here from the drawDeck
namespace Sol
{
	public class pileTable : Pile
	{

		public pileTable(Vector3 position, string name) : base(position, name)
		{
			// Inherits all stuff from Pile	
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