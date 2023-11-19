using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    [SerializeField] public bool buttonPressed = false;
    private bool alreadyStarted = false;

    private void Update()
    {
        if (buttonPressed && !alreadyStarted)
        {
            StartBeachVideo();
            alreadyStarted = true;
        }
    }
    public void StartBeachVideo()
    {
        StartVideo.startedViaButton = true;
    }
}
