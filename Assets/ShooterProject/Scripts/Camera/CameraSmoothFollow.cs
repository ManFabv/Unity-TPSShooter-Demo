using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour {

	//el objeto a seguir
    [SerializeField]
	private Transform target;

    //a que altura se movera la camara sobre el target
    [SerializeField]
    private float height = 5.0f;

    //amortiguacion del movimiento (altura y rotacion)
    [SerializeField]
    private float heightDamping = 2.0f;
    [SerializeField]
    private float rotationDamping = 3.0f;

    //offset que tendra la camara siguiendo al jugador
    [SerializeField]
    private Vector3 offset = Vector3.zero;

    //amortiguacion de translacion
    [SerializeField]
    private float followTranslationDamp = 0.55f;

    //velocidad de movimiento
	private Vector3 velocity = Vector3.zero;

	void Start()
	{
        //si no hay un objetivo asignado, busco al jugador
		if(target == null)
			target = ManagerReferencias.Instance.ObtenerReferencia(NombresReferencias.NOMBRES_REFERENCIAS.PLAYER).GetComponent<Transform>();
	}

	void FixedUpdate () 
	{
		//si no hay target, salimos del metodo
		if (!target)
            return;

		// calculamos la altura y rotacion deseada
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;

        //tomamos la altura y rotacion actual para comenzar el movimiento
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		//interpolamos el angulo para acercarnos al angulo deseado
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.fixedDeltaTime);

        //interpolamos la altura para acercarnos a la altura deseada
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.fixedDeltaTime);

        ////interpolamos la posicion a la posicion deseada (teniedo en cuenta el offset)
        transform.position = Vector3.SmoothDamp(transform.position, target.position+offset, ref velocity, followTranslationDamp);

		//asigno la altura de la camara
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
	
		//hago que siempre mire al objetivo
		transform.LookAt(target);
	}
}
