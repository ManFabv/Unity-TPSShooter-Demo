using UnityEngine;
using System.Collections;

public class SceneLoaderDebug : MonoBehaviour {

    public string nombreEscenaSilvio = "";
    public string nombreEscenaFabricio = "";

	public void CargarEscenaSilvio()
    {
        Application.LoadLevel(nombreEscenaSilvio);
    }

    public void CargarEscenaFabricio()
    {
        Application.LoadLevel(nombreEscenaFabricio);
    }
}
