using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InOutTrans_Manager : MonoBehaviour
{
    public LeanTweenType EaseIn;
    public float EaseTime;
    public float Delay;

    public bool fadeout = false;

    public Vector3 targetScale;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        if(targetScale == new Vector3(0,0,0)) targetScale = transform.localScale;
        transform.localScale = new Vector3(0,0,0);
        LeanTween.scale(gameObject,targetScale,EaseTime).setEase(EaseIn).setDelay(Delay);
    }

    public void OnHide() {
        if(!fadeout){
            gameObject.SetActive(false);
            return;
        }
        Debug.Log("hide");
        LeanTween.scale(gameObject,new Vector3(0,0,0),EaseTime).setOnComplete(Disable);
    }

    private void Disable(){
        gameObject.SetActive(false);
    }
}
