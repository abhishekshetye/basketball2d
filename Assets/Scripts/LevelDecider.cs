using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelDecider : MonoBehaviour {

	public GameObject player;

	public Button level1Button;

	public Button level2Button;

	public Button level3Button;


	public GameObject firstCompletePreviousLevelPanel;

	public GameObject loadingPanel;


	public TextMeshProUGUI levelText;

	private Rigidbody2D rigidBody;
	private Vector3 forceVector;


	public static void enterLevel1(){
		PlayerPrefs.SetFloat ("CameX", 0.63f);
		PlayerPrefs.SetFloat ("CameY", 0.03f);
		PlayerPrefs.SetFloat ("PlayerX", -5.76f);
		PlayerPrefs.SetFloat ("PlayerY", -0.5f);
		PlayerPrefs.SetInt ("CurrentLevel", 1);
		Debug.Log ("Level Decider 1");
		SceneManager.LoadScene ("main2");
	}

	public static void enterLevel2(){
		PlayerPrefs.SetFloat ("CameX", 0.9f);
		PlayerPrefs.SetFloat ("CameY", -15.64f);
		PlayerPrefs.SetFloat ("PlayerX", -4.94f);
		PlayerPrefs.SetFloat ("PlayerY", -15.83f);
		PlayerPrefs.SetInt ("CurrentLevel", 2);
		Debug.Log ("Level Decider 2");
		SceneManager.LoadScene ("main2");
	}

	public static void enterLevel3(){
		PlayerPrefs.SetFloat ("CameX", 0.9f);
		PlayerPrefs.SetFloat ("CameY", -31.51f);
		PlayerPrefs.SetFloat ("PlayerX", -4.45f);
		PlayerPrefs.SetFloat ("PlayerY", -31.01f);
		PlayerPrefs.SetInt ("CurrentLevel", 3);
		Debug.Log ("Level Decider 3");
		SceneManager.LoadScene ("main2");
	}



	public static void ProceedToNextLevel(int currentLevel){
		int nextLevel = currentLevel + 1;
		switch (nextLevel) {

		case 1:
			enterLevel1 ();
			break;

		case 2:
			enterLevel2 ();
			break;

		case 3:
			enterLevel3 ();
			break;

		default:
			MoreLevelsComingUp ();
			break;
		}
	}

	public void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene ("start");
		}

	}

	//----------NON STATIC IMPL--------

	private void EnterLevel1(){

		if (PlayerPrefs.GetInt ("First_Time", 0) == 0) {

			SceneManager.LoadScene ("Practise");
			return;
		}

		PlayerPrefs.SetFloat ("CameX", 0.63f);
		PlayerPrefs.SetFloat ("CameY", 0.03f);
		PlayerPrefs.SetFloat ("PlayerX", -5.76f);
		PlayerPrefs.SetFloat ("PlayerY", -0.5f);
		PlayerPrefs.SetInt ("CurrentLevel", 1);
		Debug.Log ("Level Decider 1");
		SceneManager.LoadScene ("main2");

	}

	private void EnterLevel2(){
		PlayerPrefs.SetFloat ("CameX", 0.9f);
		PlayerPrefs.SetFloat ("CameY", -15.64f);
		PlayerPrefs.SetFloat ("PlayerX", -4.94f);
		PlayerPrefs.SetFloat ("PlayerY", -15.83f);
		PlayerPrefs.SetInt ("CurrentLevel", 2);
		Debug.Log ("Level Decider 2");
		SceneManager.LoadScene ("main2");
	}

	private void EnterLevel3(){
		PlayerPrefs.SetFloat ("CameX", 0.9f);
		PlayerPrefs.SetFloat ("CameY", -31.51f);
		PlayerPrefs.SetFloat ("PlayerX", -4.45f);
		PlayerPrefs.SetFloat ("PlayerY", -31.01f);
		PlayerPrefs.SetInt ("CurrentLevel", 3);
		Debug.Log ("Level Decider 3");
		SceneManager.LoadScene ("main2");
	}

	// --------- BUTTON CALLERS ---------

	public void ClickedLevel1Button(){
		StartCoroutine (LoadAndWait (1));
	}

	public void ClickedLevel2Button(){
		StartCoroutine (LoadAndWait (2));
	}

	public void ClickedLevel3Button(){
		StartCoroutine (LoadAndWait (3));
	}

	// -------------------------------------

	private IEnumerator LoadAndWait(int level){

		if (HandleLevelClick (level)) {

			loadingPanel.SetActive (true);

			yield return new WaitForSeconds (1f);

			switch (level) {

			case 1:
				EnterLevel1 ();
				break;

			case 2:
				EnterLevel2 ();
				break;

			case 3:
				EnterLevel3 ();
				break;

			default:
				MoreLevelsComingUp ();
				break;
			}
		}

	}



	public static void MoreLevelsComingUp(){
		Debug.Log ("More Levels coming up");
	}


	private void Start(){
		Time.timeScale = 1.5f;
		rigidBody = player.GetComponent<Rigidbody2D> ();
		forceVector = new Vector3 (0f, 1f, 0f);
		forceVector = forceVector.normalized * 500;
		StartCoroutine(WaitAndJump ());
	}


	private bool HandleLevelClick(int level){
	

		if (!ShouldBeAbleToEnterTheLevel (level)) {
		
			levelText.text = "YOU NEED TO COMPLETE LEVEL " + (level-1) + " BEFORE PROCEEDING TO LEVEL " + level;
		
			firstCompletePreviousLevelPanel.SetActive (true);

			return false;
		}
		
		return true;
	}

	private bool ShouldBeAbleToEnterTheLevel (int currentLevel){
		if (currentLevel == 1)
			return true;

		int prevLevel = currentLevel - 1;
		int levelStatus = PlayerPrefs.GetInt ("LevelStatus" + prevLevel, 0);
		if (levelStatus != 0)
			return true;
		return false;
	}

	private IEnumerator WaitAndJump(){

		Debug.Log ("Jumping");

		yield return new WaitForSeconds (2f);

		Debug.Log ("Magnitude is " + forceVector.magnitude);

		rigidBody.AddForce (forceVector);

		StartCoroutine(WaitAndJump ());
	}

	public void Dismiss(){
		firstCompletePreviousLevelPanel.SetActive (false);
	}

}
