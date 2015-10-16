using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField]
    private GameObject equipedPrimaryWeaponModel; //modelo de arma primaria equipada
    [SerializeField]
    private GameObject equipedSecondaryWeaponModel; //modelo de arma secundaria equipada

    private GameObject primaryWeapon; //arma primaria instanciada actual equipada
    private GameObject secondaryWeapon; //arma secundaria instanciada actual equipada

    //aqui instanciare los slots en los que estan ubicadas las armas primarias y secundarias
    private Transform primarySlot;
    private Transform secondarySlot;

    void Start ()
    {
        //tomo la referencia al primer slot
        primarySlot = ManagerReferencias.Instance.ObtenerReferencia(NombresReferencias.NOMBRES_REFERENCIAS.PRIMARY_WEAPON_SLOT).transform;

        //voy a calcular las dimensiones de cada arma para poder ubicarlas en los slots
        float anchoArma = 0;
        float altoArma = 0;

        //si tiene un modelo de arma primaria equipada
        if (equipedPrimaryWeaponModel != null)
        {
            //instancio el arma primaria equipada y la activo (por defecto debe estar desactivada)
            primaryWeapon = GameObject.Instantiate(equipedPrimaryWeaponModel, primarySlot.position, primarySlot.rotation) as GameObject;
            primaryWeapon.SetActive(true);

            //calculo la mitad del ancho y alto del arma
            anchoArma = primaryWeapon.GetComponent<Renderer>().bounds.size.x / 2.0f;
            altoArma = primaryWeapon.GetComponent<Renderer>().bounds.size.y / 2.0f;

            //asigno el parent en el slot y lo translado de acuerdo al ancho y alto
            primaryWeapon.transform.SetParent(primarySlot);
            primaryWeapon.transform.Translate(anchoArma, altoArma, 0);
        }

        else
            Debug.LogWarning("NO SE ENCONTRO MODELO PRIMARIO DE ARMA EN : " + this.name);

        //asigno el slot donde ira ubicada el arma secundaria
        secondarySlot = ManagerReferencias.Instance.ObtenerReferencia(NombresReferencias.NOMBRES_REFERENCIAS.SECONDARY_WEAPON_SLOT).transform;

        //si tiene un modelo de arma secundaria equipada
        if (equipedSecondaryWeaponModel != null)
        {
            //instancio el arma secundaria equipada y la activo (por defecto debe estar desactivada)
            secondaryWeapon = GameObject.Instantiate(equipedSecondaryWeaponModel, secondarySlot.position, secondarySlot.rotation) as GameObject;
            secondaryWeapon.SetActive(true);

            //calculo la mitad del ancho y alto del arma
            anchoArma = secondaryWeapon.GetComponent<Renderer>().bounds.size.x / 2.0f;
            altoArma = secondaryWeapon.GetComponent<Renderer>().bounds.size.y / 2.0f;

            //asigno el parent en el slot y lo translado de acuerdo al ancho y alto
            secondaryWeapon.transform.SetParent(secondarySlot);
            secondaryWeapon.transform.Translate(anchoArma, -altoArma, 0);
        }

        else
            Debug.LogWarning("NO SE ENCONTRO MODELO SECUNDARIO DE ARMA EN : " + this.name);
    }
}
