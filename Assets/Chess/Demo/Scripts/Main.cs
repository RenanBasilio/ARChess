using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
	public ChessEngine engine;
	// Use this for initialization
	void Start () {
		engine.SetupPieces ();
	}

	void FixedUpdate () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
		int cellNumber = engine.RaycastCell (ray);
		if (!IsValidCell(cellNumber)) {
			return;
		}
	}

	bool IsValidCell (int cellNumber)
	{
		return cellNumber >= 0 && cellNumber < 64;
	}
}
