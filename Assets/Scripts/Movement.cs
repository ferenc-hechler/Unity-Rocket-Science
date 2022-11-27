using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            startThrustAudio();
        }
        else {
            if (audioSource.isPlaying) 
            {
                audioSource.Stop();
                mainThrustParticles.Stop();
            }
        }
    }

    private void startThrustAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            mainThrustParticles.Play();
        }
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void ProcessRotation() 
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (!leftThrustParticles.isPlaying) 
            {
                leftThrustParticles.Play();
            }
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) 
        {
            if (!rightThrustParticles.isPlaying) 
            {
                rightThrustParticles.Play();
            }
            ApplyRotation(-rotationThrust);
        }
        else {
            if (leftThrustParticles.isPlaying) 
            {
                leftThrustParticles.Stop();
            }
            if (rightThrustParticles.isPlaying) 
            {
                rightThrustParticles.Stop();
            }
        }
    }

    private void ApplyRotation(float thrust)
    {
        rb.freezeRotation = true;   // freezing rotation, so we can manually rotate
        transform.Rotate(Vector3.forward * thrust * Time.deltaTime);
        rb.freezeRotation = false;  // unfreeting rotation, so the physics system can take over
    }
}
