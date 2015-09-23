using UnityEngine;
using System.Collections;
using CnControls;

[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{	
	public float translationSpeed = 3.0f;
	public float rotationSpeed = 15.0f;
	public bool invertAxis = false;
	
	private Vector3 movement;
	
	private Rigidbody playerRigidBody;
	
	void Start()
	{
		playerRigidBody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(invertAxis)
			movement = new Vector3(CnInputManager.GetAxis("Horizontal"), 0.0f, -CnInputManager.GetAxis("Vertical"));
		else
			movement = new Vector3(CnInputManager.GetAxis("Horizontal"), 0.0f, CnInputManager.GetAxis("Vertical"));
		
        playerRigidBody.velocity = movement * translationSpeed;
		
		if(movement.sqrMagnitude != 0)
			playerRigidBody.MoveRotation( Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement.normalized), Time.deltaTime*rotationSpeed) );
	}
}
