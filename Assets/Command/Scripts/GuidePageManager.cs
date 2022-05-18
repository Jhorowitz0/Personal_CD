using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePageManager : MonoBehaviour
{
    private int page = 0;
    public GameObject next;
    public GameObject last;

    private void loadPage(int p){
        transform.GetChild(page).gameObject.SetActive(false);
        page = p;
        transform.GetChild(page).gameObject.SetActive(true);
        last.SetActive(p>0);
        next.SetActive(p<transform.childCount-1);
    }

    public void nextPage(){
        if(page >= transform.childCount)return;
        loadPage(page+1);
    }

    public void lastPage(){
        if(page <=0) return;
        loadPage(page-1);
    }

    public void reset(){
        loadPage(0);
    }
}
