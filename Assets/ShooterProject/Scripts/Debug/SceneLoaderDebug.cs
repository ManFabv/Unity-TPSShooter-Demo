using UnityEngine;

public class SceneLoaderDebug : MonoBehaviour {

    [SerializeField]
    public string nombreEscenaSilvio = "";
    [SerializeField]
    public string nombreEscenaFabricio = "";

    private bool loadingLevel = false;

	public void CargarEscenaSilvio()
    {
        if (loadingLevel == false)
        {
            loadingLevel = true;
            Application.LoadLevel(nombreEscenaSilvio);
        }
    }

    public void CargarEscenaFabricio()
    {
        if (loadingLevel == false)
        {
            loadingLevel = true;
            Application.LoadLevel(nombreEscenaFabricio);
        }
    }
}
