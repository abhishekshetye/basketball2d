using UnityEngine;
using System.Collections;

public class MovePractiseCamera : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	void Start ()
	{
		offset = transform.position - player.transform.position;
		Debug.Log ("Offset is " + offset);
	}

	void LateUpdate ()
	{
		Vector3 pos = new Vector3 (player.transform.position.x + 5.8f, this.transform.position.y, -10f);
		transform.position =  pos ; //+ offset;
	}
}