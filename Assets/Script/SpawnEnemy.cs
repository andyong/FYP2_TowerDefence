﻿using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;

	// Use this for initialization
	void Start () {

        Instantiate(testEnemyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;

        //Debug.Log(testEnemyPrefab.GetComponent<MoveEnemy>().waypoints);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
