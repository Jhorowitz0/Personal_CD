using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using TMPro;

public class FrameSpawner : MonoBehaviour
{
    Vector3 startPos;
    public Vector2 size = new Vector2(1,1);
    public GameObject framePrefab;
    private GameObject frame;

    public TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }

    void initialize(){
        startPos = transform.parent.localPosition;
        if(size.x > size.y) transform.localScale = new Vector3(1f,size.y/size.x,1f);
        else transform.localScale = new Vector3(size.x/size.y,1f,1f);
        Vector2 units = size;
        units = new Vector2(Mathf.Round(units.x / 0.5f)*0.5f,Mathf.Round(units.y / 0.5f)*0.5f); //convert to nearest 0.5in
        text.text = units.x + "in x " + units.y + "in";
    }

    public void onGrab(){
        text.gameObject.SetActive(false);
    }

    public void onRelease(){
        transform.parent.localPosition = startPos;
        gameObject.GetComponent<Renderer>().enabled = true;
        if(frame != null){
            frame.GetComponent<TapToPlace>().StopPlacement();
            frame.transform.GetChild(0).gameObject.GetComponent<FrameBehaviour>().UpdateIsMoving(false);
            frame = null;
        }
        text.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "MainMenu" && frame == null){
            gameObject.GetComponent<Renderer>().enabled = false;
            spawnFrame();
        }
    }

    private void spawnFrame(){
        frame = GameObject.Instantiate<GameObject>(framePrefab);
        frame.transform.GetChild(0).localScale = new Vector3(size.x * 0.0254f,size.y * 0.0254f,0.011f);
        frame.transform.position = transform.position;
        frame.GetComponent<TapToPlace>().StartPlacement();
    }
}
