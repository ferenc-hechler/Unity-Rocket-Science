using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    // [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 10f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    	if (period <= Mathf.Epsilon) {
    	    return;
    	}
    	float cycles = Time.time / period;
    	
    	const float tau = 2 * Mathf.PI;
    	float rawSinWave = Mathf.Sin(cycles * tau);
   	float movementFactor = 0.5f*(rawSinWave + 1f);
        Vector3 offset = movementVector * movementFactor;
        transform.position =  startingPosition + offset;
    }
}
