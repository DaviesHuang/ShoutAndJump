using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {

	private AudioSource audioSource;
	private int sampleWindow = 256;

	public float loudness = 0;
	public float sensitivity = 10;

	// Use this for initialization
	void Start () {
		//Application.RequestUserAuthorization to get user permission
		if (Microphone.devices.Length <= 0) {  
			Debug.LogWarning ("Microphone not connected!");  
		} else {
			audioSource = GetComponent<AudioSource> ();
			audioSource.clip = Microphone.Start(null, true, 10, 44100);
			audioSource.loop = true; // Set the AudioClip to loop
			//while (!(Microphone.GetPosition(null) > 0)){} // Wait until the recording has started
		}
	}
	
	// Update is called once per frame
	void Update () {
		loudness = sensitivity * GetAveragedVolume ();
	}

	float GetAveragedVolume() {
		float[] data = new float[sampleWindow];
		float a = 0;
		int micPosition = Microphone.GetPosition (null) - (sampleWindow + 1);
		if (micPosition < 0) {
			return 0;
		}
		audioSource.clip.GetData (data, micPosition);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/sampleWindow;
	}

	void StopMicrophone() {
		Microphone.End (null);
	}
		

	void OnDisable() {
		StopMicrophone ();
	}

	void OnDestory() {
		StopMicrophone ();
	}


}
