using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Advertisements;

public class BunnyController : MonoBehaviour {

	private Rigidbody2D myRigidBody;
	private Animator myAnimator;
	private Collider2D myCollider;
	private float startTime;
	private MicrophoneInput mic;
	private float nextJump = 0;
	private bool gameEnd = false;
	private float score;
	private float endTime;

	public Text myScoreText;
	public float jumpForce = 500f;
	public AudioSource jumpSfx;
	public AudioSource deathSfx;
	public GameObject muteButton;
	public Text gameOverText;
	public Text highScoreText;
	public Text endGameScoreText;

	public GameObject microphone;


	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myCollider = GetComponent<Collider2D>();
		startTime = Time.time;
		mic = microphone.GetComponent<MicrophoneInput> ();
		foreach (Text t in gameOverText.GetComponentsInChildren<Text> ()) {
			t.enabled = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		//if ((Input.GetButtonUp ("Jump") || Input.GetButtonUp("Fire1") || mic.loudness > 1) && jumpsLeft > 0 && EventSystem.current.currentSelectedGameObject == null) {
		if (!gameEnd && mic.loudness > 1 && Time.time > nextJump) {
			nextJump = Time.time + 1;
			myRigidBody.AddForce (transform.up * jumpForce);
			jumpSfx.Play();
		}
	}

	void Update() {
		if (!gameEnd) {
			myAnimator.SetFloat ("vVelocity", myRigidBody.velocity.y);
			score = Time.time - startTime;
			myScoreText.text = score.ToString ("0.0");
		} else if (Time.time > endTime + 2 && mic.loudness > 2) {
			SceneManager.LoadScene ("Start");
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (!gameEnd && collision.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			gameEnd = true;
			endTime = Time.time;
			FindObjectOfType<Button> ().gameObject.SetActive (false);
			foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>()) {
				spawner.enabled = false;
			}
			foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>()) {
				moveLefter.enabled = false;
			}
			myAnimator.SetBool ("BunnyHurt", true);
			myRigidBody.velocity = Vector2.zero;
			myRigidBody.AddForce (transform.up * jumpForce);
			myCollider.enabled = false;
			deathSfx.Play();
			showEndGameText ();

			showAds ();
		}
	}

	void showEndGameText() {
		float highScore = PlayerPrefs.GetFloat ("highScore", score);
		if (score >= highScore) {
			highScore = score;
			PlayerPrefs.SetFloat ("highScore", highScore);
			PlayerPrefs.Save ();
		}
		highScoreText.text = "High Score: " + highScore.ToString("0.0");
		endGameScoreText.text = "New Score: " + score.ToString("0.0");
		foreach (Text t in gameOverText.GetComponentsInChildren<Text> ()) {
			t.enabled = true;
		}
	}

	void showAds() {
		Advertisement.Show ();
	}
		
}
