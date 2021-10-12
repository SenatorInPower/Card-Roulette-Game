using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
namespace Sol
{
	// this is the main script attached to an empty 
	// game object in the scene

	public class GameScript : MonoBehaviour
	{

		// references to the piles needed
		pileDeck deckPile;
		pileWaste wastePile;
		pileTable[] tablePile = new pileTable[7];
		pileSuit[] suitPile = new pileSuit[4];

		// a master list of all the cards
		List<Card> cardList = new List<Card>();

		// method update don't has to be called periodically
		// so we trigger an update if neccessary
		bool isUpdateNeccessary;
		// this should be clear...
		bool isGameWon;

		// Reference to text meshes for status display
		public Transform timeDisplay;
		public Transform scoreDisplay;

		// reference to this script itself
		public static GameScript instance;

		// status vars
		int score;
		int actualTime;
		float timer;
		// timer to count every second
		float timerInterval = 1;

		// which game state ae we in
		public enum GameState
		{
			playing,
			pausing,
			gameover,
			gamewon,
		};

		GameState gameState;

		// status graphics
		public Texture2D gamePaused;
		public Texture2D gameWon;
		public GameObject pauseTable;

		// Options HUD
		public GameObject optionsDialog;

		int deckSet = 1;
		[HideInInspector]
		public int cardsToDraw = 1;
		bool redrawAllowed = true;

		// Use this for initialization
		void Start()
		{
			// set the gamestate
			gameState = GameState.playing;
			// the game is not won
			isGameWon = false;
			// set a reference to this script
			instance = this;
			// generate the piles
			deckPile = new pileDeck(new Vector3(3.72408f, 2.184951f, -0.01f), "PileDraw");
			wastePile = new pileWaste(new Vector3(2.205509f, 2.184951f, -0.01f), "PileWaste");
			tablePile[6] = new pileTable(new Vector3(3.72408f, 0.2046428f, -0.01f), "PileTable7");
			tablePile[5] = new pileTable(new Vector3(2.205509f, 0.2046428f, -0.01f), "PileTable6");
			tablePile[4] = new pileTable(new Vector3(0.768375f, 0.2046428f, -0.01f), "PileTable5");
			tablePile[3] = new pileTable(new Vector3(-0.684645f, 0.2046428f, -0.01f), "PileTable4");
			tablePile[2] = new pileTable(new Vector3(-2.17084f, 0.2046428f, -0.01f), "PileTable3");
			tablePile[1] = new pileTable(new Vector3(-3.608467f, 0.2046428f, -0.01f), "PileTable2");
			tablePile[0] = new pileTable(new Vector3(-5.077943f, 0.2046428f, -0.01f), "PileTable1");
			suitPile[0] = new pileSuit(new Vector3(-5.0779f, 2.18495f, -0.01f), "PileDiamond", "frameDiamond", 0);
			suitPile[1] = new pileSuit(new Vector3(-3.6084f, 2.18495f, -0.01f), "PileHeart", "frameHeart", 1);
			suitPile[2] = new pileSuit(new Vector3(-2.1708f, 2.18495f, -0.01f), "PileSpade", "frameSpade", 2);
			suitPile[3] = new pileSuit(new Vector3(-0.6846f, 2.18495f, -0.01f), "PileClub", "frameClub", 3);
			// init the game
			initGame();
		}



