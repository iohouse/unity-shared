using UnityEngine;
using System.Collections;

public class SpawnThings : MonoBehaviour {

	public GameObject toSpawn;
	GameObject[] spawns;
	float next=0;
	int total = 0;
	// Use this for initialization
	void Start () {
		// look for all objects marked spawnpoint
		spawns=GameObject.FindGameObjectsWithTag("spawnpoint");
		next=Time.time+Random.Range(1,2);
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time>next) && (total < 15 )){
			next=Time.time+Random.Range(1,5);
			int id=Random.Range(0,spawns.Length);
			Instantiate(toSpawn, spawns[id].transform.position,Quaternion.identity);
			total ++;
		}
	}
}
