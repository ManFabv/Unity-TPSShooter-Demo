using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(nombreEscenaSilvio);
        }
    }

    public void CargarEscenaFabricio()
    {
        if (loadingLevel == false)
        {
            loadingLevel = true;
            SceneManager.LoadScene(nombreEscenaFabricio);
        }
    }
}
