using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonInteraction : MonoBehaviour {

	private string classTag="BallonInteraction";

	public GameObject redBalloon, blueBalloon, greenBalloon, previewCube;
	public GameObject redLabel, greenLabel, blueLabel;
	private int redSize, blueSize, greenSize;
	private Balloon selectedBalloon;
	private MicState micState;
	bool startMic = false, stopMic = false;

	public MicrophoneInput micI;
	AudioVisualizer1 viz;

	enum MicState{
		INIT,
		UPDATED,
		STOPPED
	}

	public enum Balloon{
		RED,
		GREEN,
		BLUE,
		NONE
	}

	// Use this for initialization
	void Start () {
		redBalloon = GameObject.Find("RedBalloonWrapper");
		blueBalloon = GameObject.Find("BlueBalloonWrapper");
		greenBalloon = GameObject.Find("GreenBalloonWrapper");
		previewCube = GameObject.Find("previewCube");
		redLabel = GameObject.Find ("RedcolorVal");
		greenLabel = GameObject.Find ("GreenColorVal");
		blueLabel = GameObject.Find ("BlueColorVal");
		selectedBalloon =  Balloon.NONE;
		viz = micI.GetComponent<AudioVisualizer1> ();
		Debug.Log("viz is null "+ (viz == null));

		redSize = blueSize = greenSize = (int) ((redBalloon.transform.localScale.x - 0.5) * 255);
		previewCube.GetComponent<Renderer>().material.color = new Color((float)(redSize/255f), (float)(greenSize/255f), (float)(blueSize/255f));
		updateLabels ();
		micI.InitMircophone ();
		micState = MicState.INIT;
		this.startMicIfNeeded();
	}

	void updateLabels(){
		greenLabel.GetComponentInChildren<UnityEngine.UI.Text> ().text = greenSize.ToString();
		redLabel.GetComponentInChildren<UnityEngine.UI.Text> ().text = redSize.ToString();
		blueLabel.GetComponentInChildren<UnityEngine.UI.Text> ().text = blueSize.ToString();
	}
	
	// Update is called once per frame
	void Update () {

		startMicIfNeeded();

		if (micState == MicState.UPDATED && selectedBalloon != Balloon.NONE) {

			Debug.Log ("inflateBallon: "+ viz.getInflateBallon());
			if (viz.getInflateBallon()) {
				Debug.Log ("inflateBalloon");
				inflateBalloon (selectedBalloon, 2);
			}

			if (viz.getDeflateBallon()) {
				Debug.Log ("inflateBalloon");
				deflateBalloon (selectedBalloon, 10);
			}

			updateLabels ();
		}		
	}

	public void deflateBalloon(Balloon selBallon, int step){
		float balloonSize = 0f;
		int min = 0 + step;

		switch (selBallon) {
		case Balloon.RED:
			if (redSize >= min) {
				redSize = redSize - step;
				balloonSize = 0.5f + ((float)redSize / 255f);
				redBalloon.transform.localScale = new Vector3 (balloonSize, balloonSize, balloonSize);
			}
			break;
		case Balloon.GREEN:
			if (greenSize >= min) {
				greenSize = greenSize - step;
				balloonSize = 0.5f + ((float)greenSize / 255f);
				greenBalloon.transform.localScale = new Vector3 (balloonSize, balloonSize, balloonSize);
			}
			break;
		case Balloon.BLUE:
			if (blueSize >= min) {
				blueSize = blueSize - step;
				balloonSize = 0.5f + ((float)blueSize / 255f);
				blueBalloon.transform.localScale = new Vector3 (balloonSize, balloonSize, balloonSize);
			}
			break;
		}

		previewCube.GetComponent<Renderer>().material.color = new Color((float)(redSize/255f), (float)(greenSize/255f), (float)(blueSize/255f));
	}

		
	public void inflateBalloon(Balloon selBallon, int step){
		float balloonSize = 0f;
		int max = 255 - step;


		switch (selBallon) {
		case Balloon.RED:
			if (redSize <= max) {
				redSize = redSize + step;
				balloonSize = 0.5f + ((float)redSize / 255f);
				redBalloon.transform.localScale = new Vector3 (balloonSize, balloonSize, balloonSize);
			}
			break;
		case Balloon.GREEN:
			if (greenSize <= max) {
				greenSize = greenSize + step;
				balloonSize = 0.5f + ((float)greenSize / 255f);
				greenBalloon.transform.localScale = new Vector3 (balloonSize, balloonSize, balloonSize);
			}
			break;
		case Balloon.BLUE:
			if (blueSize <= max) {
				blueSize = blueSize + step;
				balloonSize = 0.5f + ((float)blueSize / 255f);
				blueBalloon.transform.localScale = new Vector3 (balloonSize, balloonSize, balloonSize);
			}
			break;
		}

		previewCube.GetComponent<Renderer>().material.color = new Color((float)(redSize/255f), (float)(greenSize/255f), (float)(blueSize/255f));
	}

	public void changeRedBalloon(){
		Debug.Log (classTag + " changeRedBalloon");
		selectedBalloon = Balloon.RED;
		startMic = true;
	}

	public void changeBlueBalloon(){
		Debug.Log (classTag + " changeBlueBalloon");
		selectedBalloon = Balloon.BLUE;
		startMic = true;
	}

	public void changeGreenBalloon(){
		Debug.Log (classTag + " changeGreenBalloon");
		selectedBalloon = Balloon.GREEN;
		startMic = true;
	}

	public void deselectBalloon(){
		Debug.Log (classTag + " deslectColor");
		selectedBalloon = Balloon.NONE;

	}

	public void startMicIfNeeded()
	{
		if (startMic & (micState != MicState.UPDATED)) {
			Debug.Log ("in start update micro");
			micI.StartMicrophone ();
			micState = MicState.UPDATED;
			startMic = false;
		}
	}
}
