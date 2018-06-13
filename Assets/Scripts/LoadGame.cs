using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class LoadGame : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;

	private bool unregister;

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
			SceneManager.LoadScene("Demo", LoadSceneMode.Additive);

			unregister = true;
        }
    }
}
