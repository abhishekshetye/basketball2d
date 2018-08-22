using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class MySceneManager : MonoBehaviour {


	public Animator anim;

	//music
	public MusicManager musicManager;

	public Image soundButtonImage;

	public Sprite musicOnSprite;

	public Sprite musicOffSprite;

	public GameObject player;

	public GameObject feedbackPanel;

	public string fbUrl;

	private Rigidbody2D rigidBody;

	private Vector3 forceVector;

	public void Start(){
		rigidBody = player.GetComponent<Rigidbody2D> ();
		forceVector = new Vector3 (0f, 1f, 0f);
		forceVector = forceVector.normalized * 500;

		//music init
		InitMusic();

		//Feedback
		TriggerFeedback();

		Advertisement.Initialize ("2753861");

		StartCoroutine(WaitAndJump ());
	}


	private void TriggerFeedback(){

		//increment number of time game was opened
		int gameOpened = PlayerPrefs.GetInt("GameOpened", 0);
		gameOpened++;
		PlayerPrefs.SetInt ("GameOpened", gameOpened);

		if (gameOpened == 4 && PlayerPrefs.GetInt ("Feedback", 0) == 0) {
			feedbackPanel.SetActive (true);
		}

	}

	public void showFeedbackPanel(){
		feedbackPanel.SetActive (true);
	}

	public void FeedbackButtonPressed(){

		PlayerPrefs.SetInt ("Feedback", 1);
		feedbackPanel.SetActive (false);
		string email = "thecodebreakergames@gmail.com";
		string subject = MyEscapeURL("Feedback for Cave Hopper");
		string body = MyEscapeURL("Feel free to drop compliments/complaints/things you would like to change/even things you find annoying in the game!");
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);

	}

	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}

	void Update()
	{

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	private void InitMusic(){
	
		int musicOn = PlayerPrefs.GetInt ("Music");

		Debug.Log ("Music Init " + musicOn);

		if (musicOn == 0) {
			//music should play
			if(!musicManager.isPlaying())
				musicManager.PlayMusic();
			soundButtonImage.sprite = musicOffSprite;
		} else {
			//music shouldn't play
			musicManager.StopMusic();
			soundButtonImage.sprite = musicOnSprite;
		}

	}


	private IEnumerator WaitAndJump(){

		Debug.Log ("Jumping");

		yield return new WaitForSeconds (2f);

		Debug.Log ("Magnitude is " + forceVector.magnitude);

		rigidBody.AddForce (forceVector);

		StartCoroutine(WaitAndJump ());
	}


	public void LoadLevel(){
		Debug.Log ("Loading level");

		anim.SetBool ("dark", true);

		//anim.SetBool ("trans", true);
		SceneManager.LoadScene ("levels");
		//StartCoroutine (WaitAndLoad());
	}


	private IEnumerator WaitAndLoad(){

		anim.SetBool ("dark", true);
		yield return new WaitForSeconds (0.18f);
		SceneManager.LoadScene ("levels", LoadSceneMode.Single);

	}

	public void ToggleMusic(){
	
		int musicOn = PlayerPrefs.GetInt ("Music");

		if (musicOn == 1) {
			//music is already off. Turning on
			musicManager.PlayMusic();
			PlayerPrefs.SetInt ("Music", 0);
			soundButtonImage.sprite = musicOffSprite;
		} else {
			//music was on. Turning off.
			musicManager.StopMusic();
			PlayerPrefs.SetInt ("Music", 1);
			soundButtonImage.sprite = musicOnSprite;
		}
	}


	public void OpenFacebookUrl(){
		Application.OpenURL(fbUrl);
	}

	public void OpenPlayStoreUrl(){
		Application.OpenURL("https://play.google.com/store/apps/developer?id=Abhishek+Shetye&hl=en_US");
	}
}
