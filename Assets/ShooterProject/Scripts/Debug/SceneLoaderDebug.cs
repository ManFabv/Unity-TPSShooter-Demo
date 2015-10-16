using UnityEngine;

public class SceneLoaderDebug : MonoBehaviour {

    [SerializeField]
    public string nombreEscenaSilvio = "";
    [SerializeField]
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
