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
		private Decoder decoder;

	#elif UNITY_ANDROID
		private AndroidJavaObject decoder;

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
			String[] args = new String[6] {
				"-hmm", Application.streamingAssetsPath + "VoiceRecognition/VoiceModel/cmusphinx-en-us-ptm-8khz-5.2",
				"-lm", Application.streamingAssetsPath + "VoiceRecognition/LanguageModel/en-70-0.1.lm.bin",
				"-dict", Application.streamingAssetsPath + "VoiceRecognition/dict-en-us.dict"
			};
			
			cmd_ln_t decoder_args = PocketSphinx.buildDecoderArgs( new cmd_ln_t(IntPtr.Zero), PocketSphinx.getArgumentDefinitions(), args );
			decoder = new Decoder(decoder_args);

		#elif UNITY_ANDROID
			// Inicializa classe Decoder em java no pacote edu.cmu.pocketsphinx
			AndroidJavaClass decoderClass = new AndroidJavaClass("edu.cmu.pocketsphinx.Decoder");

			// Inicializa objeto config em java, que contem o resultado da chamada do metodo DefaultConfig da classe Decoder
			AndroidJavaObject config = decoderClass.Call<AndroidJavaObject>("DefaultConfig");

			// Constroi um array de parametros.
			String[] hmm = new String[2] {"-hmm", Application.streamingAssetsPath + "VoiceRecognition/VoiceModel/cmusphinx-en-us-ptm-8khz-5.2"};
			String[] lm = new String[2] {"-lm", Application.streamingAssetsPath + "VoiceRecognition/LanguageModel/en-70-0.1.lm.bin"};
			String[] dict = new String[2] {"-dict", Application.streamingAssetsPath + "VoiceRecognition/dict-en-us.dict"};

			// Chama o metodo de java que configura os parametros
			config.Call("SetString", hmm[0], hmm[1]);
		#endif
	}
}
