using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using UnityEngine.Video;

//records time that the user spends looking at object, displays it at the end of the application
public class GazeCheck : MonoBehaviour, IGazeFocusable
{
    //variables
    private bool prevGazeFlag = false;
    //num times user stopped looking at the object
    private int countLoseFocus = 0;
    private VideoPlayer video;
    private double timeLookedAt = 0;
    //total time spent looking at object
    private double totalTime = 0;
    //total duration of video (375 seconds/6 minutes and 15 seconds)
    private const double totalDuration = 375.00;
    //used to change the color of the object
    private Renderer SphereRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //set color to red and set score once the video is over
        SphereRenderer = this.gameObject.GetComponent<Renderer>();
        SphereRenderer.material.SetColor("_Color", Color.red);
        video = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        video.loopPointReached += setScore;
    }

    private void setScore(UnityEngine.Video.VideoPlayer vp)
    {

        Debug.Log("Entered the setScore function.");

        //calculate % of time spent looking at object and display it in the scene
        double averageTime = totalTime / totalDuration;

        string eyeTrackingData = string.Format("Percentage of time looking at object: {0:P2}.", averageTime) + "\nTimes eye contact was broken: " + countLoseFocus.ToString() + ".\n";

        RevealConceal.logText = "[" + System.DateTime.Now + "]" + " " + eyeTrackingData;
        RevealConceal.text = eyeTrackingData;

        Debug.Log("Time Looked At: " + averageTime.ToString());
        Debug.Log("Gaze Break Count: " + countLoseFocus.ToString());
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus) //if the user is looking at the object, set color to green and record time the object was looked at
        {
            timeLookedAt = video.time;
            SphereRenderer.material.SetColor("_Color", Color.green);
        }
        else //if user is no longer looking at object set color to red
        {
            SphereRenderer.material.SetColor("_Color", Color.red);
        }

        if (!hasFocus && prevGazeFlag && timeLookedAt != 0) 
        {
            //if the user is no longer looking at the object and there was a recorded time that they looked at the object, then add the time spent looking at the object to the total
            totalTime += video.time - timeLookedAt;
        }
        if (!hasFocus && prevGazeFlag)
        {
            //if they were looking at the object and looked away, increment statistic
            countLoseFocus++;
        }
        prevGazeFlag = hasFocus;
    }
}
