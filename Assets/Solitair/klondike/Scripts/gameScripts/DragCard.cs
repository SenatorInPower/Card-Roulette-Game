using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this script realizes the behaviour of the card when dragged
// the rules of the cardgame are mapped here

// Change log:
// 2015/03/01 
// set position of dragPile to (0,-3,0) to resolve drag&drop bug
namespace Sol
{
	public class DragCard : MonoBehaviour
	{

		// we are working with a raycast to match mouse position
		// to the game
		private Vector3 screenPoint;
		private Ray vRay;
		private bool bShowRay;

		// where did the drag of the card start
		Vector3 origPos;

		// this is the pile the card is curently on
		// (when dragged)
		pileDrag dragPile;

		// status: 
		// is card on the drawPile (so it cannot be dragged)
		private bool isCardOnDrawPile;
		private bool canCardBeDragged;

		// reference to the card script
		public Card cardScript;
		// reference to the main game script
		public GameScript cardGameScript;


		// used to determine double click
		int clickCounter = 0;

		//
		// public void setCardScript(Card card)
		//
		// set the reference from the card script
		//
		public void setCardScript(Card card)
		{
			cardScript = card;
		}

		// get the reference...
		public Card getCard()
		{
			return cardScript;
		}


		//
		// public void setGameScript(GameScript gameScript)
		//
		// set the reference to the main script
		//

		public void setGameScript(GameScript gameScript)
		{
			cardGameScript = gameScript;
		}


		// Use this for initialization
		void Start()
		{
			isCardOnDrawPile = false;
			canCardBeDragged = true;
			dragPile = new pileDrag(new Vector3(0, -5, 0), "PileDrag");
		}


		// This script uses a raycast to calculate where the mouse press has been released.
		// The calculation method used here only works in camera mode "orthographics"


		//
		// void Update()
		//
		// Update is obsolete. It is only used for debugging issues 
		// (so you can see the ray in the scene window)
		//

		void Update()
		{
			// Debugging-Modus
			if (this.bShowRay)
			{
				// zeichne den Ray
				Debug.DrawRay(this.vRay.origin, this.vRay.direction * 10f, Color.red);
			}

		}


		IEnumerator lockClicks()
		{
			yield return new WaitForSeconds(0.15f);
			if (clickCounter == 1)
			{
				clickCounter = 0;
				OnSingleClick();
			}
			if (clickCounter == 2)
			{
				clickCounter = 0;
				OnDoubleClick();
			}
		}


		//
		// void OnMouseUp()
		//
		// So the mouse press has been released. we now have to check if the move was valid.
		//

		void OnMouseUp()
		{
			clickCounter++;
			if (clickCounter == 1)
				StartCoroutine(lockClicks());
		}

		void OnDoubleClick()
		{
		}

