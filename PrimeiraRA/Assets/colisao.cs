using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisao : MonoBehaviour {
	
	GameObject selectedPiece;
	List<GameObject> newpos=new List<GameObject>();
	
	int[] square(GameObject piece){
		int[] ret={(int)Mathf.Round(Mathf.Abs(piece.transform.position.x-14)/4+1),(int)Mathf.Round((float)((piece.transform.position.y-2.32)/0.26+1))};
		return ret;
	}
	
	void addCubesPawn(GameObject piece){
		GameObject cube;
		int moveForward=1;
		if ((selectedPiece.name.Split()[0]=="White"&&square(selectedPiece)[0]==2)||(selectedPiece.name.Split()[0]=="Black"&&square(selectedPiece)[0]==7))
			moveForward++;
		for (int i=0;i<moveForward;i++){
			cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.rotation=selectedPiece.transform.rotation;
			cube.transform.position=selectedPiece.transform.position;
			cube.transform.Translate(4*(i+1),0,0); //forward
			newpos.Add(cube);
		}
	}
	
	void addCubesForwardBackward(GameObject piece){
		int i=1;
		int[] sqCube;
		GameObject cube;
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[0]==1)&&!(piece.name.Split()[0]=="White"&&square(piece)[0]==8))
			while (true){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(4*i,0,0); //forward
				newpos.Add(cube);
				sqCube=square(cube);
				if (sqCube[0]==8||sqCube[0]==1)
					break;
				i++;
			}
		i=1;
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[0]==8)&&!(piece.name.Split()[0]=="White"&&square(piece)[0]==1))
			while (true){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(4*-i,0,0); //backward
				newpos.Add(cube);
				sqCube=square(cube);
				if (sqCube[0]==8||sqCube[0]==1)
					break;
				i++;
			}
		return;
	}
	
	void addCubesLeftRight(GameObject piece){
		int i=1;
		int[] sqCube;
		GameObject cube;
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[1]==8)&&!(piece.name.Split()[0]=="White"&&square(piece)[1]==1))
			while (true){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(0,4*-i,0); //left
				newpos.Add(cube);
				sqCube=square(cube);
				if (sqCube[1]==8||sqCube[1]==1)
					break;
				i++;
			}
		i=1;
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[1]==1)&&!(piece.name.Split()[0]=="White"&&square(piece)[1]==8))
			while (true){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(0,4*i,0); //right
				newpos.Add(cube);
				sqCube=square(cube);
				if (sqCube[1]==8||sqCube[1]==1)
					break;
				i++;
			}
		return;
	}
	
	void addCubesDiagonally(GameObject piece){ //add conditions in if's
		int i=1;
		int[] sqCube;
		GameObject cube;
		if (false)
			while (true){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(4*i,4*-i,0); //forward-left
				newpos.Add(cube);
				sqCube=square(cube);
				if (sqCube[0]==8||sqCube[0]==1||sqCube[1]==8||sqCube[1]==1)
					break;
				i++;
			}
		i=1;
		if (false)
			while (true){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(4*i,4*i,0); //forward-right
				newpos.Add(cube);
				sqCube=square(cube);
				if (sqCube[0]==8||sqCube[0]==1||sqCube[1]==8||sqCube[1]==1)
					break;
				i++;
			}
		i=1;
		if (false)
			while (true){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(4*-i,4*-i,0); //backward-left
				newpos.Add(cube);
				sqCube=square(cube);
				if (sqCube[0]==8||sqCube[0]==1||sqCube[1]==8||sqCube[1]==1)
					break;
				i++;
			}
		i=1;
		if (false)
			while (true){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(4*-i,4*i,0); //backward-right
				newpos.Add(cube);
				sqCube=square(cube);
				if (sqCube[0]==8||sqCube[0]==1||sqCube[1]==8||sqCube[1]==1)
					break;
				i++;
			}
	}
	
	void addCubesKnight(GameObject piece){
		GameObject cube;
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[0]>6)&&!(piece.name.Split()[0]=="White"&&square(piece)[0]<3)){
			if (!(piece.name.Split()[0]=="Black"&&square(piece)[1]==1)&&!(piece.name.Split()[0]=="White"&&square(piece)[1]==8)){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(8,-4,0); //forward-left
				newpos.Add(cube);
			}
			if (!(piece.name.Split()[0]=="Black"&&square(piece)[1]==8)&&!(piece.name.Split()[0]=="White"&&square(piece)[1]==1)){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(8,4,0); //forward-right
				newpos.Add(cube);
			}
		}
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[0]<6)&&!(piece.name.Split()[0]=="White"&&square(piece)[0]>3)){
			if (!(piece.name.Split()[0]=="Black"&&square(piece)[1]==1)&&!(piece.name.Split()[0]=="White"&&square(piece)[1]==8)){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(-8,-4,0); //backward-left
				newpos.Add(cube);
			}
			if (!(piece.name.Split()[0]=="Black"&&square(piece)[1]==8)&&!(piece.name.Split()[0]=="White"&&square(piece)[1]==1)){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(-8,4,0); //backward-right
				newpos.Add(cube);
			}
		}
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[1]>6)&&!(piece.name.Split()[0]=="White"&&square(piece)[1]<3)){
			if (!(piece.name.Split()[0]=="Black"&&square(piece)[0]==1)&&!(piece.name.Split()[0]=="White"&&square(piece)[0]==8)){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(4,-8,0); //left-front
				newpos.Add(cube);
			}
			if (!(piece.name.Split()[0]=="Black"&&square(piece)[0]==8)&&!(piece.name.Split()[0]=="White"&&square(piece)[0]==1)){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(-4,-8,0); //left-back
				newpos.Add(cube);
			}
		}
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[1]<6)&&!(piece.name.Split()[0]=="White"&&square(piece)[1]>3)){
			if (!(piece.name.Split()[0]=="Black"&&square(piece)[0]==1)&&!(piece.name.Split()[0]=="White"&&square(piece)[0]==8)){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(4,8,0); //right-front
				newpos.Add(cube);
			}
			if (!(piece.name.Split()[0]=="Black"&&square(piece)[0]==8)&&!(piece.name.Split()[0]=="White"&&square(piece)[0]==1)){
				cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.rotation=selectedPiece.transform.rotation;
				cube.transform.position=selectedPiece.transform.position;
				cube.transform.Translate(-4,8,0); //right-back
				newpos.Add(cube);
			}
		}
	}
	
	void addCubesKing(GameObject piece){
		GameObject cube;
		bool forward=false, backward=false, left=false, right=false;
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[0]==1)&&!(piece.name.Split()[0]=="White"&&square(piece)[0]==8)){
			cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.rotation=selectedPiece.transform.rotation;
			cube.transform.position=selectedPiece.transform.position;
			cube.transform.Translate(4,0,0); //forward
			newpos.Add(cube);
			forward=true;
		}
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[0]==8)&&!(piece.name.Split()[0]=="White"&&square(piece)[0]==1)){
			cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.rotation=selectedPiece.transform.rotation;
			cube.transform.position=selectedPiece.transform.position;
			cube.transform.Translate(-4,0,0); //backward
			newpos.Add(cube);
			backward=true;
		}
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[1]==8)&&!(piece.name.Split()[0]=="White"&&square(piece)[1]==1)){
			cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.rotation=selectedPiece.transform.rotation;
			cube.transform.position=selectedPiece.transform.position;
			cube.transform.Translate(0,-4,0); //left
			newpos.Add(cube);
			left=true;
		}
		if (!(piece.name.Split()[0]=="Black"&&square(piece)[1]==1)&&!(piece.name.Split()[0]=="White"&&square(piece)[1]==8)){
			cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.rotation=selectedPiece.transform.rotation;
			cube.transform.position=selectedPiece.transform.position;
			cube.transform.Translate(0,4,0); //right
			newpos.Add(cube);
			right=true;
		}
		if (forward&&left){
			cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.rotation=selectedPiece.transform.rotation;
			cube.transform.position=selectedPiece.transform.position;
			cube.transform.Translate(4,-4,0); //right
			newpos.Add(cube);
		}
		if (forward&&right){
			cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.rotation=selectedPiece.transform.rotation;
			cube.transform.position=selectedPiece.transform.position;
			cube.transform.Translate(4,4,0); //right
			newpos.Add(cube);
		}
		if (backward&&left){
			cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.rotation=selectedPiece.transform.rotation;
			cube.transform.position=selectedPiece.transform.position;
			cube.transform.Translate(-4,-4,0); //right
			newpos.Add(cube);
		}
		if (backward&&right){
			cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.rotation=selectedPiece.transform.rotation;
			cube.transform.position=selectedPiece.transform.position;
			cube.transform.Translate(-4,4,0); //right
			newpos.Add(cube);
		}
	}
	
    void OnTriggerEnter (Collider col){
		if (!selectedPiece){
			selectedPiece=col.gameObject;
			switch (selectedPiece.name.Split()[1]){
				case "Pawn(Clone)":
					addCubesPawn(selectedPiece);
					break;
				case "Bishop(Clone)":
					addCubesDiagonally(selectedPiece);
					break;
				case "King(Clone)":
					addCubesKing(selectedPiece);
					break;
				case "Queen(Clone)":
					addCubesForwardBackward(selectedPiece);
					//addCubesLeftRight(selectedPiece);
					break;
				case "Knight(Clone)":
					addCubesKnight(selectedPiece);
					break;
				case "Rook(Clone)":
					addCubesForwardBackward(selectedPiece);
					//addCubesLeftRight(selectedPiece);
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