		// Update is called once per frame
		void Update()
		{

			// check which game state we are in
			switch (gameState)
			{
				// we are playing
				case GameState.playing:
					// change to state pause
					//if (Input.GetKeyUp(KeyCode.P)) {
					//	Time.timeScale = 0;
					//	pauseTable.transform.position = new Vector3(0,0,-1);
					//	gameState = GameState.pausing;
					//}

					// start a new game
					//if (Input.GetKeyUp(KeyCode.Space)) {
					//	score = 0;
					//	actualTime = 0;
					//	resetGame();
					//}

					// change the deck
					//if (Input.GetKeyUp(KeyCode.S)) {
					//	deckSet = (deckSet + 1) % 2;
					//	setCardDeck(deckSet+1);
					//}

					// ok... cheat to check the win state
					//if (Input.GetKeyUp(KeyCode.Q)) {
					//	timer += 4;
					//	gameState = GameState.gamewon;
					//}

					// set timer and update HUD
					timer += Time.deltaTime;
					if (timer > timerInterval)
					{
						timer -= timerInterval;
						score--;
						actualTime++;
						scoreDisplay.GetComponent<TextMesh>().text = "Score: " + score.ToString();
						timeDisplay.GetComponent<TextMesh>().text = "Time: " + actualTime.ToString() + "s";
					}

					// do we have to update?
					if (isUpdateNeccessary)
					{
						// redraw the piles
						deckPile.drawDeck();
						wastePile.drawDeck();
						for (int i = 0; i < 7; i++)
						{
							tablePile[i].drawDeck();
						}
						for (int i = 0; i < 4; i++)
						{
							suitPile[i].drawDeck();
						}
						// reset bool
						isUpdateNeccessary = false;
					}

					// the game is won
					if (suitPile[0].count() == 13 && suitPile[1].count() == 13 &&
						suitPile[2].count() == 13 && suitPile[3].count() == 13)
					{
						timer += 4;
						gameState = GameState.gamewon;
					}
					break;

				// we are in state pause
				case GameState.pausing:
					// return to game
					//if (Input.GetKeyUp(KeyCode.P)) {
					//	Time.timeScale = 1;
					//	pauseTable.transform.position = new Vector3(0,0,10);
					//	gameState = GameState.playing;
					//}
					// reset game
					//if (Input.GetKeyUp(KeyCode.Space)) {
					//	score = 0;
					//	actualTime = 0;
					//	resetGame();
					//}
					break;

				// the game is won
				case GameState.gamewon:
					timer += Time.deltaTime;
					if (timer > timerInterval * 4)
					{
						timer -= timerInterval * 4;
						//// Instantiate four particle systems
						//GameObject particleSystem = Instantiate(Resources.Load("Prefabs/Firework")) as GameObject;
						//particleSystem.transform.position = new Vector3(-3f, -3.5f, -1f);
						//Texture2D tex = Resources.Load("Textures/Particles/club") as Texture2D;
						//particleSystem.GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
						//particleSystem.transform.Find("Explosion").GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
						//Destroy(particleSystem, 6f);
						//GameObject particleSystem2 = Instantiate(Resources.Load("Prefabs/Firework")) as GameObject;
						//particleSystem2.transform.position = new Vector3(-1.5f, -3.5f, -1f);
						//Texture2D tex2 = Resources.Load("Textures/Particles/diamond") as Texture2D;
						//particleSystem2.GetComponent<Renderer>().material.SetTexture("_MainTex", tex2);
						//particleSystem2.transform.Find("Explosion").GetComponent<Renderer>().material.SetTexture("_MainTex", tex2);
						//Destroy(particleSystem2, 6f);
						//GameObject particleSystem3 = Instantiate(Resources.Load("Prefabs/Firework")) as GameObject;
						//particleSystem3.transform.position = new Vector3(-0f, -3.5f, -1f);
						//Texture2D tex3 = Resources.Load("Textures/Particles/spade") as Texture2D;
						//particleSystem3.GetComponent<Renderer>().material.SetTexture("_MainTex", tex3);
						//particleSystem3.transform.Find("Explosion").GetComponent<Renderer>().material.SetTexture("_MainTex", tex3);
						//Destroy(particleSystem3, 6f);
						//GameObject particleSystem4 = Instantiate(Resources.Load("Prefabs/Firework")) as GameObject;
						//particleSystem4.transform.position = new Vector3(1.5f, -3.5f, -1f);
						//Texture2D tex4 = Resources.Load("Textures/Particles/heart") as Texture2D;
						//particleSystem4.GetComponent<Renderer>().material.SetTexture("_MainTex", tex4);
						//particleSystem4.transform.Find("Explosion").GetComponent<Renderer>().material.SetTexture("_MainTex", tex4);
						//Destroy(particleSystem4, 6f);
					}

					// ok.. we should be able to start a new game
					//if (Input.GetKeyUp(KeyCode.Space)) {
					//	score = 0;
					//	actualTime = 0;
					//	resetGame();
					//}
					break;

				// not yet implemented
				case GameState.gameover:
					break;
				default:
					break;
			}
		}



