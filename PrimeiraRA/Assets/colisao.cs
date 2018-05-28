using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisao : MonoBehaviour {
	
	GameObject selectedPiece;
	GameObject[] newpos;
	
    void OnTriggerEnter (Collider col){
		if (!selectedPiece){
			selectedPiece=col.gameObject;
			switch (selectedPiece.name.Split()[1]){
				case "Pawn(Clone)":
					newpos=new GameObject[2];
					for (int i=0;i<newpos.Length;i++){
						newpos[i]=GameObject.CreatePrimitive(PrimitiveType.Cube);
						newpos[i].transform.rotation=selectedPiece.transform.rotation;
						newpos[i].transform.position=selectedPiece.transform.position;
						newpos[i].transform.Translate(4*(i+1),0,0); //forward
					}
					break;
				case "Bishop(Clone)":
					newpos=new GameObject[1];
					for (int i=0;i<newpos.Length;i++){
						newpos[i]=GameObject.CreatePrimitive(PrimitiveType.Cube);
						newpos[i].transform.rotation=selectedPiece.transform.rotation;
						newpos[i].transform.position=selectedPiece.transform.position;
						newpos[i].transform.Translate(4*(i+1),4*(i+1),0); //diagonally to the right
					}
					break;
				case "King(Clone)":
					newpos=new GameObject[1];
					for (int i=0;i<newpos.Length;i++){
						newpos[i]=GameObject.CreatePrimitive(PrimitiveType.Cube);
						newpos[i].transform.rotation=selectedPiece.transform.rotation;
						newpos[i].transform.position=selectedPiece.transform.position;
						newpos[i].transform.Translate(4*(i+1),-4*(i+1),0); //diagonally to the left
					}
					break;
				case "Queen(Clone)":
					newpos=new GameObject[3];
					for (int i=0;i<newpos.Length;i++){
						newpos[i]=GameObject.CreatePrimitive(PrimitiveType.Cube);
						newpos[i].transform.rotation=selectedPiece.transform.rotation;
						newpos[i].transform.position=selectedPiece.transform.position;
						newpos[i].transform.Translate(4*(i+1),0,0); //diagonally to the left
					}
					break;
				case "Knight(Clone)":
					newpos=new GameObject[1];
					for (int i=0;i<newpos.Length;i++){
						newpos[i].transform.rotation=selectedPiece.transform.rotation;
						newpos[i].transform.position=selectedPiece.transform.position;
						newpos[i]=GameObject.CreatePrimitive(PrimitiveType.Cube);
						newpos[i].transform.Translate(8,4,0);
					}
					break;
				case "Rook(Clone)":
					newpos=new GameObject[1];
					for (int i=0;i<newpos.Length;i++){
						newpos[i]=GameObject.CreatePrimitive(PrimitiveType.Cube);
						newpos[i].transform.rotation=selectedPiece.transform.rotation;
						newpos[i].transform.position=selectedPiece.transform.position;
						newpos[i].transform.Translate(0,-4*(i+1),0);
					}
					break;
			}
			selectedPiece.transform.parent=gameObject.transform;
		}
		else{
			if (col.gameObject.name=="Cube"){
				selectedPiece.transform.parent=null;
				selectedPiece.transform.rotation=col.gameObject.transform.rotation;
				selectedPiece.transform.position=col.gameObject.transform.position;
				for (int i=0;i<newpos.Length;i++)
					Destroy(newpos[i]);
				selectedPiece=null;
			}
		}
    }
}
