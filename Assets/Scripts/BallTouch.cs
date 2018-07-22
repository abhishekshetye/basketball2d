using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTouch : MonoBehaviour {


	public float speed = 0.01F;

	public Camera camera;

	public GameObject plane;

	private Rigidbody rigidBody;


	public Transform righttop;
	public Transform rightbottom;
	public Transform lefttop;
	public Transform leftbottom;


	void Start(){
		
		rigidBody = this.transform.GetComponent<Rigidbody> ();


		righttop.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));
		rightbottom.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0));
		lefttop.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0));
		leftbottom.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));   


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

			rigidBody.AddForce (forceVector * speed);

			// Move object across XY plane
			//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

		}
		
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

			Debug.Log ( " Touch " + touchDeltaPosition.x + " " + touchDeltaPosition.y);

			Vector3 playerDeltaPosition = camera.WorldToScreenPoint (transform.position);

			Debug.Log ( " Player " + playerDeltaPosition.x + " " + playerDeltaPosition.y);

			Vector3 forceVector = playerDeltaPosition - new Vector3( touchDeltaPosition.x, touchDeltaPosition.y, 0);

			rigidBody.AddForce (forceVector * speed);

		}
	}


}
