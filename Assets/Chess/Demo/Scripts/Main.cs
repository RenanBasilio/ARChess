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
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			engine.RaycastCell (ray);
		}
		if (Input.touchCount > 0) {
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			engine.RaycastCell (ray);
		}
	}
}
