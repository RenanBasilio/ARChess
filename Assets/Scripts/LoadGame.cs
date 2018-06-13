using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
			GameObject.Find("Canvas").SetActive(false);

			unregister = true;
        }
    }
}
