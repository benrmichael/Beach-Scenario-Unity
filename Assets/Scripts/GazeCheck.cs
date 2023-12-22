using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using UnityEngine.Localization.Settings;
using UnityEngine.Video;

//records time that the user spends looking at object, displays it at the end of the application
public class GazeCheck : MonoBehaviour, IGazeFocusable
{
    public delegate void DataCollectionEventHandler(string eyeTrackingData);
    public static event DataCollectionEventHandler OnDataCollectionComplete;
    
    private bool prevGazeFlag = false;
   
    private int countLoseFocus = 0;
    private double timeLookedAt = 0;
    private double totalTime = 0;
    private const double totalDuration = 375.00;

    [SerializeField] public Renderer sphereRenderer;
    [SerializeField] public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        sphereRenderer.material.SetColor("_Color", Color.red);
        videoPlayer.loopPointReached += VideoOver;
    }

    // Method called when the video has ended. Invoke the event listener in ScenarioLifeCycleManager
    private void VideoOver(VideoPlayer vp)
    { 
        // Calculates the percent time spent tracking the object
        double averageTime = totalTime / totalDuration;

        string firstPart = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("dataPartOne").Result;
        string secondPart = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("dataPartTwo").Result;

        string eyeTrackingData = string.Format(firstPart + " {0:P2}.", averageTime) + "\n" + secondPart + countLoseFocus.ToString() + "\n";
        //string eyeTrackingData = string.Format("Percentage of time looking at object: {0:P2}.", averageTime) + "\nTimes eye contact was broken: " + countLoseFocus.ToString() + ".\n";

        OnDataCollectionComplete?.Invoke(eyeTrackingData);
    }


    // Method used for eye tracking data collection
    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus) //if the user is looking at the object, set color to green and record time the object was looked at
        {
            timeLookedAt = videoPlayer.time;
            sphereRenderer.material.SetColor("_Color", Color.green);
        }
        else //if user is no longer looking at object set color to red
        {
            sphereRenderer.material.SetColor("_Color", Color.red);
        }

        if (!hasFocus && prevGazeFlag && timeLookedAt != 0) 
        {
            //if the user is no longer looking at the object and there was a recorded time that they looked at the object, then add the time spent looking at the object to the total
            totalTime += videoPlayer.time - timeLookedAt;
        }
        if (!hasFocus && prevGazeFlag)
        {
            //if they were looking at the object and looked away, increment statistic
            countLoseFocus++;
        }
        prevGazeFlag = hasFocus;
    }
}
