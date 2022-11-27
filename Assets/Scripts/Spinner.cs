using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spinner : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
    }



    void ProcessRotation() 
    {
        // rb.freezeRotation = true;   // freezing rotation, so we can manually rotate
        transform.Rotate(-1.0f * Vector3.forward * rotationSpeed * Time.deltaTime);
        // rb.freezeRotation = false;  // unfreeting rotation, so the physics system can take over

    }
}
