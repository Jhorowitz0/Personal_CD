using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;

public class UpdatePointerGlow : MonoBehaviour, IMixedRealityFocusHandler
{
    private Material material;
    public GameObject highlightPlate;
    public IMixedRealityPointer Pointer;

    private bool isInFocus = false;

    private void Awake()
    {
        material = highlightPlate.GetComponent<Renderer>().material;
    }

    void IMixedRealityFocusHandler.OnFocusEnter(FocusEventData eventData)
    {
        isInFocus = true;
        var pointers = new HashSet<IMixedRealityPointer>();
        foreach (var inputSource in CoreServices.InputSystem.DetectedInputSources)
        {
            foreach (var pointer in inputSource.Pointers)
            {
                if (pointer.IsInteractionEnabled && !pointers.Contains(pointer))
                {
                    pointers.Add(pointer);
                    Pointer = pointer;
                }
            }
        }
    }

    void IMixedRealityFocusHandler.OnFocusExit(FocusEventData eventData)
    {
        isInFocus = false;
    }

    private void Update() {
        if(isInFocus && Pointer.IsInteractionEnabled)material.SetVector("_GlowPoint",Pointer.Position);
    }
}
