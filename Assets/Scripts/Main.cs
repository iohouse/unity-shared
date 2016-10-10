using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public KeyCode SpawnKey = KeyCode.Space;				// what key to press to spawn the object
	public bool UseTouchOnMobile = true;					// whether to spawn object via touch input on mobile device platform
	public GameObject ObjectToSpawn = null;					// the prefab of the object to spawn
	public Transform SpawnPoint = null;						// the location to spawn the ObjectToSpawn at
	public bool MoveObjectToTag = true;						// whether the spawned object will move towards the game object tagged Enemy
	public Tags.TagNames MoveToTag = Tags.TagNames.Enemy;	// the tag the spawned object should move towards


	// Update is called once per frame
	void Update () {
	
		bool isSpawn = false;								// initialize the var we'll check for whether to spawn and object

#if UNITY_ANDROID || UNITY_IOS

		if (UseTouchOnMobile)
		{
			int fingerCount = 0;
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
					fingerCount++;
			}
			if (fingerCount > 0) isSpawn = true;
		}

#else		

		// spawn object based on key press on standalone platforms
		if (Input.GetKeyDown(SpawnKey)) isSpawn = true;

#endif

		if (isSpawn)
		{
			// spawn a prefab at the spawn point
			if (ObjectToSpawn != null && SpawnPoint != null) {
			
				GameObject goInstance = Instantiate (ObjectToSpawn, SpawnPoint.position, Quaternion.identity) as GameObject;
			
				// if the game object should move towards the objet with "enemy" tag
				if (MoveObjectToTag) {

					GameObject goMoveTowards = GameObject.FindGameObjectWithTag (MoveToTag.ToString());
					if (goMoveTowards != null) {

						// set the enemy as the move toward target
						MoveTowardsTarget mtt = goInstance.GetComponent<MoveTowardsTarget>();
						if (mtt == null)
							mtt = goInstance.AddComponent<MoveTowardsTarget> ();
						
						// assign the target object to the instantiated object property -- so it starts moving towards it
						mtt.Target = goMoveTowards.transform;
					}
				}
			
			}
		}
	}
}
