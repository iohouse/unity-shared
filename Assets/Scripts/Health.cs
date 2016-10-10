using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(AudioSource))]
public class Health : MonoBehaviour {
	
	public int HealthAmount = 100; 
	public Slider healthSlider;                                 // Reference to the UI's health bar.
    public bool DestroyWhenHealthZero = false;                  // should we destroy this object when health reaches zero?
    public bool DestroyParent = false;                          // if this object should destroy it's containing parent instead of this
    public GameObject prefabDestroyed = null;                   // prefab to instantiate when object destroyed
    public AudioClip audioDestroyed = null;                     // audio clip to play when the object is destroyed

    private AudioSource _aud;


    // Use to set references before Start
    void Awake () {
        
        _aud = this.gameObject.GetComponent<AudioSource>();
    }


    public void ApplyHealth(int amount)
    {
        StartCoroutine(IEApplyHealth(amount));
    }

    private IEnumerator IEApplyHealth(int amount)
    {
        HealthAmount += amount;

        if (healthSlider != null)
            healthSlider.value = HealthAmount;
        
        if (HealthAmount <= 0)
        {
            // if the health of this object is on the Player then game over!
            if (this.gameObject.tag == Tags.Player)
            {
                PlayerDie();
            }
            else
            {
                // do the stuff when health is zero
                if (DestroyWhenHealthZero)
                {
                    // first stop all child audio sources from playing (such as for any continuous looped audio)
                    AudioSource[] asources = this.gameObject.GetComponentsInChildren<AudioSource>();
                    foreach (AudioSource asrc in asources)
                        asrc.Stop();


                    if (prefabDestroyed != null)
                        Instantiate(prefabDestroyed, this.gameObject.transform.position, this.gameObject.transform.rotation);

                    if (audioDestroyed != null)
                    {
                        _aud.clip = audioDestroyed;
                        _aud.Play();
                    }


                    // delay destroying object if "destroyed" audio is playing
                    if (_aud.isPlaying)
                    {
                        if (DestroyParent)
                        {
                            // find mesh renderer from the parent
                            this.transform.parent.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                            this.transform.parent.gameObject.GetComponentInChildren<Collider>().enabled = false;
                        }
                        else
                        {
                            // disable the mesh and collider so the object cannot be interacted with while waiting for audio to finish playing before destroying
                            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                            this.gameObject.GetComponent<Collider>().enabled = false;
                        }

                        while (_aud.isPlaying)
                            yield return new WaitForSeconds(0.5f);
                    }

                    // finally, destroy the object
                    if (DestroyParent)
                        Destroy(this.transform.parent.gameObject);
                    else
                        Destroy(this.gameObject);
                }
            }
        }
    }


    private void PlayerDie()
    {
        //Application.LoadLevel("youwon");
        Debug.Log("YOU DIED!");
        Time.timeScale = 0f;
    }
}