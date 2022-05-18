using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private float gridSpacing = 0.03f;
    private Material gridMat;

    private bool isClose = false;

    private Transform cam;
    private Transform Cam{
        get{
            if(cam == null) cam = Camera.main.transform;
            return cam;
        }
    }

    private Material GridMat{
        get{
            if(gridMat == null) gridMat = gameObject.GetComponent<Renderer>().material;
            return gridMat;
        }
    }

    private void Update() {
        if(getCamDist() < 0.5f != isClose){
            isClose = getCamDist() < 0.8f;
            setFrames(!isClose);
        }
    }

    public void setGridSize(float f){
        if(f < 0.01f) return;
        gridSpacing = f;
        GridMat.SetFloat("LargeSpacing",f);
    }

    public float getGridSpacing(){return gridSpacing;}

    private float getCamDist(){
        Vector3 pos1 = Cam.position;
        Vector3 pos2 = transform.InverseTransformPoint(pos1);
        pos2.z = 0;
        pos2 = transform.TransformPoint(pos2);
        return Vector3.Distance(pos1,pos2);
    }

    private void setFrames(bool state){
        // foreach(GameObject frame in GameObject.FindGameObjectsWithTag("Frame")){
        //     if(frame.layer == 0) frame.transform.GetChild(0).gameObject.SetActive(state);
        // }
    }
}
