using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;
using UnityEngine.Windows.Speech;

public class ScenarioLifeCycleManager : MonoBehaviour
{
    // Objects to be managed
    [SerializeField] public TextMeshProUGUI text;
    [SerializeField] public VideoPlayer video;
    [SerializeField] public Material    beginningSkyBox;
    [SerializeField] public Material    videoSkyBox;
    [SerializeField] public GameObject  instructions;
    [SerializeField] public GameObject  introParent;
    [SerializeField] public GameObject  pedestal;
    [SerializeField] public GameObject  beginningSphere;
    [SerializeField] public GameObject  startSphere;
    [SerializeField] public GameObject  trackedSphere;

    private bool videoStarted = false;
    public static bool startedViaSphere = false;
    const bool INVISIBLE = false;
    const bool VISIBLE = true;

    private static string endOfTrialText;

    // Set up the keyword recognizer, prep the video, and set the eye gaze sphere to inactive
    private void Start()
    {
        trackedSphere.SetActive(false);

        string[] keywords = new string[1] { "Start" };
        KeywordRecognizer keywordRecognizer = new KeywordRecognizer(keywords);
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();

        video.prepareCompleted += VideoPrepared;
        video.Prepare();

        // Subscribe to data collection event
        GazeCheck.OnDataCollectionComplete += ProcessResults;
    }

    private void Update()
    {
        if (startedViaSphere)
        {
            StartVideoRoutine();
        }
    }

    // Event listener for the phrase recognition
    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //if the user said start, set the flag to start the video
        if (args.text == "Start")
        {
            StartVideoRoutine();
        }
    }

    // Method called to pause the video
    void VideoPrepared(VideoPlayer vp)
    {
        vp.Pause();
    }

    // Method to be called when beginning the video
    public void StartVideoRoutine()
    {
        if (videoStarted) return;

        videoStarted = true;
        SetVisibility(INVISIBLE);
        RenderSettings.skybox = videoSkyBox;
        instructions.SetActive(false);
        trackedSphere.SetActive(true);

        Destroy(startSphere);
        Destroy(pedestal);
        Destroy(beginningSphere);

        video.Play();
        video.loopPointReached += EndVideoRoutine;
    }

    // Method to be called when the video is done
    public void EndVideoRoutine(VideoPlayer video)
    {
        SetVisibility(VISIBLE);
        Destroy(trackedSphere);
        RenderSettings.skybox = beginningSkyBox;
        instructions.SetActive(true);
    }

    // Set the visibility of the introduction objects
    void SetVisibility(bool isVisible)
    {
        if (introParent == null)
        {
            Debug.Log("WARNING : intro parent not assigned!");
            return;
        }

        foreach(Renderer renderer in introParent.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = isVisible;
        }
    }

    // Event listener that is subscribed to GazeCheck.VideoOver
    private void ProcessResults(string eyeTrackingData)
    {
        string results = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("results").Result;
        string ending = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("ending").Result;

        text.SetText(results + "\n\n" + eyeTrackingData + "\n" + ending);
        WriteToLog("[" + System.DateTime.Now + "]" + " " + eyeTrackingData);
    }

    // Method used to write out the eye tracking data to the log file
    void WriteToLog(string logText)
    {
        string filename = Application.dataPath + "/DataCollection.txt";
        try
        {
            TextWriter writer = new StreamWriter(filename, true);
            writer.WriteLine(logText);
            writer.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log("Error writing to the file! " + e.StackTrace);
        }
    }
  
    public void OnDestroy()
    {
        GazeCheck.OnDataCollectionComplete -= ProcessResults;
    }
}
