using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Advertisements;

public class BallTouch : MonoBehaviour {


	public float speed = 0.01F;

	public Camera camera;

	public TextMeshProUGUI scoreText;

	public GameObject playPanel;

	public GameObject retryPanel;

	public GameObject retryPanelWithAds;

	public GameObject timedOutPanel;

	public GameObject proceedToNextLevelPanel;

	public GameObject fireBallPrefab;

	public GameObject moreLevelsPanel;

	public Animator anim;

	private Rigidbody2D rigidBody;

	private int score;

	private bool pressed = false;

	private bool gameIsOver = false;

	private bool gameIsPaused = false;

	private int ultimateAliveCount = 0;

	private bool ultimateALive = false;

	private int currentLevel;
	private GameObject instObj;

	public int countDown = 150;

	public Transform righttop;
	public Transform rightbottom;
	public Transform lefttop;
	public Transform leftbottom;
	private Vector3 prevTouch;



	void Start(){
		
		rigidBody = this.GetComponent<Rigidbody2D> ();

		currentLevel = PlayerPrefs.GetInt ("CurrentLevel", 1);

		Debug.Log ("Current Level is " + currentLevel);

		//Instantiate(player, new Vector3(-5.69F, 0.87f, 26.3f), Quaternion.identity);

		Time.timeScale = 1.5f;

		if (Application.platform == RuntimePlatform.Android) {
			print ("Android");

			speed = 2.5f;
		}

		score = 0;

		if (righttop != null) {
			righttop.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
			rightbottom.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0));
			lefttop.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, 0));
			leftbottom.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));   
		}

		if (currentLevel == 1) {
			countDown = 30;
		} else if (currentLevel == 2) {
			countDown = 50;
		} else if (currentLevel == 3) {
			countDown = 45;
			//level 3
			StartCoroutine (SpawnFireBall());
			StartCoroutine (SpawnFireBall2());
		}

		StartCoroutine (StartCountDown());


	}



	private IEnumerator SpawnFireBall(){
	
		instObj = Instantiate (fireBallPrefab, new Vector3(17.0f, -27.5f, 0.08f), Quaternion.identity);

		Destroy (instObj, 5f);
		float sec = Random.Range (0, 3);
		yield return new WaitForSeconds (sec);

		StartCoroutine (SpawnFireBall());

	}


	private IEnumerator SpawnFireBall2(){

		instObj = Instantiate (fireBallPrefab, new Vector3(81.16f, -27.5f, 0.08f), Quaternion.identity);

		Destroy (instObj, 5f);
		float sec = Random.Range (0, 3);
		yield return new WaitForSeconds (sec);

		StartCoroutine (SpawnFireBall2());

	}

	private IEnumerator StartCountDown(){

		scoreText.text = "Time Remaining : " + countDown + " sec";

		yield return new WaitForSeconds (1.5f);

		if (countDown > 0 && !gameIsOver) {

			countDown--;
		}

		StartCoroutine (StartCountDown ());

	}


	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene ("levels");
		}

		if (countDown == 0) {
			if (!gameIsOver) {
				gameIsOver = true;
				timedOutPanel.SetActive (true);
			}
		}

		if (Input.GetMouseButtonDown (0)) {

			var pauseButton = new Rect (Screen.width * 3/4f, Screen.height * 3/4f, Screen.width * 1/4f, Screen.height * 1/4f);

			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.mousePosition;

			if (pauseButton.Contains (Input.mousePosition) || gameIsPaused) {
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

			if(!gameIsOver)
				rigidBody.AddForce (forceVector);

			// Move object across XY plane
			//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

		}
		
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved && !pressed) 
		{

			pressed = true;
			//move
			var pauseButton = new Rect (Screen.width * 3/4f, Screen.height * 3/4f, Screen.width * 1/4f, Screen.height * 1/4f);

			if (pauseButton.Contains (Input.GetTouch (0).position) || gameIsPaused) {
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

			if(!gameIsOver)
				rigidBody.AddForce (forceVector);

			prevTouch = touchDeltaPosition;
			//wait


		} 

		if (Input.GetTouch (0).phase == TouchPhase.Ended) {
			pressed = false;
		}
	}


	private void moveObject(){
	


		//yield return new WaitForSeconds (0.1f);
	}


	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag.Equals ("ScoreWall")) {
			
			//score += 10;

			if (!gameIsOver) {

				gameIsOver = true;
				PlayerPrefs.SetString ("LevelStatus" + currentLevel, "completed");
				PlayerPrefs.SetInt ("LevelStatus" + currentLevel, 1);
				proceedToNextLevelPanel.SetActive (true);
			}


			//scoreText.text = "Score : " + score;

			Debug.Log ("scored");

		} else if (other.collider.tag.Equals ("DangerWall") && !ultimateALive && !gameIsOver) {

			//Time.timeScale = 0f;

			//scoreText.text = " GAME OVER ";
			int prob_num = Random.Range(0, 4);
			Debug.Log ("PROB NUM IS " + prob_num);
			if (prob_num == 2 && ultimateAliveCount < 3)
				retryPanelWithAds.SetActive (true);
			else
				retryPanel.SetActive (true);

			gameIsOver = true;
		} 
	}

	public void DismissGameComplete(){
		moreLevelsPanel.SetActive (false);
		SceneManager.LoadScene ("start");
	}


	public void Pause(){
		if (gameIsOver)
			return;
		Time.timeScale = 0f;
		gameIsPaused = true;
		Debug.Log ("Clicked pause button");
		playPanel.SetActive (true);
	
	}

	public void Play(){
		gameIsPaused = false;
		Debug.Log ("Clicked play button");
		Time.timeScale = 1.5f;
		playPanel.SetActive (false);
	}

	public void RestartGame(){
		int prob_num = Random.Range(0, 3);
		if(prob_num == 1 && Advertisement.IsReady("displayads"))
		{
			Advertisement.Show("displayads");
		}
		Time.timeScale = 1.5f;
		SceneManager.LoadScene ("main2");

	}

	public void ProceedToNextLevel(){
		Debug.Log ("Current Level was " + currentLevel);
		if (currentLevel == 3) {
			moreLevelsPanel.SetActive (true);
			proceedToNextLevelPanel.SetActive (false);
			return;
		}
		LevelDecider.ProceedToNextLevel (currentLevel);
	}

	public void WatchAVideo(){
		ultimateAliveCount++;
		if(Advertisement.IsReady("video"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("video", options);
		}


	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			gameIsOver = false;
			retryPanelWithAds.SetActive (false);
			ultimateALive = true;
			StartCoroutine (WatchedVideoKeepPlayerAlive());
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			gameIsOver = false;
			retryPanelWithAds.SetActive (false);
			ultimateALive = true;
			StartCoroutine (WatchedVideoKeepPlayerAlive());
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	private IEnumerator WatchedVideoKeepPlayerAlive(){
		yield return new WaitForSeconds (5f);
		ultimateALive = false;
	}

	public void LoadHomeScene(){
		Destroy (GameObject.Find("MusicManager"));
		SceneManager.LoadScene ("start");

	}


}