		void OnSingleClick()
		{

			// deactivate debugging ray
			this.bShowRay = false;

			// did raycast hit an object ?
			RaycastHit raycastHit = default(RaycastHit);
			if (Physics.Raycast(this.vRay, out raycastHit, 5f))
			{

				// initialize some vars
				Pile hitPile = null;
				Card hitCard = null;

				// Where did user release the card? On a pile or on another card?
				cardGameScript.getTargetPile(raycastHit.collider.gameObject, out hitPile, out hitCard);
				// There are two possibilities:
				// The card can be dropped on an emtpy Pile or
				// on a not empty pile
				if (hitCard != null)
				{
					// The ray hit a card.
					// so we dropped on a non-empty pile.
					// Now get the corresponding pile
					hitPile = hitCard.getPile();
				}

				// We dropped on the waste pile
				// this is not allowed, so return all the cards
				if (hitPile.gameobject.name == "PileWaste")
				{
					if (dragPile.count() > 0)
					{
						dragPile.getSourcePile().addCards(dragPile.getCardList());
						dragPile.removeCards();
						// Now reset the position of the drag-Pile object.
						// It will else lay above the playing card and
						// prevent a further drag and drop with his collider.
						dragPile.gameobject.transform.position = new Vector3(0, -5, 0);
					}
				}

				// We dropped on the draw pile
				// this is not allowed, so return all the cards
				if (hitPile.gameobject.name == "PileDraw")
				{
					// if there are cards on the pile, this is not allowed
					if (dragPile.count() > 0)
					{
						dragPile.getSourcePile().addCards(dragPile.getCardList());
						dragPile.removeCards();
						// Now reset the position of the drag-Pile object.
						// It will else lay above the playing card and
						// prevent a further drag and drop with his collider.
						dragPile.gameobject.transform.position = new Vector3(0, -5, 0);
					}
					// user clicked on empty pile
					// so return all cards from the waste pile
					else
					{
						if (hitPile.count() > 0)
							cardGameScript.drawCard(cardGameScript.cardsToDraw);
						else
						{
							cardGameScript.turnDeck();
							// now call method from cardGamescript
						}
					}
				}

				// we dropped card(s) on on of the seven piletables
				// rules: 
				// 1) the first card on empty pile has to be a king
				// 2) any other card on the pile has to be one smaller 
				//    and other color of actual card on top
				if (hitPile.gameobject.name == "PileTable1" ||
					hitPile.gameobject.name == "PileTable2" ||
					hitPile.gameobject.name == "PileTable3" ||
					hitPile.gameobject.name == "PileTable4" ||
					hitPile.gameobject.name == "PileTable5" ||
					hitPile.gameobject.name == "PileTable6" ||
					hitPile.gameobject.name == "PileTable7")
				{
					// we dropped at least one card. so let's get started...
					if (dragPile.count() > 0)
					{
						// destination pile is empty
						// first card has to be a king
						if (hitPile.count() == 0 && dragPile.firstCard().hasRang == Card.Rang.Koenig)
						{
							hitPile.addCards(dragPile.getCardList());
							dragPile.removeCards();
							// Now reset the position of the drag-Pile object.
							// It will else lay above the playing card and
							// prevent a further drag and drop with his collider.
							dragPile.gameobject.transform.position = new Vector3(0, -5, 0);
						}
						// pile is not empty... so this is rule 2)
						else if (hitPile.count() > 0 && hitPile.lastCard().hasRang == dragPile.firstCard().hasRang + 1 &&
							hitPile.lastCard().isRed() != dragPile.firstCard().isRed() && hitPile.lastCard().isVisible())
						{
							hitPile.addCards(dragPile.getCardList());
							dragPile.removeCards();
							// Position des DragPile zurücksetzen,
							// sonst liegt er über der abgelegten Karte und verhindert
							// mit seinem Collider ein weiteres Anklicken :-(
							dragPile.gameobject.transform.position = new Vector3(0, -5, 0);
						}

					}

					// if the top card on the pile is hidden,
					// we can turn it.
					if (dragPile.count() == 0)
					{
						// turn hidden card
						if (!hitPile.lastCard().isVisible())
						{
							hitPile.lastCard().flip();
							// player is scoring
							GameObject go = Instantiate(Resources.Load("Prefabs/risingText")) as GameObject;
							go.transform.position = hitPile.gameobject.transform.position;
							go.transform.position = go.transform.position + new Vector3(-0.6f, 0, -1f);
							go.GetComponent<TextMesh>().text = "+25";
							cardGameScript.alterScore(25);
						}
					}
				}

				// we dropped a card on the suitpile
				if (hitPile.gameobject.name == "PileDiamond" ||
					hitPile.gameobject.name == "PileHeart" ||
					hitPile.gameobject.name == "PileSpade" ||
					hitPile.gameobject.name == "PileClub")
				{
					// this will only work if only one card is dragged
					if (dragPile.count() == 1)
					{
						// pile is empty, so first card has to be an ace
						if (hitPile.count() == 0 && dragPile.firstCard().hasRang == Card.Rang.As)
						{
							if (hitPile.gameobject.name.IndexOf(dragPile.firstCard().hasSuit.ToString()) >= 0)
							{
								hitPile.addCard(dragPile.getTopCard());
								Card card = hitPile.firstCard();
								dragPile.removeCards();
								// Now reset the position of the drag-Pile object.
								// It will else lay above the playing card and
								// prevent a further drag and drop with his collider.
								dragPile.gameobject.transform.position = new Vector3(0, -5, 0);
								// Player is scoring
								if (!card.didCardScore())
								{
									GameObject go = Instantiate(Resources.Load("Prefabs/risingText")) as GameObject;
									go.transform.position = hitPile.gameobject.transform.position;
									go.transform.position = go.transform.position + new Vector3(-0.6f, 0, -1f);
									go.GetComponent<TextMesh>().text = "+50";
									cardGameScript.alterScore(50);
									// make a nice sparkling particle system
									GameObject particleSystem = Instantiate(Resources.Load("Particles/Sparkles/Partikelsystem")) as GameObject;
									particleSystem.transform.position = hitPile.gameobject.transform.position;
									particleSystem.transform.position = particleSystem.transform.position + new Vector3(0, 0, -1f);
									Destroy(particleSystem, 3f);
									card.hasScored(true);
								}
							}
						}
						// pile is not empty
						// so the current card has to be one greater and the same color
						else if (hitPile.count() > 0 && hitPile.lastCard().hasRang == dragPile.firstCard().hasRang - 1 &&
								 hitPile.firstCard().hasSuit == dragPile.firstCard().hasSuit)
						{
							hitPile.addCard(dragPile.getTopCard());
							Card card = hitPile.firstCard();
							dragPile.removeCards();
							// Now reset the position of the drag-Pile object.
							// It will else lay above the playing card and
							// prevent a further drag and drop with his collider.
							dragPile.gameobject.transform.position = new Vector3(0, -3, 0);
							// player is scoring
							if (!card.didCardScore())
							{
								GameObject go = Instantiate(Resources.Load("Prefabs/risingText")) as GameObject;
								go.transform.position = hitPile.gameobject.transform.position;
								go.transform.position = go.transform.position + new Vector3(-0.6f, 0, -1f);
								go.GetComponent<TextMesh>().text = "+50";
								cardGameScript.alterScore(50);
								// make a nice sparkling particle system
								GameObject particleSystem = Instantiate(Resources.Load("Particles/Sparkles/Partikelsystem")) as GameObject;
								particleSystem.transform.position = hitPile.gameobject.transform.position;
								particleSystem.transform.position = particleSystem.transform.position + new Vector3(0, 0, -1f);
								Destroy(particleSystem, 3f);
								card.hasScored(true);
							}
						}
					}
				}


			}

			// We get only here if move was invalid
			// so return all card to the source position
			if (dragPile.count() > 0)
			{
				dragPile.getSourcePile().addCards(dragPile.getCardList());
				dragPile.removeCards();
				// Now reset the position of the drag-Pile object.
				// It will else lay above the playing card and
				// prevent a further drag and drop with his collider.
				dragPile.gameobject.transform.position = new Vector3(0, -5, 0);
			}

			// fire card game script for update
			cardGameScript.fireUpdate();

		}



