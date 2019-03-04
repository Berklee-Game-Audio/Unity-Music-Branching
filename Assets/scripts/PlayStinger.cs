using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class PlayStinger : MonoBehaviour {

    bool myToggle = false;
    // Use this for initialization
	void Start () {
        myToggle = true;
        //Debug.Log("myToggle = " + myToggle);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(){
        //Debug.Log("myToggle = " + myToggle);
        if(myToggle == false)
        {
            return;
        }

        AudioSource myAudioSource = this.GetComponent<AudioSource>();
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.Play();
        }



    }
}
