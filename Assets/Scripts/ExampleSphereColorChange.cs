using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class ExampleSphereColorChange : MonoBehaviour, IGazeFocusable
{
    private Renderer SphereRenderer;
    private void Start()
    {
        SphereRenderer = this.gameObject.GetComponent<Renderer>();
        SphereRenderer.material.SetColor("_Color", Color.red);
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            SphereRenderer.material.SetColor("_Color", Color.green);
        }
	    else
	    {
	        SphereRenderer.material.SetColor("_Color", Color.red);   
	    }
    }
}
