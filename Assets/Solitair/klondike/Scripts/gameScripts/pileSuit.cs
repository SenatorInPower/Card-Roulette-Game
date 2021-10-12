using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this class inherits from class Pile and
// represents the four decks to place the cards on
//
namespace Sol
{
	public class pileSuit : Pile
	{

		// type of card
		// cards layed out on a suitpile
		// must be the same suit

		public enum Suit
		{
			diamond = 0,
			heart = 1,
			spade = 2,
			club = 3,
		};

		public Suit suit;

		public Suit hasSuit
		{
			get { return suit; }
		}



		//
		// public pileSuit
		//
		// Constructor method
		//

		public pileSuit(Vector3 position, string name, string tex, int suit) : base(position, name)
		{
			// Inherits code from class Pile
			// load the front and backside graphics
			texture = Resources.Load("Textures/" + tex) as Texture2D;
			// set the texture
			gameobject.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
			// set the suit;
			this.suit = (Suit)suit;
		}

	}
}