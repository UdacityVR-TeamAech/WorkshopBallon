using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class analyzeSound : MonoBehaviour {
	private string Tag = "analyzeSound";

	//A boolean that flags whether there's a connected microphone  
	private bool micConnected = false;

	public GameObject btnstop, btnrec;
	private AudioSource aud;

	// Use this for initialization
	void Start () {
		aud = this.GetComponent<AudioSource>();

		//Check if there is at least one microphone connected  
		if(Microphone.devices.Length <= 0)  
		{  
			//Throw a warning message at the console if there isn't  
			Debug.Log("Microphone not connected!");  
		}  
		else //At least one microphone is present  
		{  
			micConnected = true;
			Debug.Log ("Microphone present");
			aud = this.GetComponent<AudioSource>();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startRec(){
		Debug.Log (Tag +" startRec");
		//aud.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
		//aud.Play();
	}

	public void stopRec(){
		Debug.Log (Tag +" stopRec");
	}
}
