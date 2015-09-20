using UnityEngine;
using System.Collections;

public class Swipe : MonoBehaviour {

	private bool isSwipe = false;
	private float fingerStartTime;
	private Vector2 fingerStartPos;
	
	void Start()
	{
		fingerStartTime = 0;
	}
	
	void Update()
	{
		if (Input.touchCount > 0)
		{
			foreach (Touch touch in Input.touches)
			{
				switch (touch.phase)
				{
					case TouchPhase.Began :
						/* this is a new touch */
						isSwipe = true;
						fingerStartTime = Time.time;
						fingerStartPos = touch.position;
						break;
						
					case TouchPhase.Canceled :
						/* The touch is being canceled */
						isSwipe = false;
						break;
						
					case TouchPhase.Ended :
						Vector2 direction = touch.position - fingerStartPos;
						int swipeType = -1;
						
						if (Mathf.Abs(direction.normalized.x) > 0.8)
						{
							if (Mathf.Sign(direction.x) > 0) swipeType = 0; // swipe right
							else swipeType = 1; // swipe left
						}
						
						else if (Mathf.Abs(direction.normalized.y) > 0.8)
						{
							if (Mathf.Sign(direction.y) > 0) swipeType = 2; // swipe up
							else swipeType = 3; // swipe down
						}
						
						else if (Mathf.Sign(direction.x) > 0)
						{
							if (Mathf.Sign(direction.y) > 0) swipeType = 4; // swipe diagonal up-right
							else swipeType = 5; // swipe diagonal down-right
						}
						
						else
						{
							if (Mathf.Sign(direction.y) > 0) swipeType = 6; // swipe diagonal up-left
							else swipeType = 7; // swipe diagonal down-left
						}
						
						MySwypeCallback(swipeType);
						break;
				}
			}
		}
	}
	
	void MySwypeCallback(int swipeType)
	{
		// this function is called whenever a swipe gesture is detected. swipeType
		// contains the information of the direction of the swipe gesture. See above for 
		// the number correspondance with each direction.
		Debug.Log("Swipe to "+swipeType.ToString());
	}
}
