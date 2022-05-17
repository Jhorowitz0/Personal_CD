using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour
{
    private Transform cam;
    private Transform Cam{
        get{
            if(cam == null) cam = Camera.main.transform;
            return cam;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation((transform.position - Cam.position).normalized);

    }
}
