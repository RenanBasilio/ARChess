using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pecas : MonoBehaviour {
	
	int rodadaUltimoMovimento=0;
	
	public int getRodadaUltimoMovimento(){
		return rodadaUltimoMovimento;
	}
	
	public void setRodadaUltimoMovimento(int rodada){
		rodadaUltimoMovimento=rodada;
	}
	
	void OnTriggerStay(Collider col){
		pecas script=col.gameObject.GetComponent<pecas>();
		if (gameObject.transform.parent==null && rodadaUltimoMovimento>script.getRodadaUltimoMovimento() && gameObject.name.Split()[0]!=col.gameObject.name.Split()[0])
			Destroy(col.gameObject);
	}
}
