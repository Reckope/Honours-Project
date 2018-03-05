﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour {

	Vector2 initialPositionCrab = new Vector2 (11f, -3.8f); // - 3.8
	Vector2 currentPosition;

	private float speed = 6f;
	private float direction = -1f;
	private float upwardForce = 500f;

	public static float upwardDirection = 1f;
	private float heightLimit = 2f;


	// Use this for initialization
	void Start () {

		transform.position = initialPositionCrab;
		upwardDirection = 1f;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (GameControl.instance.gameOver == true || GameControl.instance.questionTime == true) {

		}

			else if (GameControl.instance.gameOver == false || GameControl.instance.questionTime == false) { 

			MoveCrab ();
			if (transform.position.x <= -10f) {
				RespawnCrab ();
			}
		}
	}

	void MoveCrab(){

		transform.Translate (direction * speed * Time.deltaTime * 1, 0, 0);

		if (transform.position.x > -15f && transform.position.x < -1f) {

			transform.Translate (0, upwardDirection*speed*Time.deltaTime * 1, 0);
			Debug.Log ("IN ZONE");

			if (transform.position.y > -1) {
				upwardDirection = -1;
			}

			if (transform.position.y < -4f) {
				Debug.Log ("STOP");
				upwardDirection = 0f;
				transform.Translate (direction * speed * Time.deltaTime * 1, 0, 0);
			}

		}

	}

	void RespawnCrab(){

		transform.position = new Vector2 (11f, -3.8f);
		upwardDirection = 1f;
	}

}


