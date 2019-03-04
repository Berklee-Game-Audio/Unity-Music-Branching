using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStateSwitch : MonoBehaviour {

	

	public int whichState = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void OnTriggerEnter(){
		MusicBranching musicGameObject = GameObject.FindObjectOfType(typeof(MusicBranching)) as MusicBranching;
		musicGameObject.SwitchStates(whichState);

		//AudioSource audio = GetComponent<AudioSource>();
		//if(audio != null){
		//	audio.Play();
	//}
        
        //audio.Play(44100);

	}
}
