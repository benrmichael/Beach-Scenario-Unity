using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

//used to create objects that can be looked at to start the application
public class StartWithGaze : MonoBehaviour, IGazeFocusable
{
    private Renderer SphereRenderer;
    private void Start()
    {
        //object is red until the user looks at it
        SphereRenderer = this.gameObject.GetComponent<Renderer>();
        SphereRenderer.material.SetColor("_Color", Color.red);
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        //if the user looks at the object, change color to green and set the flag in StartVideo to begin the video; delete this object afterwards
        if (hasFocus)
        {
            SphereRenderer.material.SetColor("_Color", Color.green);
        }
        StartVideo.gazeStart = hasFocus;
        Destroy(this.gameObject);
    }
}
