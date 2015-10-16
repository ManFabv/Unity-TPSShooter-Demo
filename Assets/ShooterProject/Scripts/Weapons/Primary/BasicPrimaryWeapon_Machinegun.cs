using UnityEngine;

public class BasicPrimaryWeapon_Machinegun : BasicPrimaryWeapon
{    
    public override void Shoot()
    {
        timer = 0f; //hago cero el timer

        gunAudio.Play(); //reproduzco el sonido del disparo

        gunLight.enabled = true; //activo el point light del arma

        gunParticles.Stop(); //detengo y vuelvo a iniciar el sistema de particulas
        gunParticles.Play();

        gunLine.enabled = true; //activo el line renderer para representar al disparo

        //digo que la posicion cero (cero es el punto inicial del disparo), va a estar en la posicion POSITION
        gunLine.SetPosition(0, transform.position);

        //configuro el origen y la direccion del rayo que usare para hacer el raycast
        //va desde la posicion desde donde salen los disparos (el this, porque este script esta en el punto desde donde salen los disparos,
        //el cual es el punto de la punta del rifle que este equipado)
        shootRay.origin = transform.position; 

        shootRay.direction = transform.forward; //el rayo va hacia adelante

        //ejecuto el raycast y busco solo objetos en el layer shootablemask el cual es = "Shootable"
        //Y si hay un objeto con el cual colisiono, entonces se que puedo armar el line renderer
        //con el final en la posicion en la cual colisiono el raycast
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //si el objeto con el que colisiono es un enemigo, entonces envio un mensaje de TakeDamage
            if(shootHit.collider.gameObject.CompareTag("Enemy") == true)
                shootHit.collider.gameObject.SendMessage("TakeDamage", damagePerShot, SendMessageOptions.DontRequireReceiver);

            //pongo como posicion 1 (1 es la posicion del final del line renderer) la posicion en la que choco el raycast
            gunLine.SetPosition(1, shootHit.point);
        }

        else //si no colisiono nada, entonces trazo una linea larga en la direccion en la que iba el raycast
        {
            gunLine.SetPosition( 1, shootRay.origin + (shootRay.direction * range) );
        }
    }
}
