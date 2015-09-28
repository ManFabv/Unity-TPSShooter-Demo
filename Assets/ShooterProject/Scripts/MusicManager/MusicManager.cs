using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicManager : Singleton<MusicManager> 
{
	public AudioMixerSnapshot vol_low;
	public AudioMixerSnapshot vol_high;
	public AudioMixer audioMixer;

	public float timeForFade = 0.5f;

	private static MusicManager instanceRef = null; //para evitar objetos duplicados voy a usar esta variable como control

	// Use this for initialization
	void Start () 
	{
		OnLevelWasLoaded();
	}

	void Awake()
	{
		if(instanceRef == null) //si no hay instancia referenciada
		{
			//agrego la instancia y digo que no se destruya
			instanceRef = this;
			DontDestroyOnLoad(gameObject);
		}
		
		else //si ya hay una instancia, entonces puedo destruir esta instancia
		{
			DestroyImmediate(gameObject);
		}
	}
	
	//when a new level is loaded, we check to see if the the scene is the menu, so we can deactivate or reactivate all of the children objects.
	void OnLevelWasLoaded () 
	{
		audioMixer.TransitionToSnapshots (new AudioMixerSnapshot[] {vol_low, vol_high}, new float[]{1, 0}, float.Epsilon);

		Invoke ("InitFadeIn", float.Epsilon*1.0f);
	}

	public void InitFadeIn()
	{
		vol_high.TransitionTo (timeForFade);
	}
}
