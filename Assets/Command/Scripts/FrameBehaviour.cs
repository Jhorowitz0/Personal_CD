using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameBehaviour : MonoBehaviour
{
    private Transform canvas = null;
    bool isOnGrid = false; //is the frame ready to be deleted?
    bool isMoving = true; //is the frame being moved right now
    
    private GameObject displayFrame;

    public Color defaultColor;
    public Color selectedColor;

    public Color deleteColor;

    private Material displayFrameMat;

    // Start is called before the first frame update

    private void Start() {
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
        float gridSpacing = 0.6f;
        Vector3 localPos = canvas.InverseTransformPoint(transform.position);
        localPos.z = 0;
        Vector3 offset = new Vector3(transform.localScale.x / canvas.localScale.x,transform.localScale.y / canvas.localScale.y,0f) * 0.5f;
        Vector3 minusPos = localPos - offset;
        Vector3 minusGridPos = new Vector3(getNearestGridValue(minusPos.x,canvas.localScale.x,gridSpacing),getNearestGridValue(minusPos.y,canvas.localScale.y,gridSpacing),0f);
        Vector3 plusPos = localPos + offset;
        Vector3 plusGridPos = new Vector3(getNearestGridValue(plusPos.x,canvas.localScale.x,gridSpacing),getNearestGridValue(plusPos.y,canvas.localScale.y,gridSpacing),0f);

        if(dist(minusPos.x,minusGridPos.x) < dist(plusPos.x,plusGridPos.x)) localPos.x = minusGridPos.x + offset.x;
        else localPos.x = plusGridPos.x - offset.x;

        if(dist(minusPos.y,minusGridPos.y) < dist(plusPos.y,plusGridPos.y)) localPos.y = minusGridPos.y + offset.y;
        else localPos.y = plusGridPos.y - offset.y;

        displayFrame.transform.position = canvas.TransformPoint(localPos);
        displayFrame.transform.rotation = canvas.transform.rotation;
    }

    private Vector3 nearestGridPos(Vector3 pos, Vector3 scale, float gridSpacing){
        Vector3 result = pos;
        result = new Vector3(result.x * scale.x, result.y * scale.y,0f);
        result.x = Mathf.Round(result.x / gridSpacing) * gridSpacing;
        result.y = Mathf.Round(result.y / gridSpacing) * gridSpacing;
        result = new Vector3(result.x / scale.x, result.y * scale.y,0f);
        return result;
    }

    float dist(float x, float y){
        return Mathf.Abs(x-y);
    }

    //given a value and a grid scale, round to the nearest point
    float getNearestGridValue(float v, float scale, float gridSpacing){
        v *= scale;
        v = Mathf.Round(v / gridSpacing) * gridSpacing;
        return (v / scale);
    }

    private void OnCollisionEnter(Collision other) { //on a collision
        if(other.gameObject.layer == 7) //if the frame collides with a grid
        {
            Debug.Log("onGrid");
            canvas = other.transform;
            Debug.Log(canvas.gameObject.name);
            displayFrame.transform.parent = null;
            isOnGrid = true;
            LeanTween.color(displayFrame,selectedColor,0.2f).setEase(LeanTweenType.easeInQuad);
            displayFrameMat.SetFloat("_isBlinking",0);
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.layer == 7){
            Debug.Log("offGrid");
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
            GameObject.Destroy(transform.parent.gameObject);
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
