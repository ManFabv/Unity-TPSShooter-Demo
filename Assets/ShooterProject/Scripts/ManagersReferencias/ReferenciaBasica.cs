using UnityEngine;

public class ReferenciaBasica : MonoBehaviour {

    [SerializeField]
	private NombresReferencias.NOMBRES_REFERENCIAS nombreReferencia;
	
	void Awake()
	{
		ManagerReferencias.Instance.AgregarReferencia(nombreReferencia, this.gameObject);
	}
	
	public bool CompararSi(NombresReferencias.NOMBRES_REFERENCIAS nombreABuscar)
	{
		if(nombreReferencia.CompareTo(nombreABuscar) == 0)
			return true;
			
		else
			return false;
	}
}
