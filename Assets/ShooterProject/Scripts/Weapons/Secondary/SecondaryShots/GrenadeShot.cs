using UnityEngine;
using System.Collections;

public class GrenadeShot : MonoBehaviour {

    //recursos que usare para realizar el disparo (como las particulas, el audio, etc)
    protected ParticleSystem gunParticlesDisparo;
    protected AudioSource gunAudioDisparo;
    protected Light gunLightDisparo;

    [SerializeField]
    private LayerMask layerExplosion;

    [SerializeField]
    protected float timeUntilExplosion = 2.5f;

    [SerializeField]
    private float explosionRadius = 8; //radio de la explosion
    [SerializeField]
    private float explosionForce = 900; //fuerza que aplicare a los objetos dentro del rango de explosion

    [SerializeField]
    private float explosionDamage = 10; //daño que hara la explosion

    void Start()
    {
        //obtengo los scripts necesarios para representar el disparo
        gunParticlesDisparo = this.GetComponent<ParticleSystem>();
        gunAudioDisparo = this.GetComponent<AudioSource>();
        gunLightDisparo = this.GetComponent<Light>();

        gunAudioDisparo.Stop(); //desactivo el sonido del disparo
        gunLightDisparo.enabled = false; //desactivo el point light del arma
        gunParticlesDisparo.Stop(); //detengo el sistema de particulas

        //si no llega a colisionar con nada, igual explotara por que se cumplio su tiempo
        Invoke("Explode", timeUntilExplosion);
    }

    void OnCollisionEnter(Collision other)
    {
        //si la granada colisiono con un enemigo, entonces hago que explote
        if (other.gameObject.CompareTag("Enemy") == true)
            Explode();
    }

    private void Explode()
    {
        CancelInvoke("Explode"); //si explota antes de ser invocado, entonces por las dudas cancelo el invoke

        //obtengo todos los colliders
        Collider[] cols = this.GetComponents<Collider>();

        //desactivo los colliders
        for (int i = 0; i < cols.Length; i++)
            cols[i].enabled = false;

        this.GetComponent<Rigidbody>().isKinematic = true; //desactivo la fisica

        gunAudioDisparo.Play(); //reproduzco el sonido del disparo

        gunLightDisparo.enabled = true; //activo el point light del arma

        gunParticlesDisparo.Stop(); //detengo y vuelvo a iniciar el sistema de particulas
        gunParticlesDisparo.Play();

        this.GetComponent<MeshRenderer>().enabled = false; //desactivo el mesh renderer

        AfectarObjetosEnRadio(); //hago que la explosion afecte a los objetos en el radio

        //invoco al metodo clean para que se ejecute en un cierto tiempo. El tiempo lo tomo como el maximo
        //valor entre la duracion del sistema de particulas mas un 20%, o la duracion del clip de audio mas un 10%
        Invoke("Clean", Mathf.Max(gunParticlesDisparo.duration*1.2f, gunAudioDisparo.clip.length+0.1f));
    }    

    private void AfectarObjetosEnRadio()
    {
        //busco los colliders que esten en el rango de accion de la explosion y que esten dentro del layerExplosion
        Collider[] cols = Physics.OverlapSphere(this.transform.position, explosionRadius, layerExplosion);

        //recorro todos los colliders encontrados
        for (int i = 0; i < cols.Length; i++)
        {
            //veo si el objeto tiene cuerpo rigido
            Rigidbody enemyRigidbody = cols[i].GetComponent<Rigidbody>();

            //si el objeto tiene cuerpo rigido y ademas es del tipo (por el tag) enemigo, entonces aplico una fuerza
            if (enemyRigidbody != null && enemyRigidbody.CompareTag("Enemy") == true)
            {
                //aplico una fuerza de explosion
                enemyRigidbody.AddExplosionForce(explosionForce, this.transform.position, explosionRadius);

                //mando un mensaje de TakeDamage al enemigo
                enemyRigidbody.SendMessage("TakeDamage", explosionDamage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void Clean()
    {
        //elimino el objeto
        GameObject.Destroy(this.gameObject);
    }
}