		//
		// void OnMouseDown()
		//
		// Player pressed mouse
		//

		void OnMouseDown()
		{
			// Did player click on a pile
			Pile sourcePile = cardScript.getPile();

			// we clicked the waste pile
			if (sourcePile.getPileName() == "PileWaste")
			{
				// so get the first card
				dragPile.setSourcePile(sourcePile);
				dragPile.addCard(sourcePile.getCard());
			}

			if (cardGameScript.isRedrawAllowed())
			{
				// we clicked one of the suit piles
				if (sourcePile.getPileName() == "PileClub" ||
					sourcePile.getPileName() == "PileSpade" ||
					sourcePile.getPileName() == "PileHeart" ||
					sourcePile.getPileName() == "PileDiamond")
				{
					// so get the first card
					dragPile.setSourcePile(sourcePile);
					dragPile.addCard(sourcePile.getCard());
				}
			}

			// we clicked on of the seven piles on the table
			if (sourcePile.getPileName() == "PileTable1" ||
				   sourcePile.getPileName() == "PileTable2" ||
				sourcePile.getPileName() == "PileTable3" ||
				sourcePile.getPileName() == "PileTable4" ||
				sourcePile.getPileName() == "PileTable5" ||
				sourcePile.getPileName() == "PileTable6" ||
				sourcePile.getPileName() == "PileTable7")
			{
				// react to the click only if the clicked card is visible
				if (cardScript.isVisible())
				{
					// put all cards to the end in the new drag list
					dragPile.setSourcePile(sourcePile);
					dragPile.addCards(sourcePile.removeToEnd(cardScript));
				}
			}

			// the new pile is only moved if it contains one or more cards
			if (dragPile.count() > 0)
			{
				Vector3 position = gameObject.transform.position;
				origPos = position;
				position.z = -0.5f;
				gameObject.transform.position = position;
				// shoot a ray
				this.screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
				// and enable the visual debugging
				this.bShowRay = true;
			}
		}



		//
		// void OnMouseDrag()
		//
		// the mouse is dragged while button is pressed
		//

		void OnMouseDrag()
		{
			// get actual mouse position
			Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.screenPoint.z);
			// calculate screen position(mouse) to world position(game) 
			Vector3 vector2 = Camera.main.ScreenToWorldPoint(vector);
			Vector3 position = vector2;

			// do we have some cards to drag?
			// adjust the position
			if (dragPile.count() > 0)
			{
				dragPile.gameobject.transform.position = position;
				dragPile.drawDeck();
			}

			// calculate the rays's position
			this.vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 origin = this.vRay.origin;
			// Ray starts at y = 0.3f. we set the cards to y=0.5f before, so
			// the ray can't intersect the cards and deliver a wrong result.
			origin.z = -0.3f;
			this.vRay.origin = (origin);
			RaycastHit raycastHit = default(RaycastHit);
			if (Physics.Raycast(this.vRay, out raycastHit, 100f))
			{
				// no calculation or validation done,
				// but raycastHit is calculated nevertheless
			}
		}
	}
}