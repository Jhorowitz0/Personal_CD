using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePlacementManager : MonoBehaviour
{
    public Transform[,] gizmos;
    private Vector2 selected;
    public Transform topLeft;
    public Transform topRight;
    public Transform botLeft;
    public Transform botRight;

    public Transform grid;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        gizmos = new Transform[2,2];
        gizmos[0,0] = botLeft;
        gizmos[1,0] = botRight;
        gizmos[0,1] = topLeft;
        gizmos[1,1] = transform.GetChild(3);
        selected = new Vector2(-1,-1);
    }

    // Update is called once per frame
    void Update()
    {
        if(selected == new Vector2(-1,-1) || gizmos[0,0].parent == transform)return;
        Vector3 selectedPos = transform.InverseTransformPoint(gizmos[(int)selected[0],(int)selected[1]].position);
        if(selected != new Vector2(-1,-1)){
            for(int i = 0; i < 2; i++){
                for(int j = 0; j < 2; j++){
                    if(i == selected[0]){
                        if(j == selected[1]) continue;
                        else{
                            Vector3 localPos = transform.InverseTransformPoint(gizmos[i,j].position);
                            localPos.x = selectedPos.x;
                            gizmos[i,j].position = transform.TransformPoint(localPos);
                        }
                    } else if(j == selected[1]){
                        Vector3 localPos = transform.InverseTransformPoint(gizmos[i,j].localPosition);
                        localPos.y = selectedPos.y;
                        gizmos[i,j].localPosition = transform.TransformPoint(localPos);
                    }
                }
            }
        }
        updateCanvasSize();
    }

    public void onGrab(Transform gizmo){
        unParent();
        for(int i = 0; i<2; i++){
            for(int j = 0; j < 2; j++){
                if(gizmo == gizmos[i,j]){
                    selected = new Vector2(i,j);
                    return;
                }
            }
        }
    }

    public void offGrab(){
        selected = new Vector2(-1,-1);
        parent();
    }

    public void onHover(){
        //LeanTween.value( gameObject, 0f, 1f, 0.2f).setOnUpdate( (float val)=>{mat.SetFloat("_Brightness",val);} );
    }

    public void offHover(){
        //LeanTween.value( gameObject, 1f, 0f, 0.2f).setOnUpdate( (float val)=>{mat.SetFloat("_Brightness",val);} );
    }

    public void updateBrightness(){

    }

    public void unParent(){
        for(int i = 0; i<2; i++){
            for(int j = 0; j<2; j++){
                gizmos[i,j].parent = transform.parent;
            }
        }
    }

    
    public void parent(){
        for(int i = 0; i<2; i++){
            for(int j = 0; j<2; j++){
                gizmos[i,j].parent = transform;
                gizmos[i,j].localEulerAngles = new Vector3(0,0,0);
                gizmos[i,j].localPosition = new Vector3(gizmos[i,j].localPosition.x,gizmos[i,j].localPosition.y,0f);
            }
        }
    }

    public void updateCanvasSize(){
        Vector3 pos = transform.position;
        pos = (gizmos[0,0].position + gizmos[1,0].position + gizmos[0,1].position + gizmos[1,1].position)/4;
        transform.position = pos;
        Vector3 scale = transform.localScale;
        scale.x = Vector3.Distance(gizmos[0,0].position,gizmos[1,0].position);
        scale.y = Vector3.Distance(gizmos[0,0].position, gizmos[0,1].position);
        transform.localScale = scale;
    }

    public void placeCanvas(){
        grid.gameObject.SetActive(true);
        grid.parent = transform.parent;
        GameObject.Destroy(gameObject);
    }
}

