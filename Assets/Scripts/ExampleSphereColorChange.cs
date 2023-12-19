using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSphereColorChange : MonoBehaviour
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
