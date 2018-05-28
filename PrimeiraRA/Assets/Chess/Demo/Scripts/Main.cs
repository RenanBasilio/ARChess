using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
	public ChessUiEngine uiEngine;

	void Start () {
		uiEngine.SetupPieces ();
	}

	void FixedUpdate () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
		int cellNumber = uiEngine.RaycastCell (ray);
		if (!IsValidCell(cellNumber)) {
			return;
		}
	}

	bool IsValidCell (int cellNumber)
	{
		return cellNumber >= 0 && cellNumber < 64;
	}
}
