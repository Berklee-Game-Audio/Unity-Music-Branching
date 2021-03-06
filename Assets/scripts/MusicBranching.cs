﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBranching : MonoBehaviour {

	//public AudioSource[] audioSources;
	public AudioSource audiosourceA;
	public AudioSource audiosourceB;
	
	[System.Serializable]
	public struct musicBranch{
		public AudioClip theAudioClip;
		public float reverbTailTime;
	}
	
	//public myStruct newvariable;
	
	public musicBranch intro;
	public musicBranch ending;
	public musicBranch[] state1;
	public musicBranch[] state2;
	public musicBranch[] state3;
	
	public int whichState = 1;
	private int whichClip = 0;
	private bool altSource = false; 
	private float nextCheckTime = -1.0F;
	private bool isPlaying = true;
	private float tempReverbTailTime = 0.0f;
    private float currentClipLengthWithoutReverbTail = 0.0f;
    private bool hasStarted = false;
    private bool firstFrameDrawn = false;

    public GameObject myTimeline;
    public float timelineBeginningXPosition = 0.0f;
    public float timelineEndingXPosition = 0.0f;

    // Use this for initialization
    void Start () {
		//how long is the intro loop?
		

		Debug.Log("Start");
        //myTimeline.transform.position.x



    }
	
	// Update is called once per frame
	void Update () {
        if (!hasStarted && Time.realtimeSinceStartup > 4.0f)
        {
            audiosourceA.clip = intro.theAudioClip;
            audiosourceA.Play();

            nextCheckTime = Time.time + intro.theAudioClip.length - intro.reverbTailTime;
            currentClipLengthWithoutReverbTail = intro.theAudioClip.length - intro.reverbTailTime;

            //audioSources[0].clip = intro.theAudioClip;
            //audioSources[0].Play();

            altSource = true;
            hasStarted = true;
            firstFrameDrawn = true;

            myTimeline.transform.position = new Vector3(timelineBeginningXPosition, myTimeline.transform.position.y, myTimeline.transform.position.z);

        }

        if(firstFrameDrawn)
        {
            float timelinePercentage = (nextCheckTime - Time.time) / currentClipLengthWithoutReverbTail;
            //Debug.Log("update - timelinePercentage: " + timelinePercentage);

            float newTimelinePosition = timelineEndingXPosition - timelinePercentage * (timelineEndingXPosition - timelineBeginningXPosition);

            myTimeline.transform.position = new Vector3(newTimelinePosition, myTimeline.transform.position.y, myTimeline.transform.position.z);
        }
       

    }
	
	// Called more often
	void FixedUpdate () {
        //Debug.Log("fixed update = " + nextCheckTime + " " + Time.time);
        if (!firstFrameDrawn)
        {
            return;
        }

		if(Time.time >= nextCheckTime && nextCheckTime != -1.0f){
			nextCheckTime = -1.0f;
			PlayNext();			
		}
		
	}
	
	void PlayNext(){
		AudioClip nextClip = intro.theAudioClip;
		switch(whichState){
			case 1:
				//Debug.Log(whichClip);
				nextClip = state1[whichClip].theAudioClip;
				tempReverbTailTime = state1[whichClip].reverbTailTime;
				whichClip = whichClip + 1;
				if(whichClip >= state1.Length){
					whichClip = 0;
				}
				break;
			case 2:
				nextClip = state2[whichClip].theAudioClip;
				tempReverbTailTime = state2[whichClip].reverbTailTime;
				whichClip = whichClip + 1;
				if(whichClip >= state2.Length){
					whichClip = 0;
				}
				break;
			
			case 3:
				nextClip = state3[whichClip].theAudioClip;
				tempReverbTailTime = state3[whichClip].reverbTailTime;
				whichClip = whichClip + 1;
				if(whichClip >= state3.Length){
					whichClip = 0;
				}
				break;
			
			case 4:
				nextClip = ending.theAudioClip;
				break;
				
			default:
				
				break;
			
			
			
		}
		
		if(altSource){
			Debug.Log("Playing: " + nextClip.name);
			nextCheckTime = Time.time + nextClip.length - tempReverbTailTime;
            currentClipLengthWithoutReverbTail = nextClip.length - tempReverbTailTime;
            audiosourceB.clip = nextClip;
			audiosourceB.Play();
			altSource = false;
		} else {
			Debug.Log("Playing: " + nextClip.name);
			nextCheckTime = Time.time + nextClip.length - tempReverbTailTime;
            currentClipLengthWithoutReverbTail = nextClip.length - tempReverbTailTime;
            audiosourceA.clip = nextClip;
			audiosourceA.Play();
			altSource = true;
		
		}
		
		if(nextClip == ending.theAudioClip){
			nextCheckTime = -1;
			isPlaying = false;
		}
	
	}
	
	public void SwitchStates(int newStateRequest){
		whichState = newStateRequest;
		whichClip = 0;
		Debug.Log("New state request: " + newStateRequest);
		
	
	}
	
	void End(){
	
	}
	
	
}
