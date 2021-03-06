using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delayInSeconds = 3f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool isCollisonDisabled = false;
    bool isDebugOn = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if(isDebugOn)
        {
            RespondToDebugKeys();
        }
    }

    void RespondToDebugKeys() {
        if(Input.GetKeyDown(KeyCode.L)) 
        {
            NextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C)) 
        {
            // Toggle collission on / off
            isCollisonDisabled = !isCollisonDisabled;
        }    
    }

    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || isCollisonDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                StartFriendlySequence();
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartFriendlySequence()
    {
        // TO DO
    }

    void StartSuccessSequence()
    {
        StopMovementAndPlayAudio(successSound);
        successParticle.Play();
        Invoke("NextLevel", delayInSeconds);
    }

    void StartCrashSequence()
    {
        StopMovementAndPlayAudio(crashSound);
        crashParticle.Play();
        Invoke("ReloadLevel", delayInSeconds);
    }

    void ReloadLevel()
    {
        // Index, scene string or actice scene build index
        // SceneManager.LoadScene(0);
        // SceneManager.LoadScene("Sandbox");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        // Reloading will reset all values
    }

    void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        // Reloading will reset all values
    }

    void StopMovementAndPlayAudio(AudioClip audioClip) {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
}
