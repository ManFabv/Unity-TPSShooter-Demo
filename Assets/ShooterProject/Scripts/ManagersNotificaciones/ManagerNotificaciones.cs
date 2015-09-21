using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerNotificaciones : Singleton<ManagerNotificaciones> {

	//aqui guardare todos los listeners
	private Dictionary<string, List<Component>> Listeners = new Dictionary<string, List<Component>>();
	
	public void AddListener(Component Sender, string NotificationName)
	{
		//si no existe el listener
		if(!Listeners.ContainsKey(NotificationName))
			Listeners.Add(NotificationName, new List<Component>());
		
		//agrego el listener al listado
		Listeners[NotificationName].Add(Sender);
	}
	
	public void RemoveListener(Component Sender, string NotificationName)
	{
        //si no existe el listener, salgo
		if(!Listeners.ContainsKey(NotificationName))
			return;
		
		//veo si hay considencias y las remuevo del diccionario
		for(int i = Listeners[NotificationName].Count-1; i>=0; i--)
		{
			if(Listeners[NotificationName][i].GetInstanceID() == Sender.GetInstanceID())
				Listeners[NotificationName].RemoveAt(i); //remuevo en la posicion i
		}
	}
	
	//envio una notificacion
	public void PostNotification(Component Sender, string NotificationName)
	{
        //si no hay listener que concuerde, salgo
		if(!Listeners.ContainsKey(NotificationName))
			return;
		
		//si encontro al menos un listener, recorro todos los encontrados y hago la llamada al metodo
		//que definio el listener
		foreach(Component Listener in Listeners[NotificationName])
			Listener.SendMessage(NotificationName, Sender, SendMessageOptions.DontRequireReceiver);
	}
	
	public void ClearListeners()
	{
		//elimino los listeners
		Listeners.Clear();
	}
	
	//remuevo si hay listeners repetidos
	public void RemoveRedundancies()
	{
		//creo un diccionario temporal
		Dictionary<string, List<Component>> TmpListeners = new Dictionary<string, List<Component>>();
		
		
		foreach(KeyValuePair<string, List<Component>> Item in Listeners)
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
				TmpListeners.Add (Item.Key, Item.Value);
		}
		
		//reemplazo los listeners viejos por los nuevos sin redundancias
		Listeners = TmpListeners;
	}
	
	//remuevo los listeners redundantes al cargar el nivel
	void OnLevelWasLoaded()
	{
		RemoveRedundancies();
	}
}