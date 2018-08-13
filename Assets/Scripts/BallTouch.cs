using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallTouch : MonoBehaviour {


	public float speed = 0.01F;

	public Camera camera;

	public Text scoreText;

	public Button retryButton;

	private Rigidbody2D rigidBody;

	private int score;

	private bool pressed = false;

	public Transform righttop;
	public Transform rightbottom;
	public Transform lefttop;
	public Transform leftbottom;


	void Start(){
		
		rigidBody = this.GetComponent<Rigidbody2D> ();

		//Instantiate(player, new Vector3(-5.69F, 0.87f, 26.3f), Quaternion.identity);

		Time.timeScale = 1.3f;

		scoreText.text = "Score : " + score;

		if (Application.platform == RuntimePlatform.Android) {
			print ("Android");

			speed = 2.5f;

			scoreText.text = "Android Score : " + score;
		}

		retryButton.gameObject.SetActive (false);

		score = 0;

		if (righttop != null) {
			righttop.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
			rightbottom.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0));
			lefttop.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, 0));
			leftbottom.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));   
		}


	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) {

			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.mousePosition;

			Debug.Log ( " Touch " + touchDeltaPosition.x + " " + touchDeltaPosition.y);

			Vector3 playerDeltaPosition = camera.WorldToScreenPoint (transform.position);

			Debug.Log ( " Player " + playerDeltaPosition.x + " " + playerDeltaPosition.y);

			Vector3 forceVector = playerDeltaPosition - new Vector3( touchDeltaPosition.x, touchDeltaPosition.y, 0);

			//forceVector = forceVector.normalized * 720f;

			forceVector = forceVector.normalized * 690f;

			Debug.Log ("Magnitude is " + forceVector.magnitude);

			rigidBody.AddForce (forceVector);

			// Move object across XY plane
			//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

		}
		
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved && !pressed) {

			pressed = true;

			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;

			Debug.Log (" Touch " + touchDeltaPosition.x + " " + touchDeltaPosition.y);

			Vector3 playerDeltaPosition = camera.WorldToScreenPoint (transform.position);

			Debug.Log (" Player " + playerDeltaPosition.x + " " + playerDeltaPosition.y);

			Vector3 forceVector = playerDeltaPosition - new Vector3 (touchDeltaPosition.x, touchDeltaPosition.y, 0);

			forceVector = forceVector.normalized * 720f;

			scoreText.text = " Touch " + touchDeltaPosition.x + " " + touchDeltaPosition.y + " Player " + playerDeltaPosition.x + " " + playerDeltaPosition.y;

			Debug.Log ("Magnitude is " + forceVector.magnitude);

			rigidBody.AddForce (forceVector);

		} 

		if (Input.GetTouch (0).phase == TouchPhase.Ended) {
			pressed = false;
		}
	}


	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag.Equals ("ScoreWall")) {
			
			score += 10;

			scoreText.text = "Score : " + score;

			Debug.Log ("scored");

		} else if (other.collider.tag.Equals ("DangerWall")) {

			Time.timeScale = 0f;

			scoreText.text = " GAME OVER ";

			retryButton.gameObject.SetActive (true);
		} 
	}



	public void RestartGame(){

		Time.timeScale = 1f;
		SceneManager.LoadScene ("main2");

	}


}