		//
		// void OnGUI()
		//
		// Display status information
		//

		void OnGUI()
		{
			if (gameState == GameState.pausing)
			{
				float width = Screen.width / 1920f;
				float height = Screen.height / 1080f;
				float pictureWidth = gamePaused.width * width;
				float pictureHeight = gamePaused.height * height;
				GUI.DrawTexture(new Rect((Screen.width - pictureWidth) / 2, (Screen.height - pictureHeight) / 2, pictureWidth, pictureHeight), gamePaused);
			}
			if (gameState == GameState.gamewon)
			{
				float width = Screen.width / 1920f;
				float height = Screen.height / 1080f;
				float pictureWidth = gameWon.width * width;
				float pictureHeight = gameWon.height * height;
				GUI.DrawTexture(new Rect((Screen.width - pictureWidth) / 2, (Screen.height - pictureHeight) / 2, pictureWidth, pictureHeight), gameWon);
			}
		}



		//
		// void initGame()
		//
		// The game is initiated

		void initGame()
		{
			generateCards();
			setCardDeck(deckSet + 1);
			shuffleCards();
			distributeCards();
			isUpdateNeccessary = true;
		}



		//
		// void resetGame()
		//
		// The game is resetted (player pressed space)
		//

		public void resetGame()
		{
			// reset game state
			gameState = GameState.playing;
			Time.timeScale = 1f;
			// reset status vars
			isGameWon = false;
			score = 0;
			actualTime = 0;
			// Clean the piles
			wastePile.Clear();
			deckPile.Clear();
			for (int i = 0; i < 7; i++)
			{
				tablePile[i].Clear();
			}
			for (int i = 0; i < 4; i++)
			{
				suitPile[i].Clear();
			}

			// Use the master list to reset the cards
			// (they don't have to be deleted)
			foreach (Card card in cardList)
			{
				card.hasScored(false);
				if (card.isVisible())
					card.flip();
				deckPile.addCard(card);
			}
			// shuffle and distribute
			shuffleCards();
			distributeCards();
			// trigger update
			isUpdateNeccessary = true;
		}



		//
		// void generateCards()
		//
		// this method generate 52 card object
		//

		public void generateCards()
		{
			for (int suit = 0; suit < 4; suit++)
			{
				for (int rank = 1; rank < 14; rank++)
				{
					Card card = new Card(suit, rank, this);
					// put cards in master list
					cardList.Add(card);
					// put cards in the deckpile
					deckPile.addCard(card);
				}
			}
		}



		//
		// void shuffleCards()
		//
		// this method shuffles the cards by
		// calling deckpile's shuffle method
		//

		public void shuffleCards()
		{
			deckPile.shuffle();
		}



