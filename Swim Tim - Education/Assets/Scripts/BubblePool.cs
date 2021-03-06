﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePool : MonoBehaviour {

	public GameObject foodPrefab;

	public int foodPoolSize = 7;
	public float spawnRate = 10f;

	private float lastSpawnTime;
	//private GameObject foodPre;
	private GameObject[] foodPool;
	private Vector2 foodPoolPosition = new Vector2 (20f, -20f);	// Position where the pool initially spawns

	private float spawnXPosition = 20f;
	private float spawnYPosition;

	private float minYPosition = -2.70f;
	private float maxYPosition = 3.20f;

	private int currentFood = 0;



	// Use this for initialization
	void Start () {

		lastSpawnTime = 0f;
		foodPool = new GameObject[foodPoolSize];
		for (int i = 0; i < foodPoolSize; i++) {
			foodPool [i] = (GameObject)Instantiate (foodPrefab, foodPoolPosition, Quaternion.identity);
		}
	}

	// Update is called once per frame
	void Update () {

		lastSpawnTime += Time.deltaTime;

		if (lastSpawnTime >= spawnRate) 
		{
			lastSpawnTime = 0f;
			spawnYPosition = Random.Range (minYPosition, maxYPosition);
			foodPool[currentFood].transform.position = new Vector2 (spawnXPosition, spawnYPosition);
			currentFood++;

			if (currentFood >= foodPoolSize) 
			{
				currentFood = 0;
			}
		}

	}
}
