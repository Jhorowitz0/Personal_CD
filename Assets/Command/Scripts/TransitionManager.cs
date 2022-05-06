using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
       mat = GetComponent<Renderer>().material; 
    }

    public void FadeIn(){
        mat.SetFloat("_TimeOfStart", Time.time + 0.5f);
    }
}
