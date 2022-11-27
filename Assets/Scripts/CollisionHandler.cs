using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float nextLevelDelay = 1.0f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool checkCollision;

    void Start()
    {
        checkCollision = true;
        audioSource = GetComponent<AudioSource>();
    }

     void Update()
    {
        ProcessCheatKeys();
    }

    void ProcessCheatKeys() 
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            checkCollision = !checkCollision;
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            StartFinishedSequence();
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || !checkCollision) { return; }
        switch (other.gameObject.tag)
        {
            case "Fuel":
            case "Friendly":
                break;
            case "Finish":
                StartFinishedSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {   
        GetComponent<Movement>().enabled=false;
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        Invoke("ReloadLevel", nextLevelDelay);
    }

    void StartFinishedSequence()
    {   
        GetComponent<Movement>().enabled=false;
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex+1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

}
