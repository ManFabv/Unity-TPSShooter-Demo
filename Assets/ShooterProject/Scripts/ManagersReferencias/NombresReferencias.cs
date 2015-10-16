using UnityEngine;
using System.Collections;

public class NombresReferencias : Singleton<NombresReferencias> {
	
	public enum NOMBRES_REFERENCIAS
	{
		PLAYER = 0, //referencia al jugador local
		CAMARA_SMOOTH_FOLLOW = 1, //referencia a la camara que sigue al jugador local
        PRIMARY_WEAPON_SLOT = 2, //slot donde instanciare el arma primaria equipada
        SECONDARY_WEAPON_SLOT = 3 //slot donde instanciare el arma secundaria equipada
	};
}
