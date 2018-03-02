﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public static GameControl instance;

	public GameObject GameOver;
	public GameObject Menu;
	public GameObject QuestionTime;
	public GameObject QuestionBackground;

	public Text scoreText;
	public Text gameOverText;
	public Text QuestionTimerText;
	public Text nextQuestionTimerText;
	public Text totalTimeText;
	public Text deathReasonText;

	public Text questionText;
	public Text firstChoiceText;
	public Text secondChoiceText;
	public Text thirdChoiceText;
	public Text fourthChoiceText;
	public Text correctText;

	public bool gameOver = false;
	public bool questionTime = false;

	public float scrollSpeed = -6.5f;
	public float tunaSpeed;
	public float enemySpeed;

	private float QuestionTimer = 12.0f;
	private float nextQuestionTimer;

	public static int score = 0;
	public static float totalTime = 0f;
	public static int deathReason;

	// Use this for initialization
	void Awake () {

		nextQuestionTimer = 15f;
		totalTime = 0f;
		Tim.deadStatus = false;
		Application.targetFrameRate = 600;

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		
	}

	// Update is called once per frame
	void Update () {

		StartNextQuestionTimer ();
		StartTotalTime ();

		if(questionTime == true){

			StartQuestionTimer ();

			if (QuestionTimer < 0.0f) {

				QuestionTimerText.text = "0";
				QuestionTime.SetActive (false);
				deathReason = 4;
				TimDied ();
			}
		}
		
	}

	public void TimDied() {

		SelectDeathReason ();
		Tim.deadStatus = true;
		QuestionTime.SetActive (false);
		QuestionBackground.SetActive (true);
		gameOverText.text = "Final Score: " + score.ToString ();
		DisplayTotalTime ();
		scoreText.text = null;
		GameOver.SetActive (true);
		Menu.SetActive (true);
		gameOver = true;
		Tim.ani.SetTrigger ("TimDead");

	}

	public void TimScored(){

		if (gameOver == true) {
			return;
		}
			
		score++;
		scoreText.text = "Score: " + score.ToString ();

	}

	public void DisplayQuestion() {

		questionTime = true;
		QuestionTime.SetActive (true);
		QuestionBackground.SetActive (true);

	}

	public void completeQuestion() {
		
		questionTime = false;
		QuestionTime.SetActive (false);
		QuestionBackground.SetActive (false);
		QuestionTimer = 12.0f;
		nextQuestionTimer = Random.Range (15f, 25f);
		nextQuestionTimerText.color = new Color (199.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
		QuestionTimerText.color = Color.white;
		nextQuestionTimerText.fontSize = 17;
		CharacterHealth.currentHealth = 20f;
		QuestionsControl.randomQuestion = -1;
		StartCoroutine (DisplayCorrectText ());
	}

	public IEnumerator DisplayCorrectText () {

			correctText.text = "CORRECT";
			yield return new WaitForSeconds (1);
			correctText.text = " ";
	}

	public void StartQuestionTimer() {

		QuestionTimer -= Time.deltaTime;
		QuestionTimerText.text = QuestionTimer.ToString ("N0");
		if(QuestionTimer < 3.50f && QuestionTimer >= 0){
			QuestionTimerText.color = Color.red;
		}

	}

	public void StartNextQuestionTimer (){

		if (questionTime == false && Tim.deadStatus == false) {
			nextQuestionTimer -= Time.deltaTime;
			nextQuestionTimerText.text = "Next Question: " + nextQuestionTimer.ToString ("N0");

			if(nextQuestionTimer < 3.50f && nextQuestionTimer >= 0){
				nextQuestionTimerText.color = Color.red;
				nextQuestionTimerText.fontSize = 26;
			}

			if(nextQuestionTimer < 0.0f){
				DisplayQuestion ();
			}
		}

	}

	public void StartTotalTime(){
		if (questionTime == false && Tim.deadStatus == false) {
			totalTime += Time.deltaTime;
		}
	}

	public void IncreaseGameSpeed(){

		scrollSpeed = (scrollSpeed - (float)0.08); 			
		tunaSpeed = (tunaSpeed - (float)0.06);
		enemySpeed = (enemySpeed - (float)0.08);

	}

	public void DisplayTotalTime(){

		if (totalTime > 0.95f && totalTime <= 1.49f) {
			totalTimeText.text = "Time Survived: " + totalTime.ToString ("F2") + " Second";
		}

		if (totalTime > 1.49f) {
			totalTimeText.text = "Time Survived: " + totalTime.ToString ("F2") + " Seconds";
		}
	}

	public void SelectDeathReason(){
		switch(deathReason){
			case 4:
				deathReasonText.text = "You ran out of time!";
				break;	
			case 3:
				deathReasonText.text = "You Lost All Your Health!";
				break;
			case 2:
				deathReasonText.text = "You Got The Question Wrong!";
				break;
			case 1:
				deathReasonText.text = "You Hit Something!";
				break;
			default:
				deathReasonText.text = "You Died!";
				break;
		}
	}
}
