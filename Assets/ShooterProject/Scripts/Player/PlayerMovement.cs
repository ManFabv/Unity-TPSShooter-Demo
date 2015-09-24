using UnityEngine;
using System.Collections;
using CnControls;

[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{	
	public float translationSpeed = 4.0f;
	public float rotationSpeed = 15.0f;
	public bool invertAxis = false;
	
	private Vector3 movement;
	
	private Rigidbody playerRigidBody;
	private Transform playerTransform;
	private BasicWeaponShooting basicWeaponShooting;
	
	void Awake()
	{
		playerRigidBody = this.GetComponent<Rigidbody>();
		playerTransform = this.GetComponent<Transform>();
		basicWeaponShooting = this.GetComponentInChildren<BasicWeaponShooting>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(invertAxis)
			movement = new Vector3(CnInputManager.GetAxis("Horizontal"), 0.0f, -CnInputManager.GetAxis("Vertical"));
		else
			movement = new Vector3(CnInputManager.GetAxis("Horizontal"), 0.0f, CnInputManager.GetAxis("Vertical"));
			
		movement = movement * translationSpeed * Time.deltaTime;
		
		playerRigidBody.MovePosition(playerTransform.position + movement);
		
		if(movement.sqrMagnitude != 0 && basicWeaponShooting.IsShooting() == false)
			playerRigidBody.MoveRotation( Quaternion.Lerp(playerTransform.rotation, Quaternion.LookRotation(movement.normalized), Time.deltaTime*rotationSpeed) );
	}
	
	public void RotationUpdate(Vector3 externalMovement)
	{
		playerRigidBody.MoveRotation( Quaternion.Lerp(playerTransform.rotation, Quaternion.LookRotation(externalMovement.normalized), Time.deltaTime*rotationSpeed) );
	}
}
