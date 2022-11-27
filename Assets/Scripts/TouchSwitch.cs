using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSwitch : MonoBehaviour
{

    [SerializeField] Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        myLight.enabled = false;
    }

    void OnCollisionEnter(Collision other) 
    {
        Debug.Log("SWITCH LIGHTS");
        myLight.enabled = true;
    }

}
