using System.Collections;
using System.Collections.Generic;
//using Boo.Lang;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject redBalloon, blueBalloon, greenBalloon, cube;
	// Use this for initialization
    private int redSize, blueSize, greenSize;
	void Start ()
	{
	    redBalloon = GameObject.Find("RedBalloonSphere");
        blueBalloon = GameObject.Find("BlueBalloonSphere");
        greenBalloon = GameObject.Find("GreenBalloonSphere");
        cube = GameObject.Find("previewCube");
	    redSize = blueSize = greenSize = (int) ((redBalloon.transform.localScale.x - 0.5) * 255);
        cube.GetComponent<Renderer>().material.color = new Color((float)(redSize/255f), (float)(greenSize/255f), (float)(blueSize/255f));
        print("INIT");
    }


	// Update is called once per frame
	void Update () {
	    if (Input.GetKey("q") && redSize >= 0)
	    {
	    	print("Q DOWN");
	        redSize = redSize - 1;
	        float balloonSize = 0.5f + ((float) redSize / 255f);
	        redBalloon.transform.localScale = new Vector3(balloonSize, balloonSize, balloonSize);
	    }
        else if (Input.GetKey("w") && redSize <= 255)
        {
            redSize = redSize + 1;
            float balloonSize = 0.5f + ((float)redSize / 255f);
            redBalloon.transform.localScale = new Vector3(balloonSize, balloonSize, balloonSize);
        }
        else if (Input.GetKey("a") && greenSize >= 0)
        {
            greenSize = greenSize - 1;
            float balloonSize = 0.5f + ((float)greenSize / 255f);
            greenBalloon.transform.localScale = new Vector3(balloonSize, balloonSize, balloonSize);
        }
        else if (Input.GetKey("s") && greenSize <= 255)
        {
            greenSize = greenSize + 1;
            float balloonSize = 0.5f + ((float)greenSize / 255f);
            greenBalloon.transform.localScale = new Vector3(balloonSize, balloonSize, balloonSize);
        }
        else if (Input.GetKey("z") && blueSize >= 0)
        {
            blueSize = blueSize - 1;
            float balloonSize = 0.5f + ((float)blueSize / 255f);
            blueBalloon.transform.localScale = new Vector3(balloonSize, balloonSize, balloonSize);
        }
        else if (Input.GetKey("x") && blueSize <= 255)
        {
            blueSize = blueSize + 1;
            float balloonSize = 0.5f + ((float)blueSize / 255f);
            blueBalloon.transform.localScale = new Vector3(balloonSize, balloonSize, balloonSize);
        }

        cube.GetComponent<Renderer>().material.color = new Color((float)(redSize/255f), (float)(greenSize/255f), (float)(blueSize/255f));
    }
}
