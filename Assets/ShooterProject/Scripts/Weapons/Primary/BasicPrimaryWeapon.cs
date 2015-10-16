using UnityEngine;
using CnControls;

public abstract class BasicPrimaryWeapon : MonoBehaviour {

    [SerializeField]
    protected int damagePerShot = 20; //daño que realiza por disparo
    [SerializeField]
    protected float timeBetweenBullets = 0.15f; //tiempo entre disparos
    [SerializeField]
    protected float range = 100f; //rango hasta el que llega el disparo

    //me dira si esta disparando o no (por ejemplo, para no rotar al jugador mientras dispara)
    protected bool isShooting = false;

    //timer para llevar el control del disparo
    protected float timer;

    //aqui guardare la informacion del raycast (la linea imaginaria que uso para saber si golpeo algo el disparo)
    protected Ray shootRay;
    protected RaycastHit shootHit;

    //mascara en la cual revisare por colisiones en el raycast
    [SerializeField]
    protected LayerMask shootableMask;
    
    //recursos que usare para realizar el disparo (como las particulas, el audio, etc)
    protected ParticleSystem gunParticles;
    protected LineRenderer gunLine;
    protected AudioSource gunAudio;
    protected Light gunLight;

    protected float effectsDisplayTime = 0.2f; //tiempo que demora en terminar los efectos

    protected PlayerMovement playerMovement; //script que me permitira ejecutar la rotacion mientras disparo

    protected Vector3 aimMovement; //vector con el que armare la direccion en la que estoy disparando

    void Start()
    {
        //obtengo los scripts necesarios para representar el disparo
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();

        //busco una referencia al script que controla el movimiento del jugador
        playerMovement = ManagerReferencias.Instance.ObtenerReferencia(NombresReferencias.NOMBRES_REFERENCIAS.PLAYER).GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        //si esta disparando, voy incrementando el tiempo
        if(isShooting)
            timer += Time.fixedDeltaTime;

        //tomo la direccion en la que esta apuntando el jugador con el stick derecho
        aimMovement = new Vector3(CnInputManager.GetAxis("HorizontalAim"), 0.0f, CnInputManager.GetAxis("VerticalAim"));

        //si hay movimiento en cualquier direccion
        if (aimMovement.x != 0 || aimMovement.z != 0)
        {
            isShooting = true; //digo que esta disparando

            //actualizo la direccion de rotacion para moverme en la direccion en la que dispara
            playerMovement.RotationUpdate(aimMovement);

            //si el tiempo que paso es mayor que el tiempo entre disparos, entonces vuelvo a disparar
            //y si ademas el timescale no es cero (no esta en pausa)
            if (timer >= timeBetweenBullets && Time.timeScale != 0)
                Shoot();
        }

        else //si no esta disparando, digo que ya no dispara
            isShooting = false;

        //si paso el tiempo del disparo, desactivo los efectos del disparo
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public bool IsShooting()
    {
        return isShooting; //digo si esta disparando o no (para no hacer que rote con el stick izquierdo, por ejemplo)
    }

    public void DisableEffects()
    {
        gunLine.enabled = false; //desactivo la luz y el render line al finalizar el disparo
        gunLight.enabled = false;
    }

    public abstract void Shoot(); //metodo que debo sobreescribir para implementar el disparo
}
