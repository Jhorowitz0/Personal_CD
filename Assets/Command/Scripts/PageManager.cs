using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public TransitionManager Transition;
    public Transform Background;
    public int PageNumber = 0;
    private int nextPage = 0;
    private Transform CurrentPage;

    public GameObject pageIndicator;

    public Transform pageNavigation;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPage = transform.GetChild(0);
    }

    public void OpenPage(int newPage){
        if(newPage == PageNumber)return;
        nextPage = newPage;
        Transition.gameObject.SetActive(true);
        Transition.FadeIn();
        HidePage(CurrentPage);
        LeanTween.moveLocalY(CurrentPage.gameObject,CurrentPage.localPosition.y - 0.011f,0.3f);
        LeanTween.moveLocalY(Background.gameObject,Background.localPosition.y - 0.02f,0.3f).setOnComplete(onComplete);

        if(newPage == 1)LeanTween.moveLocalZ(pageIndicator,0.0428f,0.2f).setEase(LeanTweenType.easeInOutQuad);
        if(newPage == 2)LeanTween.moveLocalZ(pageIndicator,-0.0105f,0.2f).setEase(LeanTweenType.easeInOutQuad);
        if(newPage == 3)LeanTween.moveLocalZ(pageIndicator,-0.0619f,0.2f).setEase(LeanTweenType.easeInOutQuad);
    }

    IEnumerator AfterDelay(float t){
        yield return new WaitForSeconds(t);

        resetY(Background);
        CurrentPage.gameObject.SetActive(false);
        resetY(CurrentPage);
        PageNumber = nextPage;
        CurrentPage = transform.GetChild(PageNumber);
        showPage(CurrentPage);
    }

    void onComplete(){
        StartCoroutine(AfterDelay(1.2f));
    }

    void resetY(Transform T){
        Vector3 pos = T.localPosition;
        pos.y = 0f;
        T.localPosition = pos;
        Transition.gameObject.SetActive(false);
    }

    void showPage(Transform page){
        foreach(Transform child in page){
            child.gameObject.SetActive(true);
        }
        page.gameObject.SetActive(true);
    }

    void HidePage(Transform page){
        foreach(Transform child in page){
            InOutTrans_Manager TransitionScript = child.gameObject.GetComponent<InOutTrans_Manager>();
            if(TransitionScript != null && TransitionScript.fadeout){
                TransitionScript.OnHide();
            }
        }
    }

    public void toggleNav(bool state){
        pageNavigation.gameObject.SetActive(state);
    }
}
