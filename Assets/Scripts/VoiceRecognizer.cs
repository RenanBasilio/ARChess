using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

public class VoiceRecognizer : MonoBehaviour {

	#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
		IntPtr decoder;

	#elif UNITY_ANDROID
		AndroidJavaObject decoder;

	#endif

	AudioSource source_mic;

	Text debugLog;

	// Use this for initialization
	void Start () {
		debugLog = GameObject.Find("DebugLog").GetComponent<Text>();
		debugLog.text = "Debug log found.\n";

		debugLog.text += Application.streamingAssetsPath + "/VoiceModel/cmusphinx-en-us-ptm-8khz-5.2" + "\n";
		debugLog.text += Directory.Exists(Application.streamingAssetsPath + "/VoiceModel/cmusphinx-en-us-ptm-8khz-5.2") + "\n";
		SetupDecoder();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Set up a pocketsphinx decoder
	void SetupDecoder () {
		Debug.Log("Inicializando decodificador.");

		#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
			Debug.Log(PocketSphinxPINVOKE.ps_args());
			//decoder = PocketSphinxPINVOKE.ps_init(PocketSphinxPINVOKE.ps_args());
			debugLog.text += decoder + "\n";
		#elif UNITY_ANDROID
			// Inicializa classe Decoder em java no pacote edu.cmu.pocketsphinx
			AndroidJavaClass decoderClass = new AndroidJavaClass("edu.cmu.pocketsphinx.Decoder");

			// Inicializa objeto config em java, que contem o resultado da chamada do metodo DefaultConfig da classe Decoder
			AndroidJavaObject config = decoderClass.Call<AndroidJavaObject>("DefaultConfig");

			// Constroi um array de parametros.
			object[] hmm = new object[2] {"-hmm", Application.streamingAssetsPath + "/VoiceModel/cmusphinx-en-us-ptm-8khz-5.2"};

			// Chama o metodo de java que configura os parametros
			config.Call("SetString", hmm);
		#endif
	}
}
