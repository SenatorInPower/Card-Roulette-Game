using UnityEngine;
using System.Collections;
namespace Sol
{
	public class optionScript : menuScript
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
		// If exit is clicked, the app shows the options dialog
		// (not yet implemented)
		//


		void OnMouseDown()
		{
			Debug.Log("Starting up Options Dialog");
			//Application.Quit ();
		}
	}
}