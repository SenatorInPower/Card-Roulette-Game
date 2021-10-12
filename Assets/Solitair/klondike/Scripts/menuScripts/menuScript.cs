using UnityEngine;
using System.Collections;
namespace Sol
{
	// this is the main script for the menu entries
	// the single menu entries inherit this scrit
	//
	// This script wobbles the menu entries

	public class menuScript : MonoBehaviour
	{

		// how fast is the animation
		public float speed = 4f;
		float timer;
		float size;
		Vector3 scale;

		// set if mouse enters menu entry
		public bool playAnimation;
		// set if mouse leaves menu entry
		public bool isMenuLeft;

		// Use this for initialization
		void Start()
		{
			// animation is not played
			playAnimation = false;
			// entry is not left
			isMenuLeft = false;
			// get the actual scale and save it
			scale = transform.localScale;
		}



		//
		// void Update()
		//
		// Update is called once per frame
		//

		void Update()
		{
			if (playAnimation)
			{
				// use timer to steer animation
				timer = (timer + Time.deltaTime) % (Mathf.PI * 2);
				// animation is a sinusoid scale (wobble)
				size = (Mathf.Sin(timer * speed) * 0.2f);
				// change scale of meny entry
				transform.localScale = scale + new Vector3(1, 1, 0) * size;
				// if mouse leaves menu entry, it scales softly back
				// (otherwise is looks kinda frozen)
				if (isMenuLeft)
				{
					if (size < 0.01f && size > -0.01f)
						playAnimation = false;
				}
			}
		}



		// 
		// void OnMouseEnter()
		//
		// This method is called if player
		// move mouse above menu entry
		//

		void OnMouseEnter()
		{
			playAnimation = true;
			isMenuLeft = false;
		}



		//
		// void OnMoueExit()
		//
		// this method is called if player
		// leaves the menu menu entry
		//

		void OnMouseExit()
		{
			isMenuLeft = true;
		}
	}
}