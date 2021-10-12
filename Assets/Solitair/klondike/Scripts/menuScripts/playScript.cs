using UnityEngine;
using System.Collections;
namespace Sol
{
	public class playScript : menuScript
	{

		// don't use Start - it will override start from menuscript
		// Use this for initialization
		//void Start () {
		//}

		// don't use Update - it will override update from menuscript
		// Update is called once per frame
		//void Update () {
		//}

		//
		// void OnMouseDown()
		//
		// If exit is clicked, the scene "game" is shown
		//

		void OnMouseDown()
		{
			Debug.Log("Starting Game...");
			Application.LoadLevel("game");
		}

	}
}