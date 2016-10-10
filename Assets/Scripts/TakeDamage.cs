using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AudioSource))]
public class TakeDamage : MonoBehaviour {

    public Tags.TagNames[] AffectedTags;
    public GameObject prefabDamage = null;                   // prefab to instantiate when object destroyed
    public AudioClip audioDamage = null;                     // audio clip to play when the object is destroyed

    private Health _health;


    // Use this for setting references before Start
    void Awake () {

        _health = this.gameObject.GetComponent<Health>();
    }

	void OnTriggerEnter(Collider other)
	{
		ApplyDamage (other.gameObject);
	}

    void OnCollisionEnter(Collision other)
    {
		ApplyDamage (other.gameObject);
	}


	/// <summary>
	/// 	Applies the damage asigned to the colliding game object to this game object
	/// </summary>
	/// <param name="other">Other.</param>
	/// 
	void ApplyDamage(GameObject other)
	{
		// can we take damage from the object that collided with us?
		for (int i = 0; i < AffectedTags.Length; i++)
		{
			if (other.tag == AffectedTags[i].ToString() || AffectedTags[i].ToString() == Tags.ALL)
			{
				Debug.Log(string.Format("'{0}' collided by object with Tag '{1}'", 
					this.gameObject.name, AffectedTags[i].ToString()));

				// get the damage amount from the object that collided -- if it has a Damage value
				Damage damage = other.GetComponent<Damage>();
				if (damage != null)
				{
					Debug.Log(string.Format("Applying Damage of {0} to '{1}' from collision object '{2}'",
						damage.DamageAmount, this.gameObject.name, other.name));

					// do stuff when taking damage
					if (prefabDamage != null)
						Instantiate(prefabDamage, other.transform.position, other.transform.rotation);

					if (audioDamage != null)
					{
						AudioSource aud = this.gameObject.GetComponent<AudioSource>();
						aud.clip = audioDamage;
						aud.Play();
					}

					// now apply damage to Health
					_health.ApplyHealth(-damage.DamageAmount);
				}
				break;
			}
		}
	}
}
