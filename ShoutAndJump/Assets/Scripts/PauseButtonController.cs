using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour {

	public GameObject pauseIcon;
	public GameObject resumeIcon;

	private bool paused = false;

	// Use this for initialization
	void Start () {
		SetState ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleButton() {
		paused = !paused;
		SetState ();
	}

	private void SetState() {
		if (paused) {
			pauseIcon.SetActive (false);
			resumeIcon.SetActive (true);
			Time.timeScale = 0;
		} else {
			pauseIcon.SetActive (true);
			resumeIcon.SetActive (false);
			Time.timeScale = 1;
		}
	}

	void OnDisable() {
		paused = false;
		SetState ();
	}
}
