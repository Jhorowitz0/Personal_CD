using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeOrbAnim : MonoBehaviour
{
    private Transform smallOrb = null;

    private GameObject SmallOrb{
        get{
            if(smallOrb == null) smallOrb = transform.GetChild(0);
            return smallOrb.gameObject;
        }
    }

    public void setGrab(bool state){
        float size = 0.8f;
        if(state) size = 0.15f;
        LeanTween.scale(SmallOrb,new Vector3(size,size,size),0.3f).setEase(LeanTweenType.easeInOutQuad);
        Color c = new Color(1,1,1,1);
        if(state) c = new  Color(1,0,0,1);
        LeanTween.color(SmallOrb,c,0.3f).setEase(LeanTweenType.easeInOutQuad);
    }

    public void setHover(bool state){
        float size = 1;
        if(state) size = 1.2f;
        LeanTween.scale(gameObject,new Vector3(size,size,size),0.3f).setEase(LeanTweenType.easeInOutQuad);

        size = 0.3f;
        if(state) size = 0.7f;
        LeanTween.scale(SmallOrb,new Vector3(size,size,size),0.3f).setEase(LeanTweenType.easeInOutQuad);
    }
}
