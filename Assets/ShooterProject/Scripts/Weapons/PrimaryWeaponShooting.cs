using UnityEngine;

public class PrimaryWeaponShooting : MonoBehaviour
{

    private BasicPrimaryWeapon primaryWeapon; //tomo el script del arma equipada

    void Start()
    {
        //asigno la referencia del script que maneja el arma
        primaryWeapon = this.GetComponentInChildren<BasicPrimaryWeapon>();
    }

    public bool IsShooting()
    {
        //digo si el jugador esta disparando (usando el script del arma equipada)
        return primaryWeapon.IsShooting();
    }
}