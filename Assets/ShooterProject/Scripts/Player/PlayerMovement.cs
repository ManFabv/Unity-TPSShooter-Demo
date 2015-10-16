using UnityEngine;
using CnControls;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{	
    [SerializeField]
    private bool invertAxis = false; //si debe invertir el eje de movimiento vertical
    [SerializeField]
    private float translationSpeed = 4.0f; //velocidad de traslacion
    [SerializeField]
    private float rotationSpeed = 15.0f; //velocidad de rotacion

    private Rigidbody playerRigidBody; //cuerpo rigido que usare para mover al personaje con fisica

    private Vector3 movement = Vector3.zero; //aqui voy a calcular el movimiento en cada frame

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

    public void RotationUpdate(Vector3 externalMovement)
    {
        //hago rotar al cuerpo rigido en la direccion especificada
        playerRigidBody.MoveRotation(Quaternion.Lerp(playerRigidBody.rotation, Quaternion.LookRotation(externalMovement.normalized), Time.fixedDeltaTime * rotationSpeed));
    }

    public void MovementUpdate(Vector3 externalMovement)
    {
        //muevo al cuerpo rigido en la direccion especificada
        playerRigidBody.MovePosition(playerRigidBody.position + externalMovement);
    }

    void FixedUpdate()
    {
        //de acuerdo a si debe o no invertir el eje de movimiento, lo que hago es invertir el valor del input
        if (invertAxis)
            movement = new Vector3(CnInputManager.GetAxis("Horizontal"), 0.0f, -CnInputManager.GetAxis("Vertical"));
        else
            movement = new Vector3(CnInputManager.GetAxis("Horizontal"), 0.0f, CnInputManager.GetAxis("Vertical"));

        //calculo el movimiento de acuerdo al tiempo y a la velocidad
        movement = movement * translationSpeed * Time.fixedDeltaTime;

        //si no esta usando el arma secundaria, entonces actualizo el movimiento
        if(secondaryWeaponShooting != null && secondaryWeaponShooting.IsShooting() == false)
            MovementUpdate(movement);

        //si hay movimiento y no esta disparando ni el arma principal ni la secundaria, actualizo la rotacion
        if ((movement.x != 0 || movement.z != 0) && primaryWeaponShooting.IsShooting() == false && secondaryWeaponShooting.IsShooting() == false)
            RotationUpdate(movement);
    }
}