using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {


	public Animator anim;

	//music
	public MusicManager musicManager;

	public Image soundButtonImage;

	public Sprite musicOnSprite;

	public Sprite musicOffSprite;

	public GameObject player;

	private Rigidbody2D rigidBody;

	private Vector3 forceVector;

	public void Start(){
		rigidBody = player.GetComponent<Rigidbody2D> ();
		forceVector = new Vector3 (0f, 1f, 0f);
		forceVector = forceVector.normalized * 500;

		//music init
		InitMusic();

		StartCoroutine(WaitAndJump ());
	}


	private void InitMusic(){
	
		int musicOn = PlayerPrefs.GetInt ("Music");

		Debug.Log ("Music Init " + musicOn);

		if (musicOn == 0) {
			//music should play
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
}
