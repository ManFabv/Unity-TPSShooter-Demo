using UnityEngine;
using CnControls;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{	
    [SerializeField]
    private float translationSpeed = 4.0f; //velocidad de traslacion
    [SerializeField]
    private float rotationSpeed = 15.0f; //velocidad de rotacion

    private Rigidbody playerRigidBody; //cuerpo rigido que usare para mover al personaje con fisica

    private Vector3 moveDirection = Vector3.zero; //aqui guardare la direccion de traslacion
    private Vector3 rotationDirection = Vector3.zero; //aqui guardare la rotacion

    //scripts de las armas para saber si esta disparando o no para poder mover al personaje
	private PrimaryWeaponShooting primaryWeaponShooting;
    private SecondaryWeaponShooting secondaryWeaponShooting;

    void Start()
	{
        playerRigidBody = this.GetComponent<Rigidbody>(); //tomo el cuerpo rigido

        Invoke("InvokeSetWeaponReferences", float.Epsilon * 2.0f); //espero un instante antes de obtener las referencias
    }

    private void InvokeSetWeaponReferences()
    {
        //tomo las referencias a las armas que estan ubicadas en el slot
        primaryWeaponShooting = ManagerReferencias.Instance.ObtenerReferencia(NombresReferencias.NOMBRES_REFERENCIAS.PRIMARY_WEAPON_SLOT).GetComponentInChildren<PrimaryWeaponShooting>();
        secondaryWeaponShooting = ManagerReferencias.Instance.ObtenerReferencia(NombresReferencias.NOMBRES_REFERENCIAS.SECONDARY_WEAPON_SLOT).GetComponentInChildren<SecondaryWeaponShooting>();
    }

    //uso el metodo update para tomar el input del jugador, pero el movimiento lo realizare en el metodo fixed update
    void Update()
    {
        //actualizo el movimiento de acuerdo al input del jugador y lo guardo en movedirection
        MovementUpdate(new Vector3(CnInputManager.GetAxis("Horizontal"), 0.0f, CnInputManager.GetAxis("Vertical")));

        //si hay movimiento, y si los scripts de las armas son validos y no esta disparando ni el arma principal ni la secundaria, 
        //entonces actualizo la rotacion con la direccion del desplazamiento del jugador
        if (moveDirection != Vector3.zero && (primaryWeaponShooting != null && secondaryWeaponShooting != null) &&
           (primaryWeaponShooting.IsShooting() == false && secondaryWeaponShooting.IsShooting() == false))
        {
            //actualizo la rotacion de acuerdo al input (el cual guarde en movedirection) del stick de movimiento
            RotationUpdate(moveDirection);
        }
    }

    //uso el fixed update para realizar el movimiento
    void FixedUpdate()
    {
        //si no esta usando el arma secundaria, entonces actualizo el movimiento
        if ((secondaryWeaponShooting != null && secondaryWeaponShooting.IsShooting() == false))
        {
            //realizo el movimiento, de haber alguno
            Move();
        }

        //realizo la rotacion, de haber alguna
        Rotate();
    }

    private void Move()
    {
        //si no hay movimiento, salgo del metodo
        if (moveDirection == Vector3.zero)
            return;
        
        //muevo al cuerpo rigido en la direccion especificada
        playerRigidBody.MovePosition(Vector3.Lerp(playerRigidBody.position, playerRigidBody.position + moveDirection, Time.fixedDeltaTime * translationSpeed));
    }

    private void Rotate()
    {
        //si la rotacion es cero, entonces no hago el look at y salgo del metodo
        if (rotationDirection == Vector3.zero)
            return;
        
        //roto al cuerpo rigido en la direccion especificada
        playerRigidBody.MoveRotation(Quaternion.RotateTowards(playerRigidBody.rotation, Quaternion.LookRotation(rotationDirection), Time.fixedDeltaTime * rotationSpeed));
    }

    public void RotationUpdate(Vector3 externalRotation)
    {
        //guardo la rotacion
        rotationDirection = externalRotation;
    }

    public void MovementUpdate(Vector3 externalMovement)
    {
        //guardo el movimiento
        moveDirection = externalMovement;
    }
}