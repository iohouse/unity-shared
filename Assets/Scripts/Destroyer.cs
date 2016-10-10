using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
	{
		// destroy the gameobject that entered this
		Destroy(other.gameObject);
	}

	void OnCollisionEnter(Collision other)
	{
		// destroy the gameobject that collided with this
		Destroy(other.gameObject);
	}
}
