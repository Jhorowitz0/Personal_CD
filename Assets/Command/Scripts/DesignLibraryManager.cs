using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignLibraryManager : MonoBehaviour
{
    private Transform Design;
    public Transform Canvas;

    public GameObject preview;

    public GameObject text;

    public int layer;

    // Start is called before the first frame update
    public void onPress(){
        if(Design == null)getFrames();
        generatePreview();
        loadDesign();
    }

    private void getFrames(){
        Design = new GameObject().transform;
        Design.name = transform.name;
        foreach(GameObject frame in GameObject.FindGameObjectsWithTag("Frame")){
            if(frame.gameObject.layer < 8){
                frame.transform.parent = Design;
                frame.gameObject.layer = 8;
                frame.transform.GetChild(0).GetComponent<FrameBehaviour>().show(false,layer);
            }
        }
        text.SetActive(false);
    }

    private void loadDesign(){
        foreach(GameObject frame in GameObject.FindGameObjectsWithTag("Frame")){
            if(frame.gameObject.layer < 8)frame.transform.GetChild(0).GetComponent<FrameBehaviour>().destroyFrame();
        }
        foreach(Transform child in Design){
            child.GetChild(0).GetComponent<FrameBehaviour>().InstantiateFrame();
        }
    }

    private void generatePreview(){
        preview.SetActive(true);
    }

    // foreach(var go in GameObject.FindObjectsWithTag("Spawn"))
    // {
    //     t.Add(go.transform);
    // }
}
