using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private float gridSpacing = 0.03f;
    private Material gridMat;
    private Material GridMat{
        get{
            if(gridMat == null) gridMat = gameObject.GetComponent<Renderer>().material;
            return gridMat;
        }
    }

    public void setGridSize(float f){
        if(f < 0.01f) return;
        gridSpacing = f;
        GridMat.SetFloat("LargeSpacing",f);
    }

    public float getGridSpacing(){return gridSpacing;}
}
