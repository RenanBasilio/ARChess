using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisao : MonoBehaviour {
	
	GameObject selectedPiece;
	List<GameObject> newpos=new List<GameObject>();
	
	int[] numSquare(GameObject piece){
		int[] ret={(int)Mathf.Round(Mathf.Abs(piece.transform.position.x-14)/4+1),(int)Mathf.Round((float)((piece.transform.position.y-2.32)/0.26+1))};
		return ret;
	}
	
    void OnTriggerEnter (Collider col){
		if (!selectedPiece){
			selectedPiece=col.gameObject;
			GameObject cube;
			switch (selectedPiece.name.Split()[1]){
				case "Pawn(Clone)":
					int moveForward=1;
					if ((selectedPiece.name.Split()[0]=="White"&&numSquare(selectedPiece)[0]==2)||(selectedPiece.name.Split()[0]=="Black"&&numSquare(selectedPiece)[0]==7))
						moveForward++;
					for (int i=0;i<moveForward;i++){
						cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
						cube.transform.rotation=selectedPiece.transform.rotation;
						cube.transform.position=selectedPiece.transform.position;
						cube.transform.Translate(4*(i+1),0,0); //forward
						newpos.Add(cube);
					}
					break;
				case "Bishop(Clone)":
					for (int i=0;i<2;i++){
						cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
						cube.transform.rotation=selectedPiece.transform.rotation;
						cube.transform.position=selectedPiece.transform.position;
						cube.transform.Translate(4*(i+1),4*(i+1),0); //diagonally to the right
						newpos.Add(cube);
					}
					break;
				case "King(Clone)":
					for (int i=0;i<1;i++){
						cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
						cube.transform.rotation=selectedPiece.transform.rotation;
						cube.transform.position=selectedPiece.transform.position;
						cube.transform.Translate(4*(i+1),-4*(i+1),0); //diagonally to the left
						newpos.Add(cube);
					}
					break;
				case "Queen(Clone)":
					for (int i=0;i<3;i++){
						cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
						cube.transform.rotation=selectedPiece.transform.rotation;
						cube.transform.position=selectedPiece.transform.position;
						cube.transform.Translate(4*(i+1),0,0); //diagonally to the left
						newpos.Add(cube);
					}
					break;
				case "Knight(Clone)":
					for (int i=0;i<1;i++){
						cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
						cube.transform.rotation=selectedPiece.transform.rotation;
						cube.transform.position=selectedPiece.transform.position;
						cube.transform.Translate(8,4,0);
						newpos.Add(cube);
					}
					break;
				case "Rook(Clone)":
					for (int i=0;i<1;i++){
						cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
						cube.transform.rotation=selectedPiece.transform.rotation;
						cube.transform.position=selectedPiece.transform.position;
						cube.transform.Translate(0,-4*(i+1),0);
						newpos.Add(cube);
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
				for (int i=0;i<newpos.Count;i++)
					Destroy(newpos[i]);
				newpos.Clear();
				selectedPiece=null;
			}
		}
    }
}
