using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {

	[Tooltip("Lifetime value of 0 means the object will not destroy itself over time.")]
	public float Lifetime = 3f;					// how long this gameobject will exist in the hierarchy before destroying itself
    public bool DestroyOnCollision = true;
    public Tags.TagNames[] AffectedTags;


    // Update is called once per frame
	void Update () {

		// check the lifetime of this gameobject and destroy if lifetime is met
		if (Lifetime > 0) {
			Lifetime -= Time.deltaTime;

			if (Lifetime <= 0) {
				Destroy (this.gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		DestroyThis (other.gameObject);
	}

	void OnCollisionEnter(Collision other)
	{
		DestroyThis (other.gameObject);
	}


	/// <summary>
	/// 	Destroys this gameobject if an object with the affected tag is collided with
	/// </summary>
	/// <param name="collision">Collision.</param>
	/// 
	void DestroyThis(GameObject other)
	{
		if (DestroyOnCollision && (AffectedTags != null && AffectedTags.Length > 0))
		{
			for (int i = 0; i < AffectedTags.Length; i++)
			{
				if (other.tag == AffectedTags[i].ToString() 
					|| AffectedTags[i].ToString() == Tags.ALL)
				{
					Destroy(this.gameObject);
					break;
				}
			}
		}

	}
}
