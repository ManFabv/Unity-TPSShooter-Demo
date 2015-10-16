using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : Singleton<MusicManager> 
{
    [SerializeField]
	private AudioMixerSnapshot vol_low; //volumen cero
    [SerializeField]
    private AudioMixerSnapshot vol_high; //volumen maximo
    [SerializeField]
    private AudioMixerSnapshot vol_distortion; //con efectos de distorsion para representar que revento una granada
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private float timeForFade = 0.5f;

	private static MusicManager instanceRef = null; //para evitar objetos duplicados voy a usar esta variable como control

	void Awake()
	{
		if(instanceRef == null) //si no hay instancia referenciada
		{
			//agrego la instancia y digo que no se destruya
			instanceRef = this;
			DontDestroyOnLoad(gameObject);

            OnLevelWasLoaded(); //inicializo el audio
        }
		
		else //si ya hay una instancia, entonces puedo destruir esta instancia
		{
			DestroyImmediate(gameObject);
		}
	}
	
	//cuando un nivel es cargado, se ejecuta este metodo callback
	void OnLevelWasLoaded () 
	{
        //cargo los snapshots, diciendo con el segundo parametro, el peso de cada snapshot (vemos que como segundo parametro tenemos 1,0, esto
        //quiere decir que el snapshot actual esta completamente hacia el snapshot de volumen bajo y nada del snapshot de volumen alto)
		audioMixer.TransitionToSnapshots (new AudioMixerSnapshot[] {vol_low, vol_high, vol_distortion }, new float[]{1, 0, 0}, float.Epsilon);

        //llamo con un retraso para comenzar el fade in
		Invoke ("InitFadeIn", float.Epsilon*2.0f);
	}

	public void InitFadeIn()
	{
        //inicio la transicion del audio, hacia el snapshot con volumen alto y le paso el tiempo de fade
		vol_high.TransitionTo (timeForFade);
	}

    public void InitDistortion(float timeToInit)
    {
        //inicio una transicion casi inmediata al snapshot que implementa el distortion
        vol_distortion.TransitionTo(timeToInit);
    }

    public void EndDistortion(float timeToEnd)
    {
        //hago una transicion suave al snapshot normal
        vol_high.TransitionTo(timeToEnd);
    }
}
