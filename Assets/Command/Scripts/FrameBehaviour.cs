using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameBehaviour : MonoBehaviour
{
    private Transform canvas = null;
    bool isOnGrid = false; //is the frame ready to be deleted?
    bool isMoving = false; //is the frame being moved right now
    
    private GameObject displayFrame;

    public Color defaultColor;
    public Color selectedColor;

    public Color deleteColor;

    private Material displayFrameMat;

    // Start is called before the first frame update

    private void Start() {
        transform.parent = null;
        displayFrame = transform.GetChild(0).gameObject;
        if(displayFrame != null) displayFrameMat = displayFrame.GetComponent<Renderer>().material;
    }
    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            if(isOnGrid)gridSnap();
            else {
                displayFrame.transform.position = transform.position;
                displayFrame.transform.rotation = transform.rotation;
            }
        }
    }

    //snaps display frame to canvas grid
    void gridSnap()
    {
        Vector3 localPos = canvas.InverseTransformPoint(transform.position);
        Vector3 localScale = new Vector3(transform.localScale.x * canvas.localScale.x,transform.localScale.y * canvas.localScale.y,1f);
        Vector3 gridPos = canvas.InverseTransformPoint(transform.localPosition);
        Vector3 minusPoint = canvas.InverseTransformPoint(transform.localPosition - (transform.localScale/2));
        Vector3 plusPoint = canvas.InverseTransformPoint(transform.localPosition - (transform.localScale/2));

        float leftXPos = getNearestGridValue(minusPoint.x, canvas.localScale.x) + ((transform.localScale.x/canvas.localScale.x)/2f);
        float rightXPos = getNearestGridValue(plusPoint.x, canvas.localScale.x) - ((transform.localScale.x/canvas.localScale.x)/2f);
        float leftDist = dist(localPos.x,leftXPos);
        float rightDist = dist(localPos.x,rightXPos);
        if(leftDist < rightDist)gridPos.x = leftXPos;
        else gridPos.x = rightXPos;

        float botYPos = getNearestGridValue(minusPoint.y, canvas.localScale.y) + ((transform.localScale.y/canvas.localScale.y)/2f);
        float topYPos = getNearestGridValue(plusPoint.y, canvas.localScale.y) - ((transform.localScale.y/canvas.localScale.y)/2f);
        float topDist = dist(localPos.y,topYPos);
        float botDist = dist(localPos.y,botYPos);
        if(topDist < botDist) gridPos.y = topYPos;
        else gridPos.y = botYPos;

        displayFrame.transform.position = canvas.TransformPoint(gridPos);
    }

    float dist(float x, float y){
        return Mathf.Abs(x-y);
    }

    //given a value and a grid scale, round to the nearest point
    float getNearestGridValue(float v, float scale){
        float gridSize = 0.05f;
        v = (v + 0.5f) * scale;
        v = Mathf.Round(v / gridSize) * gridSize;
        return (v / scale) - 0.5f;
    }

    private void OnCollisionEnter(Collision other) { //on a collision
        if(other.gameObject.layer == 7) //if the frame collides with a grid
        {
            Debug.Log("onGrid");
            canvas = other.transform;
            displayFrame.transform.parent = null;
            isOnGrid = true;
            LeanTween.color(displayFrame,selectedColor,0.2f).setEase(LeanTweenType.easeInQuad);
            displayFrameMat.SetFloat("_isBlinking",0);
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.layer == 7){
            isOnGrid = false;
            LeanTween.color(displayFrame,deleteColor,0.2f).setEase(LeanTweenType.easeInQuad);
            displayFrameMat.SetFloat("_isBlinking",1f);
        }
    }

    public void UpdateIsMoving(bool state){
        isMoving = state;
        Color targetColor = defaultColor;
        if(state) targetColor = selectedColor;
        LeanTween.color(displayFrame,targetColor,0.2f).setEase(LeanTweenType.easeInQuad);
        if(!state && !isOnGrid){
            GameObject.Destroy(displayFrame);
            GameObject.Destroy(gameObject);
        }
    }

    public void onHover(){
        LeanTween.value(gameObject,0,0.15f,0.2f).setEase(LeanTweenType.easeInQuad).setOnUpdate((float val) =>{displayFrameMat.SetFloat("Background",val);});
    }

    public void offHover(){
        LeanTween.value(gameObject,0.15f,0,0.3f).setEase(LeanTweenType.easeInQuad).setOnUpdate((float val) =>{displayFrameMat.SetFloat("Background",val);});
    }

}
