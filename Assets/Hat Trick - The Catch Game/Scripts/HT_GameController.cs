using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HT_GameController : MonoBehaviour {
	
	public Camera cam;
	public GameObject[] balls;
	public List<GameObject> list  = new List<GameObject>();
	public GameObject tempBall;
	public float timeLeft;
	public Text timerText;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
	public HT_HatController hatController;
	
	private float maxWidth;
	private bool counting;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		timerText.text = "TIME LEFT:\n" + Mathf.RoundToInt (timeLeft);
		restartButton.SetActive(false);
	}

	void FixedUpdate () {
		if (counting) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;
			}
			timerText.text = "TIME LEFT:\n" + Mathf.RoundToInt (timeLeft);
		}
	}

	public void StartGame () {
		splashScreen.SetActive (false);
		startButton.SetActive (false);
		hatController.ToggleControl (true);
		StartCoroutine (Spawn ());
	}

	public IEnumerator Spawn () {
		yield return new WaitForSeconds (2.0f);
		counting = true;
		while (timeLeft > 0) {
			GameObject ball = balls [Random.Range (0, balls.Length)];
			Vector3 spawnPosition = new Vector3 (
				transform.position.x + Random.Range (-maxWidth, maxWidth), 
				transform.position.y, 
				0.0f
			);
			Quaternion spawnRotation = Quaternion.identity;
			GameObject tempBall = Instantiate (ball, spawnPosition, spawnRotation) as GameObject;
			list.Add(tempBall);
			yield return new WaitForSeconds (Random.Range (1f, 2.0f));
		}
		yield return new WaitForSeconds (2.0f);
		gameOverText.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		restartButton.SetActive (true);
	}
}
