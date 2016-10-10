using UnityEngine;
using System.Collections;

public class MoveTowardsTarget : MonoBehaviour {

	[Tooltip("The target transform this GameObject will move towards.")]
	public Transform Target = null;			// the target this gameobject will move towards
	[Range(0.1f, 100f)]
	public float Speed = 5f;				// the speed at which this gameobject will move towards the target


	// Update is called once per frame
	void Update () {
	
		// if we don't have a target assigned then don't do anything
		if (Target == null) {
			return;
		}

		// set the "step" of the movement by time x speed
		float step = Speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards(
			this.transform.position, Target.position, step);	// assign the new position
	}
}