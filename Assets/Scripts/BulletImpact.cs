using UnityEngine;
using System.Collections;

public class BulletImpact : MonoBehaviour {

	public GameObject explosion;


	void OnCollisionEnter()	//Collision other)
	{
		Destroy(this.gameObject);
	
		if (explosion != null) {
			Instantiate(explosion, transform.position, transform.rotation);
		}
	}
}
