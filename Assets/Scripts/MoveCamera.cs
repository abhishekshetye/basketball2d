using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	void Start ()
	{
		Debug.Log ("Came positions " + PlayerPrefs.GetFloat("CameX") + " " + PlayerPrefs.GetFloat("CameY"));
		this.transform.position = new Vector3 (PlayerPrefs.GetFloat("CameX"), PlayerPrefs.GetFloat("CameY"), -10f);
		player.transform.position = new Vector3 (PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), 0.53f);
		PlayerPrefs.DeleteAll ();

		offset = transform.position - player.transform.position;
		Debug.Log ("Offset is " + offset);
	}

	void LateUpdate ()
	{
		Vector3 pos = new Vector3 (player.transform.position.x + 5.8f, this.transform.position.y, -10f);
		//Vector3 pos = new Vector3 (player.transform.position.x, 0f , 0f);
		//transform.position = player.transform.position + offset;
		transform.position =  pos ; //+ offset;
	}
}