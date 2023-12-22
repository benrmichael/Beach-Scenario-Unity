/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using System.IO;

//used to conceal the end screen score text and reveal it at the end of the video
public class RevealConceal : MonoBehaviour
{
    //flag to indicate if the video has ended and the text is revealed
    public static bool revealed = false;
    private bool alreadyLogged = false;
    //text to be displayed, set by GazeCheck
    public static string text;
    public static string logText;

    // Update is called once per frame
    void Update()
    {
        // Set text and reveal text at the end of the video
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.SetText(text);

        if (revealed && !alreadyLogged)
        {
            WriteToLog();
        }

        GameObject videoPlayerObj = GameObject.Find("Video Player");

        if (videoPlayerObj != null)
        {
            VideoPlayer video = videoPlayerObj.GetComponent<VideoPlayer>();
            if (video != null)
            {
                video.loopPointReached += Reveal;
            }
            else
            {
                Debug.LogError("VideoPlayer component not found on the 'Video Player' GameObject.");
            }
        }
        else {
            Debug.LogError("GameObject 'Video Player' not found in the scene.");
        }
        //VideoPlayer video = videoPlayerObj.GetComponent<VideoPlayer>();
        //video.loopPointReached += Reveal;
    }

    //reveal text and hide application objects
    void Reveal(UnityEngine.Video.VideoPlayer vp)
    {
        revealed = true;
    }

    void WriteToLog()
    {
        string filename = Application.dataPath + "/DataCollection.txt";
        try
        {
            TextWriter writer = new StreamWriter(filename, true);
            writer.WriteLine(logText);
            writer.Close();
        }
        catch (System.Exception e) {
            Debug.Log("Error writing to the file! " + e.StackTrace);
        }
        alreadyLogged = true;
    }
}
*/