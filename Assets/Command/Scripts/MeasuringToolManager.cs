using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeasuringToolManager : MonoBehaviour
{
    public GameObject outerRing;
    public GameObject innerRing;

    public GameObject outerRing2;
    public GameObject innerRing2;

    public LineRenderer line;

    public TextMeshProUGUI text;
    public MeasuringToolManager tool2;

    private bool isMoving;
    private bool isMetric = false;

    private void Update() {
        if(isMoving){
            line.SetPosition(1,innerRing.transform.position);
            line.SetPosition(0,outerRing2.transform.position); 
            text.transform.position = (outerRing2.transform.position + outerRing.transform.position)/2;
            text.text = getUnits();
        }
    }
    public void hoverState(bool state){
        float scale = 1;
        if(state)scale = 1.5f;
        LeanTween.scale(outerRing,new Vector3(scale,scale,scale),0.2f).setEase(LeanTweenType.easeInQuad);
        scale = 0.5f;
        if(state) scale = 0.75f;
        LeanTween.scale(innerRing,new Vector3(scale,scale,scale),0.2f).setEase(LeanTweenType.easeInQuad);
    }

    public void hoverState2(bool state){
        float scale = 1;
        if(state)scale = 1.5f;
        LeanTween.scale(outerRing2,new Vector3(scale,scale,scale),0.2f).setEase(LeanTweenType.easeInQuad);
        scale = 0.5f;
        if(state) scale = 0.75f;
        LeanTween.scale(innerRing2,new Vector3(scale,scale,scale),0.2f).setEase(LeanTweenType.easeInQuad);
    }

    public void grabState2(bool state){
        isMoving = state;
        float scale = 0.75f;
        if(state)scale = 0.05f;
        LeanTween.scale(innerRing2,new Vector3(scale,scale,scale),0.2f).setEase(LeanTweenType.easeInQuad);
    }

    public void grabState(bool state){
        if(transform.parent != null){
            outerRing2.gameObject.SetActive(true);
            LeanTween.scale(outerRing2,new Vector3(1,1,1),0.2f).setEase(LeanTweenType.easeSpring);
            tool2.reset(transform.parent);
            transform.parent = null;
            text.gameObject.SetActive(true);
            line.gameObject.SetActive(true);
        }
        isMoving = state;
        float scale = 0.75f;
        if(state)scale = 0.05f;
        LeanTween.scale(innerRing,new Vector3(scale,scale,scale),0.2f).setEase(LeanTweenType.easeInQuad);
    }

    private float getDistance(){
        float dist = Vector3.Distance(outerRing2.transform.position, outerRing.transform.position);
        float scale = 100f;
        if(!isMetric) scale = 39.3701f;
        dist = Mathf.Round(dist * scale) / scale;
        return dist;
    }

    private string getUnits(){
        float dist = Vector3.Distance(outerRing2.transform.position, outerRing.transform.position);
        float scale = 100f;
        if(!isMetric)scale = 39.3701f;
        dist = Mathf.Round(dist*scale);
        if(isMetric) return dist + "cm";
        else return dist + "in";
    }

    private void reset(Transform parent){
        gameObject.SetActive(false);
        transform.parent = parent;
        transform.localPosition = new Vector3(0,-0.05f,0);
        outerRing.transform.localPosition = new Vector3(0,0,0);
        outerRing2.transform.localPosition = new Vector3(0,0,0);
        outerRing2.transform.localScale = new Vector3(0,0,0);
        outerRing2.SetActive(false);
        text.gameObject.SetActive(false);
        line.gameObject.SetActive(false);
    }
}
