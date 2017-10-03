using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System;


public class AudioVisualizer1 : MonoBehaviour {
		
	public Transform[] audioSpectrumObjects;
	public float secWriteToFile = 0.0f;
	private bool inflate;
	private bool deflate;

	[Range(1, 100)] private float heightMultiplier;
	[Range(64, 8192)] private int numberOfSamples; 

	private FFTWindow fftWindow;
	private float lerpTime = 1;
	private string fftout="";

	private float fMax = 0f;
	private int countBlasen=0;

	private bool debug = false;
	private bool writefile = true;

	private string locTag = "none";

	/*
	 * The intensity of the frequencies found between 0 and 44100 will be
	 * grouped into 1024 elements. So each element will contain a range of about 43.06 Hz.
	 * The average human voice spans from about 60 hz to 9k Hz
	 * we need a way to assign a range to each object that gets animated. that would be the best way to control and modify animatoins.
	*/

	void Start(){

		heightMultiplier = 100; //for visualization
		numberOfSamples = 1024; //step by 2, for analysis & visualisation
		fMax = AudioSettings.outputSampleRate/2; //for analysis
		inflate=false;
	}

	public bool getInflateBallon(){
		return inflate;
	}

	public bool getDeflateBallon(){
		return deflate;
	}

	public float BandVol(float[] freqData, float fLow, float fHigh){
		fLow =  Mathf.Clamp(fLow, 20, fMax); // limit low...
		fHigh = Mathf.Clamp(fHigh, fLow, fMax); // and high frequencies

		int n1 = (int) Mathf.Floor(fLow * numberOfSamples / fMax); // = 0, =128, ...
		int n2 = (int) Mathf.Floor(fHigh * numberOfSamples / fMax); // =128, =256, ...
		float sum = 0;
		// average the volumes of frequencies fLow to fHigh
		for (var i = n1; i <= n2; i++){
			sum += freqData[i];
		}

		float x = (sum / (n2 - n1 + 1)) * 1000;
		return Mathf.Clamp(x, 0.1f, 2f);
	}

	public void triggerBlasen(bool blasen){

		//false = no blowing (Blasen :)
		if (!blasen & countBlasen == 0) {
			inflate = false;
			return;
		} else if (!blasen & countBlasen > 0) {
			countBlasen--;
			inflate = false;
			return;
		} else if (blasen) {
			if (countBlasen > 5) {//Blowing is detected, but also clapping with the hands.
				Debug.Log (Time.time.ToString ("F2") + "------------------Blasen: " + countBlasen);
				inflate = true;
			}
			if (countBlasen < 10) {
				countBlasen++;
			}
		}
	}

	public void triggerKiss(bool kiss){
		Debug.Log (Time.time.ToString ("F2") + "------------------Kiss: ");
		if (kiss) {
			deflate = true;
		} else {
			deflate = false;
		}
	}
		
	void Update() {

		// initialize our float array
		float[] spectrum = new float[numberOfSamples];

		// populate array with fequency spectrum data
		GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

		float band1, band2, band3, band4, band5, band6, band7;
		band1 = BandVol (spectrum, 0, 3000); //compute volume, frequency band from 0 to 3000 Hz
		band2 = BandVol (spectrum, 3001, 6000);
		band3 = BandVol (spectrum, 6001, 9000);
		band4 = BandVol (spectrum, 9001, 12000);
		band5 = BandVol (spectrum, 12001, 14000);
		band6 = BandVol (spectrum, 14001, 17000);
		band7 = BandVol (spectrum, 17001, 20000);

		//assumption: blowing results in high volume in all bands (more/less)
		if (band4 != 0.1f & band1 >= 0.5f) {
			triggerBlasen (true);
		} else if (band4 == 0.1f & band1 > 0.5f & band2 != 0.1f & band3 != 0.1f) {
			triggerBlasen (true);
		} else {
			triggerBlasen (false);
		}

		if (band7 != 0.1f) {
			triggerKiss (true);
		} else if (deflate && band7 == 0.1f) {
			triggerKiss (false);
		}

		if (debug){
			string output = Time.time.ToString("F2") + ", " + band1.ToString("F5") + ", " + band2.ToString("F5") + ", " + band3.ToString("F5") + ", " + band4.ToString("F5");
			fftout = fftout + " " + locTag + ", " + output + "\n";
			
			if (Time.time > secWriteToFile & writefile) {
					writefile = false;
					Debug.Log (Time.time+"---------------------------------------writeFile");
					System.IO.File.WriteAllText("C:\\Users\\stadlers\\Documents\\vrnd-curproj\\test\\unity.txt", fftout);
			}
		}
			

		// loop over audioSpectrumObjects and modify according to fequency spectrum data
		// this loop matches the Array element to an object on a One-to-One basis.
		// This is for visualization.
		for(int i = 0; i < audioSpectrumObjects.Length; i++)
		{			
			// apply height multiplier to intensity
			float intensity = spectrum[i] * heightMultiplier;

			// calculate object's scale
			float lerpY = Mathf.Lerp(audioSpectrumObjects[i].localScale.y,intensity,lerpTime);
			Vector3 newScale = new Vector3( audioSpectrumObjects[i].localScale.x, lerpY, audioSpectrumObjects[i].localScale.z);

			// appply new scale to object
			audioSpectrumObjects[i].localScale = newScale;
		}
	}
}
