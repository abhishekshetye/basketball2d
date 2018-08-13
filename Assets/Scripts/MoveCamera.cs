using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	void Start ()
	{
		offset = transform.position - player.transform.position;
	}

	void LateUpdate ()
	{
		Vector3 pos = new Vector3 (player.transform.position.x, -0f, 0f);
		//transform.position = player.transform.position + offset;
		transform.position =  pos + offset;
	}
}