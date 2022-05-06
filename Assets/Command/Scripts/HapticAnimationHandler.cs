using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticAnimationHandler : MonoBehaviour
{
    private Animator AnimationManager;
    

    private void Start() {
        AnimationManager = GetComponent<Animator>();
    }

    public void onPress(Transform pressLocation){
        Vector3 pressPosition = transform.InverseTransformPoint(pressLocation.position);
        if(AnimationManager != null){
            AnimationManager.SetFloat("PressX",pressPosition.x);
            AnimationManager.SetFloat("PressY",pressPosition.z);
            AnimationManager.SetTrigger("OnPress");
        }
    }
}
