using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to make objects move in an oscilating linear movement
// - goes from left to right...
public class MovingObject : MonoBehaviour
{
    // The rate at which the sphere moves across the screen
    float change = 0.02f;
    UnityEngine.Video.VideoPlayer video;

    // Update is called once per frame
    void Update()
    {
        // Gets x, y, z coordinates for object ==> even though we only use the x??
        float x = this.gameObject.transform.position.x;
        float y = this.gameObject.transform.position.y;
        float z = this.gameObject.transform.position.z;

        // Check if the object hits the bounds of -45 or 45, then reverse direction
        if ((x - 45f) >= 0 || (x + 45f) <= 0)
        {
            change = -1 * change;
        }
        
        // Move the spehere
        this.gameObject.transform.position += new Vector3(change, 0);
    }
}
