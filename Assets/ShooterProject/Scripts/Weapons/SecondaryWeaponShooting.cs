using UnityEngine;

public class SecondaryWeaponShooting : MonoBehaviour
{

    private BasicSecondaryWeapon secondaryWeapon; //tomo el script del arma equipada

    void Start()
    {
        //asigno la referencia del script que maneja el arma
        secondaryWeapon = this.GetComponentInChildren<BasicSecondaryWeapon>();
    }

    public bool IsShooting()
    {
        //digo si el jugador esta disparando (usando el script del arma equipada)
        return secondaryWeapon.IsShooting();
    }
}