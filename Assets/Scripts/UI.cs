using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class UI : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;

	private bool unregister;

	public GameObject winText;
	public GameObject resetButton; 

	// Use this for initialization
	void Start () {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(unregister) {
			GameObject.Find("Title").GetComponent<Text>().CrossFadeAlpha(0.0f, 0.5f, false);
			GameObject.Find("Hint").GetComponent<Text>().CrossFadeAlpha(0.0f, 0.5f, false);
			GameObject.Find("HintIcon").GetComponent<RawImage>().CrossFadeAlpha(0.0f, 0.5f, false);
			unregister = false;
		}
	}

	void LateUpdate() {
		if(unregister) {
			mTrackableBehaviour.UnregisterTrackableEventHandler(this);
		}
	}

    void ITrackableEventHandler.OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
			Debug.Log("OK");
			SceneManager.LoadScene("Game", LoadSceneMode.Additive);

			unregister = true;
        }
    }

	public void ResetGame() {
		Debug.Log("Resetting");
		GameObject.Find("Chess").GetComponent<ChessEngine>().Reset();
		winText.SetActive(false);
		resetButton.SetActive(false);
	}

	public void GameOverScreen(String winner) {
		winText.SetActive(true);
		resetButton.SetActive(true);
		winText.GetComponent<Text>().text = winner.ToUpper() + " GANHOU!";
	}
}
