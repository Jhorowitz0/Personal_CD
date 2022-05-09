using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverGlow : MonoBehaviour
{
    public Color defaultColor;
    public Color hoverColor;

    private Vector3 initialScale;

    public bool grow = false;

    public float hoverGrowMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        setGlow(defaultColor);
    }

    // Update is called once per frame
    public void onHover(){
        setGlow(hoverColor);
        if(grow)LeanTween.scale(gameObject,initialScale*hoverGrowMultiplier,0.2f);
    }

    public void offHover(){
        setGlow(defaultColor);
        if(grow)LeanTween.scale(gameObject,initialScale,0.2f);
    }

    void setGlow(Color c){
        foreach(Transform child in transform){
            LeanTween.color(child.gameObject,c,0.2f);
        }
    }
}
