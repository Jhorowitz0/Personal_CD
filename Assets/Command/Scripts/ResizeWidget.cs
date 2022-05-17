using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResizeWidget : MonoBehaviour
{
    public GameObject outerRing;
    public GameObject innerRing;

    public LineRenderer line;

    public GridManager grid;

    public TextMeshProUGUI text;

    private bool isMoving;
    private bool isMetric = false;

    private void OnEnable() {
        outerRing.transform.localPosition = new Vector3(0,0,0);
    }

    private void Update() {
        line.SetPosition(1,innerRing.transform.position);
        line.SetPosition(0,transform.position);
        if(isMoving){
            grid.setGridSize(getDistance());
            text.transform.position = (transform.position + outerRing.transform.position)/2;
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

    public void grabState(bool state){
        isMoving = state;
        text.gameObject.SetActive(state);
        float scale = 0.75f;
        if(state)scale = 0.05f;
        LeanTween.scale(innerRing,new Vector3(scale,scale,scale),0.2f).setEase(LeanTweenType.easeInQuad);
        if(!state){
            LeanTween.moveLocal(outerRing,new Vector3(0,0,0),0.3f).setEase(LeanTweenType.easeSpring);
        }
    }

    private float getDistance(){
        float dist = Vector3.Distance(transform.position, outerRing.transform.position);
        float scale = 100f;
        if(!isMetric) scale = 39.3701f;
        dist = Mathf.Round(dist * scale) / scale;
        return dist;
    }

    private string getUnits(){
        float dist = Vector3.Distance(transform.position, outerRing.transform.position);
        float scale = 100f;
        if(!isMetric)scale = 39.3701f;
        dist = Mathf.Round(dist*scale);
        if(isMetric) return dist + "cm";
        else return dist + "in";
    }
}
