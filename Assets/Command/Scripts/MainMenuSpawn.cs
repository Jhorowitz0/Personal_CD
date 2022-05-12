using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpawn : MonoBehaviour
{
    public Transform reference;
    // Start is called before the first frame update
    public void onSpawn(){
        transform.position = reference.position;
        transform.eulerAngles = new Vector3(0f,reference.eulerAngles.y,0f);
        gameObject.SetActive(true);
    }
}
