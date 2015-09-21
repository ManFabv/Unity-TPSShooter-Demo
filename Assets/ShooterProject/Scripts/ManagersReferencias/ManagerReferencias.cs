using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerReferencias : Singleton<ManagerReferencias> {

	private Dictionary<NombresReferencias.NOMBRES_REFERENCIAS, List<GameObject>> listadoReferencias;
	
	void Awake()
	{
		listadoReferencias = new Dictionary<NombresReferencias.NOMBRES_REFERENCIAS, List<GameObject>>();
	}
	
	public void AgregarReferencia(NombresReferencias.NOMBRES_REFERENCIAS nombreReferencia, GameObject referencia)
	{
		//si no existe la referencia
		if(!listadoReferencias.ContainsKey(nombreReferencia))
			listadoReferencias.Add(nombreReferencia, new List<GameObject>());
			
		listadoReferencias[nombreReferencia].Add(referencia);
	}
	
	public GameObject ObtenerReferencia(NombresReferencias.NOMBRES_REFERENCIAS nombreReferencia)
	{
		//si no encontro la clave devuelvo un objeto nulo
		if(listadoReferencias.ContainsKey(nombreReferencia) == false)
		{
			Debug.LogWarning("FALTA REFERENCIA = " + nombreReferencia);
			return null;
		}
			
		//si necesito solo una referencia, devuelvo la primera coincidencia
		return listadoReferencias[nombreReferencia][0];
	}
	
	public List<GameObject> ObtenerMultiplesReferencias(NombresReferencias.NOMBRES_REFERENCIAS nombreReferencias)
	{
		//si no encontro nada devuelvo una lista vacia
		if(listadoReferencias.ContainsKey(nombreReferencias) == false)
		{
			Debug.LogWarning("FALTA REFERENCIA = " + nombreReferencias);
			return new List<GameObject>();
		}
			
		//devuelvo toda la lista de referencia bajo el nombre buscado
		return listadoReferencias[nombreReferencias];
	}
	
	public void EliminarReferencia(NombresReferencias.NOMBRES_REFERENCIAS nombreReferencias)
	{
		//si no existe la referencia, salgo
		if(!listadoReferencias.ContainsKey(nombreReferencias))
		{
			Debug.LogWarning("FALTA REFERENCIA = " + nombreReferencias);
			return;
		}
		
		//veo si hay considencias y las remuevo del diccionario
		for(int i = listadoReferencias[nombreReferencias].Count-1; i>=0; i--)
		{
			//remuevo desde atras hacia adelante
			listadoReferencias[nombreReferencias].RemoveAt(i);
		}
	}
	
	//remuevo si hay listeners repetidos
	public void LimpiarListadoDeInnecesarios()
	{
		//creo un diccionario temporal
		Dictionary<NombresReferencias.NOMBRES_REFERENCIAS, List<GameObject>> TmpReferencias = new Dictionary<NombresReferencias.NOMBRES_REFERENCIAS, List<GameObject>>();		
		
		foreach(KeyValuePair<NombresReferencias.NOMBRES_REFERENCIAS, List<GameObject>> Item in listadoReferencias)
		{
			//en cada key recorro la lista asociada (seria como si fuera una matriz)
			for(int i = Item.Value.Count-1; i>=0; i--)
			{
				//si es nulo, lo remuevo
				if(Item.Value[i] == null)
					Item.Value.RemoveAt(i);
			}
			
			//si el item sigue en la lista, lo agrego al diccionario temporal
			if(Item.Value.Count > 0)
				TmpReferencias.Add (Item.Key, Item.Value);
		}
		
		//reemplazo los listeners viejos por los nuevos sin redundancias
		listadoReferencias = TmpReferencias;
	}
	
	//remuevo las referencias redundantes al cargar el nivel
	void OnLevelWasLoaded()
	{
		LimpiarListadoDeInnecesarios();
	}
}