		//
		// void distributeCards()
		//
		// take the cards from the deckpile
		// and distribute them to the seven piles
		public void distributeCards()
		{
			Card card;
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < i + 1; j++)
				{
					card = deckPile.getCard();
					tablePile[i].addCard(card);
				}
				// turn last card of pile 
				tablePile[i].lastCard().flip();
			}
			// put card on wastepile
			//card = deckPile.getCard ();
			//wastePile.addCard (card);
			//wastePile.lastCard ().flip();
			drawCard(cardsToDraw);
		}



		//
		// void drawCard
		//
		// draw a card from draw deck
		// and put it on the waste pile
		// 

		public void drawCard(int _cardsToDraw)
		{
			Card card;
			for (int i = 0; i < _cardsToDraw; i++)
			{
				card = deckPile.getCard();
				// does drawPile contain at least one card
				if (card != null)
				{
					wastePile.addCard(card);
					wastePile.lastCard().flip();
				}
			}
			isUpdateNeccessary = true;
		}


		public void allowRedraw(bool redraw)
		{
			redrawAllowed = redraw;
		}

		public bool isRedrawAllowed()
		{
			return redrawAllowed;
		}

		//
		// void setCardDeck
		//
		// Player changes card appearance in the options dialog 
		// Set new pictures to all cards
		//

		public void setCardDeck(int deck)
		{
			foreach (Card card in cardList)
			{
				card.setDeck(deck);
			}
		}

		public void setCardsToDraw(int amount)
		{
			cardsToDraw = amount;
		}

		//
		// void turnDeck
		//
		// return all cards from the waste pile 
		// to the deck pile
		//

		public void turnDeck()
		{
			Card card;
			// the card deck is empty
			// return all cards from wastepile to draw deck
			while (wastePile.count() > 0)
			{
				card = wastePile.getCard();
				card.flip();
				deckPile.addCard(card);
			}
			isUpdateNeccessary = true;
		}



		//
		// void getTargetPile
		//
		// this method is called from script DragCard
		// it is used to determine the pile clicked
		//

		public void getTargetPile(GameObject hitGameobject, out Pile destinationPile, out Card destinationCard)
		{
			destinationCard = null;
			destinationPile = null;
			// an empty pile has been clicked
			if (hitGameobject.name.IndexOf("Pile", 0) >= 0)
			{
				destinationPile = getPile(hitGameobject);
			}
			// pile contains at least one card
			else
			{
				// you can drop cards on the table outside an pile.
				// this produces an error. Solution:
				// table's collider has been disabled
				destinationCard = hitGameobject.GetComponent<DragCard>().getCard();
			}
		}



		//
		// public Pile getPile
		//
		// get pile's name and return it
		//

		public Pile getPile(GameObject hitGameobject)
		{
			if (hitGameobject.name == "PileTable1")
				return tablePile[0];
			if (hitGameobject.name == "PileTable2")
				return tablePile[1];
			if (hitGameobject.name == "PileTable3")
				return tablePile[2];
			if (hitGameobject.name == "PileTable4")
				return tablePile[3];
			if (hitGameobject.name == "PileTable5")
				return tablePile[4];
			if (hitGameobject.name == "PileTable6")
				return tablePile[5];
			if (hitGameobject.name == "PileTable7")
				return tablePile[6];
			if (hitGameobject.name == "PileWaste")
				return wastePile;
			if (hitGameobject.name == "PileDraw")
				return deckPile;
			if (hitGameobject.name == "PileDiamond")
				return suitPile[0];
			if (hitGameobject.name == "PileHeart")
				return suitPile[1];
			if (hitGameobject.name == "PileSpade")
				return suitPile[2];
			if (hitGameobject.name == "PileClub")
				return suitPile[3];
			return null;
		}



		// 
		// void fireUpdate()
		//
		// this method is called from other scripts to trigger update
		//

		public void fireUpdate()
		{
			isUpdateNeccessary = true;
		}



		// 
		// void alterScore()
		//
		// this method is called from other scripts to trigger score update
		//

		public void alterScore(int _score)
		{
			score += _score;
			AudioEvent.AudioEvents.AudioPlay(AudioAction.Win);

		}


		// -------------------------------------------------------------------------------------

		public void showOptions()
		{
			pauseTable.transform.position = new Vector3(0, 0, -1);
			optionsDialog.SetActive(true);
			Time.timeScale = 0;
		}

		public void closeOptions()
		{
			pauseTable.transform.position = new Vector3(0, 0, 10);
			optionsDialog.SetActive(false);
			Time.timeScale = 1;
		}

	}
}