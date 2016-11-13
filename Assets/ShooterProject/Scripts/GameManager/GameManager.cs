using UnityEngine;

/// <summary>
/// Clase que me servira para ir manejando las diferentes cuestiones del juego
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// Aqui guardare cual es el layer de las armas del jugador
    /// para que no colisione con los colliders del jugador
    /// </summary>
    private LayerMask layerArmasJugador;

    /// <summary>
    /// Aqui guardare el layer del player para que no colisione consigo mismo
    /// </summary>
    private LayerMask layerPlayer;

	// Use this for initialization
	void Awake ()
    {
        //obtengo el layer a traves del nombre
        layerArmasJugador = LayerMask.NameToLayer(Literales.layerNamePlayerWeapon);

        //obtengo el layer a traves del nombre
        layerPlayer = LayerMask.NameToLayer(Literales.layerNamePlayer);

        //Digo que ignore las colisiones entre las armas del jugador y el jugador
        Physics.IgnoreLayerCollision(layerArmasJugador, layerPlayer);

        //digo que ignore las colisiones entre todos los colliders del player
        Physics.IgnoreLayerCollision(layerPlayer, layerPlayer);
	}
}
