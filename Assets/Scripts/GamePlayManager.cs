﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour {

	//adjust this to change speed
	public float speed = 3f;
	//adjust this to change how high it goes
	public float height = 2f;

	public GameObject prop;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update() {
		//get the objects current position and put it in a variable so we can access it later with less code
		Vector3 pos = prop.transform.position;
		//calculate what the new Y position will be
		float newY = Mathf.Sin(Time.time * speed);
		//set the object's Y to the new calculated Y
		prop.transform.position = new Vector3(pos.x, newY, 0f).normalized * height;	
	}

}
