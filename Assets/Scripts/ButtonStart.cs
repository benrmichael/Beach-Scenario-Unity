using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    
    private bool alreadyStarted = false;  

    public void StartVideoPlayer()
    {
        if(alreadyStarted) return;
	
	StartVideo.startedViaButton = true;
	alreadyStarted = true;
    }
}
