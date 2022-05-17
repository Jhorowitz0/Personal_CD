using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignLibraryManager : MonoBehaviour
{
    private Transform Design;

    // Start is called before the first frame update
    public void onPress(){
        if(Design == null)getFrames();
        loadDesign();
    }

    private void getFrames(){
        Design = new GameObject().transform;
        Design.name = transform.name;
        foreach(GameObject frame in GameObject.FindGameObjectsWithTag("Frame")){
            if(frame.activeSelf){
                frame.transform.parent = Design;
                frame.transform.GetChild(0).GetComponent<FrameBehaviour>().show(false);
            }
        }
    }

    private void loadDesign(){
        foreach(GameObject frame in GameObject.FindGameObjectsWithTag("Frame")){
            if(frame.activeSelf)frame.transform.GetChild(0).GetComponent<FrameBehaviour>().destroyFrame();
        }
        foreach(Transform child in Design){
            child.GetChild(0).GetComponent<FrameBehaviour>().InstantiateFrame();
        }
    }

    private void generatePreview(){

    }

    // foreach(var go in GameObject.FindObjectsWithTag("Spawn"))
    // {
    //     t.Add(go.transform);
    // }
}
