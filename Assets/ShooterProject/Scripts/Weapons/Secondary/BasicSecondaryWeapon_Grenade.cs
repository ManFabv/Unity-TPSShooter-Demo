using UnityEngine;
using System.Collections;

public class BasicSecondaryWeapon_Grenade : BasicSecondaryWeapon
{
    protected override void InstantiateShot()
    {
        //instancio un objeto con el modelo del arma secundaria, en la posicion y rotacion del slot del arma secundaria
        GameObject tempShot = GameObject.Instantiate(equipedSecondaryWeaponModel, throwPosition.position, throwPosition.rotation) as GameObject;
        tempShot.SetActive(true); //activo el objecto

        //calculo una fuerza que sera la suma de una fuerza horizontal hacia adelante y una fuerza vertical hacia arriba
        //De esta forma voy a lanzar el disparo en arco
        Vector3 force = tempShot.transform.forward + tempShot.transform.up;

        //normalizo el vector porque solo quiero la direccion de la fuerza
        force.Normalize();

        //aplico la fuerza en la direccion calculada previamente, por el modulo de fuerza, y digo que el tipo de fuerza es impulso
        tempShot.GetComponent<Rigidbody>().AddForce(force * throwForce, ForceMode.Impulse);

        //llamo al metodo que hara que el jugador ya pueda volver a caminar
        Invoke("NowPlayerCanWalkInvoke", timePlayerCanWalkAfterShot);
    }
}
