using UnityEngine;
using System.Collections;
namespace Sol
{
	public class TurnDeck : MonoBehaviour
	{

		// this class is connected with the piledeck
		// if no card is left on the piledeck,
		// a click will return all cards on the wastePile to this pile
		// and hide them

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}



		//
		// public void OnMouseUp()
		//
		// This event can only be fired if this pile is empty
		// (so no other collider is in the way)
		//

		public void OnMouseUp()
		{
			GameScript.instance.turnDeck();
			// Umdrehen des Piles kostet
			GameObject go = Instantiate(Resources.Load("Prefabs/risingText")) as GameObject;
			go.transform.position = this.transform.position;
			go.transform.position = go.transform.position + new Vector3(-0.6f, 0, -1f);
			go.GetComponent<TextMesh>().text = "-100";
			GameScript.instance.alterScore(-100);
		}
	}
}