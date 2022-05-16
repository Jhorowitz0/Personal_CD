using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;

public class FrameCreationManager : MonoBehaviour
{
    private Vector2 measurements = new Vector2();
    public TextMeshProUGUI WidthText;
    public TextMeshProUGUI HeightText;
    public GameObject framePrefab;
    public GridObjectCollection Library;
    public GameObject background;
    private Vector2 slidePositions = new Vector2(0.03f,-0.0482f);


    public bool isEditingWidth = true;

    public void clear(){
        if(isEditingWidth)measurements[0] = 0;
        else measurements[1] = 0;
        updateText();
    }

    private void OnEnable() {
        measurements = new Vector2();
        updateText();
    }

    public void set(float n){
        if(isEditingWidth) measurements[0] = newNumber(measurements[0],n);
        else measurements[1] = newNumber(measurements[1],n);
        updateText();
    }

    private float newNumber(float measurement, float n){
        bool half = false;
        if(measurement % 1 != 0){
            half = true;
            measurement = Mathf.Floor(measurement);
        }
        measurement = measurement * 10 + n;
        if(half) measurement += 0.5f;
        return measurement;

    }

    public void updateText(){
        WidthText.text = measurements[0] + "in";
        HeightText.text = measurements[1] + "in";
    }

    public void toggleHalf(){
        if(isEditingWidth){
            if(measurements[0]%1 != 0) measurements[0] = Mathf.Floor(measurements[0]);
            else measurements[0] += 0.5f;
        } else {
            if(measurements[1]%1 != 0) measurements[1] = Mathf.Floor(measurements[1]);
            else measurements[1] += 0.5f;
        }
        updateText();
    }

    public void spawnFrame(){
        GameObject newFrame = GameObject.Instantiate<GameObject>(framePrefab);
        newFrame.transform.GetChild(0).GetComponent<FrameSpawner>().size = measurements;
        newFrame.transform.parent = Library.transform;
        Library.UpdateCollection();
        foreach(Transform child in Library.transform){
            FrameSpawner script = child.GetChild(0).GetComponent<FrameSpawner>();
            if(script != null) script.initialize();
        }
    }

    public void setEditing(bool state){
        if(state == isEditingWidth) return;
        isEditingWidth = state;
        if(state) LeanTween.moveLocalZ(background,slidePositions[0],0.1f).setEase(LeanTweenType.easeInCubic);
        else LeanTween.moveLocalZ(background,slidePositions[1],0.1f).setEase(LeanTweenType.easeInCubic);
    }
}
