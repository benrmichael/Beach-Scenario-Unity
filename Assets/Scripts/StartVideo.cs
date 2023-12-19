using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;
using UnityEngine.Windows.Speech;
using System.Text;

//class to handle user input to start the video and hide objects while they read instructions
public class StartVideo : MonoBehaviour
{
    //flag for if the user said "Start"
    private bool startSaid = false;

    //flag for if the user looked at the sphere to start the program
    public static bool gazeStart = false;

    //flag for if the video has been started
    private bool started = false;

    // flag for the button
    public static bool startedViaButton = false;

    //video player and ball that moves across screen
    private VideoPlayer video;
    public static GameObject ball;

    //voice recognition variables
    [SerializeField]
    private string[] keywords;
    private KeywordRecognizer keyword_recog;
    // Start is called before the first frame update
    void Start()
    {
        //find the ball that moves across the screen and disable it until the video is started
        ball = GameObject.Find("Sphere");
        ball.SetActive(false);

        //set up voice recognition to recognize the keyword "start"
        keywords = new string[1] {"Start"};
        keyword_recog = new KeywordRecognizer(keywords);
        keyword_recog.OnPhraseRecognized += OnPhraseRecognized;
        keyword_recog.Start();

        //stop the video so the user can start it via user input
        video = this.gameObject.GetComponent<VideoPlayer>();
        video.prepareCompleted += VideoPrepared;
        video.Prepare();
    }

    void VideoPrepared(VideoPlayer vp) 
    {
        vp.Pause();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //if the user said start, set the flag to start the video
        if (args.text == "Start")
        {
            startSaid = true;
        }
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //if the user decided to start the video either through voice command or looking at the object, begin the video
        if ((gazeStart || startSaid || startedViaButton) && !started)
        {
            beginVideo();
        }
    }

    void beginVideo()
    {
	DestroyObjects();


        //indicate that the video has been started, hide the instructions, and reveal the ball; start the video
        started = true;
        ball.SetActive(true);

        this.video.Play();
    }

	private void DestroyObjects()
	{
		GameObject introObject = GameObject.Find("IntroObjs");
        	if (introObject != null)
        	{
        	    DestroyImmediate(introObject);
        	}
	}
}
