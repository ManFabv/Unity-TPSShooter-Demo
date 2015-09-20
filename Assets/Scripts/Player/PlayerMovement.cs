using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{	
	public float translationSpeed = 3.0f;
	public bool invertAxis = false;
	
	private VirtualJoystick joystick;
	
	private Vector3 movement;

	// Use this for initialization
	void Start () 
	{
		joystick = this.GetComponent<VirtualJoystick>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
#if UNITY_EDITOR
		if(invertAxis)
			movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, -Input.GetAxis("Vertical"));
		else
			movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
#else
		if(invertAxis)
			movement = new Vector3(joystick.movement.x, 0.0f, joystick.movement.y);
		else
			movement = new Vector3(joystick.movement.x, 0.0f, -joystick.movement.y);
#endif
		
        GetComponent<Rigidbody>().velocity = movement * translationSpeed;
	}
}
