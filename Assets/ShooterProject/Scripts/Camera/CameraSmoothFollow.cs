using UnityEngine;
using System.Collections;

public class CameraSmoothFollow : MonoBehaviour {

	// The target we are following
	private Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 5.0f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	
	public Vector3 offset = Vector3.zero;
	public float followTranslationDamp = 1.0f;
	private Vector3 velocity = Vector3.zero;

	// Place the script in the Camera-Control group in the component menu
	[AddComponentMenu("Camera-Control/Smooth Follow")]
	
	void Start()
	{
		if(target == null)
			target = ManagerReferencias.Instance.ObtenerReferencia(NombresReferencias.NOMBRES_REFERENCIAS.PLAYER).GetComponent<Transform>();
	}

	void LateUpdate () {
		// Early out if we don't have a target
		if (!target) return;

		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;

		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
	
		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		transform.position = Vector3.SmoothDamp(transform.position, target.position+offset, ref velocity, followTranslationDamp);

		// Set the height of the camera
		transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);
	
		// Always look at the target
		transform.LookAt(target);
	}
}
