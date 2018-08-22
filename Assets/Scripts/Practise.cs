using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Advertisements;

public class Practise : MonoBehaviour {


	public float speed = 0.01F;

	public Camera camera;

	private Rigidbody2D rigidBody;

	public GameObject ProceedLevelPanel;

	private bool pressed = false;

	private Vector3 prevTouch;

	void Start(){
		PlayerPrefs.SetInt ("First_Time", 1);
		rigidBody = this.GetComponent<Rigidbody2D> ();
		Time.timeScale = 1.5f;
	}
		
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene ("levels");
		}
			

		if (Input.GetMouseButtonDown (0)) {

			var pauseButton = new Rect (Screen.width * 3/4f, Screen.height * 3/4f, Screen.width * 1/4f, Screen.height * 1/4f);

			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.mousePosition;

			if (pauseButton.Contains (Input.mousePosition)) {
				Debug.Log ("In pause area / game was paused ");
				return;
			}

			Debug.Log ( " Touch " + touchDeltaPosition.x + " " + touchDeltaPosition.y);

			Vector3 playerDeltaPosition = camera.WorldToScreenPoint (transform.position);

			Debug.Log ( " Player " + playerDeltaPosition.x + " " + playerDeltaPosition.y);

			Vector3 forceVector = playerDeltaPosition - new Vector3( touchDeltaPosition.x, touchDeltaPosition.y, 0);

			//forceVector = forceVector.normalized * 720f;

			forceVector = forceVector.normalized * 690f;

			//forceVector = forceVector.normalized * 500f; //Easy

			Debug.Log ("Magnitude is " + forceVector.magnitude);

			rigidBody.AddForce (forceVector);

			// Move object across XY plane
			//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

		}

		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved && !pressed) 
		{

			pressed = true;
			//move
			var pauseButton = new Rect (Screen.width * 3/4f, Screen.height * 3/4f, Screen.width * 1/4f, Screen.height * 1/4f);

			if (pauseButton.Contains (Input.GetTouch (0).position)) {
				Debug.Log ("In pause area");
				return;
			}

			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;

			if (prevTouch != null && prevTouch.x == touchDeltaPosition.x && prevTouch.y == touchDeltaPosition.y) {
				Debug.Log ("Same Touch");
				return;
			}

			Debug.Log (" Touch " + touchDeltaPosition.x + " " + touchDeltaPosition.y);

			Vector3 playerDeltaPosition = camera.WorldToScreenPoint (transform.position);

			Debug.Log (" Player " + playerDeltaPosition.x + " " + playerDeltaPosition.y);

			Vector3 forceVector = playerDeltaPosition - new Vector3 (touchDeltaPosition.x, touchDeltaPosition.y, 0);

			forceVector = forceVector.normalized * 690;

			//scoreText.text = " Touch " + touchDeltaPosition.x + " " + touchDeltaPosition.y + " Player " + playerDeltaPosition.x + " " + playerDeltaPosition.y;

			Debug.Log ("Magnitude is " + forceVector.magnitude);

			rigidBody.AddForce (forceVector);

			prevTouch = touchDeltaPosition;
			//wait


		} 

		if (Input.GetTouch (0).phase == TouchPhase.Ended) {
			pressed = false;
		}
	}
		

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag.Equals ("ScoreWall")) {
			
			Debug.Log ("scored");

			ProceedLevelPanel.SetActive (true);

		}
	}

	public void ProceedToLevel1(){
		PlayerPrefs.SetFloat ("CameX", 0.63f);
		PlayerPrefs.SetFloat ("CameY", 0.03f);
		PlayerPrefs.SetFloat ("PlayerX", -5.76f);
		PlayerPrefs.SetFloat ("PlayerY", -0.5f);
		PlayerPrefs.SetInt ("CurrentLevel", 1);
		Debug.Log ("Level Decider 1");
		SceneManager.LoadScene ("main2");
	}


}
