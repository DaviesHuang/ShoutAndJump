using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

	public Transform prefabToSpawn;
	public float nextSpawn;
	public float spawnRate;
	public float randomDelay;
	public float speedUpRate;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn) {
			Instantiate (prefabToSpawn, transform.position, Quaternion.identity);
			nextSpawn = Time.time + spawnRate + Random.Range (0, randomDelay);
			spawnRate = spawnRate - speedUpRate;
		}
	}
}


