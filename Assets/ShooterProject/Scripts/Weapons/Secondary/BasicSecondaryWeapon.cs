using UnityEngine;
using CnControls;

public abstract class BasicSecondaryWeapon : MonoBehaviour {

    [SerializeField]
    private float timeBetweenShots = 1;

    //timer para controlar el tiempo entre disparos
    private float timer = 0;

    //esta variable me dira si esta disparando o no (lo usare por ejemplo para que el jugador no rote cuando este disparando
    private bool shooting = false;

    [SerializeField]
    protected GameObject equipedSecondaryWeaponModel; //este sera el modelo que voy a instanciar cada vez que ataque

    //esta sera la posicion desde la que lanzare la posicion (sera la posicion del slot)
    protected Transform throwPosition;

    [SerializeField]
    protected float throwForce = 5.5f; //que tanta fuerza debo aplicar para impulsar el disparo

    private bool canPlayerMove = true; //me permitira inmovilizar al jugador mientras esta tirando el disparo

    //es un tiempo que toma la mitad del tiempo entre disparos, esta variable me dira cuando el jugador
    //ya puede moverse despues de tirar el disparo que sera un tiempo menor al total de tiempo entre disparos
    protected float timePlayerCanWalkAfterShot;

    private AudioSource weaponAudioSource; //me servira para reproducir un sonido cuando haya algun lanzamiento en el disparo

    // Use this for initialization
    void Start()
    {
        //reinicio el timer
        timer = 0;

        //digo que no esta disparando
        shooting = false;

        //tomo como tiempo en el cual ya puede caminar el jugador la mitad del tiempo entre disparos
        timePlayerCanWalkAfterShot = timeBetweenShots / 2.0f;

        //busco el slot secundario para tomar su posicion y rotacion para el lanzamiento del disparo
        throwPosition = ManagerReferencias.Instance.ObtenerReferencia(NombresReferencias.NOMBRES_REFERENCIAS.SECONDARY_WEAPON_SLOT).transform;

        weaponAudioSource = this.GetComponent<AudioSource>(); //tomo el audio source para reproducir el sonido throw
    }

    void FixedUpdate()
    {
        //si el jugador presiono el boton de la gui de tirar bombas (el disparo secundario)
        if (CnInputManager.GetButton("Bomb") == true)
        {
            //si no esta disparando, hago que dispare
            if (shooting == false)
                Shot();
        }

        //si ya esta disparando, voy controlando el tiempo en el cual ya puede
        //volver a disparar
        if (shooting == true)
        {
            timer += Time.fixedDeltaTime; //incremento el tiempo

            //si paso el tiempo, digo que termino el disparo
            if (timer >= timeBetweenShots)
                EndShot();
        }
    }

    //este metodo inicializa todo para comenzar el disparo
    private void Shot()
    {
        canPlayerMove = false; //digo que el jugador no se podra mover

        shooting = true; //digo que esta disparando
        timer = 0; //reinicio el timer

        InstantiateShot(); //procedo a instanciar el disparo

        weaponAudioSource.PlayOneShot(weaponAudioSource.clip); //reproduzco el sonido de throw
    }

    //este metodo reinicia los valores para finalizar el disparo
    private void EndShot()
    {
        shooting = false; //digo que ya no dispara
        timer = 0; //reinicio el timer
        canPlayerMove = true; //por precausion vuelvo a decir que el jugador ya puede caminar de nuevo
    }

    //metodo abstracto en el que instanciare el disparo (sea granada o similar)
    protected abstract void InstantiateShot();

    public bool IsShooting()
    {
        return !canPlayerMove; //si no se esta moviendo, entonces esta disparando
    }

    protected void NowPlayerCanWalkInvoke()
    {
        canPlayerMove = true; //digo que ya puede caminar (llamo a este metodo con un Invoke)
    }
}
