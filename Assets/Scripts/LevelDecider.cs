using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDecider : MonoBehaviour {


	public void Start(){
	}

	public void enterLevel1(){
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetFloat ("CameX", 0.63f);
		PlayerPrefs.SetFloat ("CameY", 0.03f);
		PlayerPrefs.SetFloat ("PlayerX", -5.76f);
		PlayerPrefs.SetFloat ("PlayerY", -0.5f);
		Debug.Log ("Level Decider 1");
		SceneManager.LoadScene ("main2");
	}


	public void enterLevel2(){
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetFloat ("CameX", 0.9f);
		PlayerPrefs.SetFloat ("CameY", -15.64f);
		PlayerPrefs.SetFloat ("PlayerX", -4.94f);
		PlayerPrefs.SetFloat ("PlayerY", -15.83f);
		Debug.Log ("Level Decider 2");
		SceneManager.LoadScene ("main2");
	}

	public void enterLevel3(){
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetFloat ("CameX", 0.9f);
		PlayerPrefs.SetFloat ("CameY", -31.51f);
		PlayerPrefs.SetFloat ("PlayerX", -4.45f);
		PlayerPrefs.SetFloat ("PlayerY", -31.01f);
		Debug.Log ("Level Decider 3");
		SceneManager.LoadScene ("main2");
	}

}
