using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this class inherits from class Pile and
// represents the deck in the upper right corner
//
// each card clicked on th edrawdeck is transported here
// and made visible
namespace Sol
{
	public class pileWaste : Pile
	{

		public pileWaste(Vector3 position, string name) : base(position, name)
		{
			// Inherits all stuff from pile class
			texture = Resources.Load("Textures/frame1") as Texture2D;
			// set the texture
			gameobject.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
		}



		//
		// public virtual void drawDeck()
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
					//fY += -0.26f;
				}
			}
		}


	}
}