using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delayInSeconds = 3f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;

    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision other)
    {
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

    private void StartFriendlySequence()
    {
        // To Do
    }

    void StartSuccessSequence()
    {
        setMovement(false);
        audioSource.PlayOneShot(successSound);
        Invoke("NextLevel", delayInSeconds);
    }

    void StartCrashSequence()
    {
        setMovement(false);
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", delayInSeconds);
    }

    void ReloadLevel()
    {
        // Index, scene string or actice scene build index
        // SceneManager.LoadScene(0);
        // SceneManager.LoadScene("Sandbox");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        setMovement(true);
    }

    void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        setMovement(true);
    }

    void setMovement(bool isEnabled)
    {
        GetComponent<Movement>().enabled = isEnabled;
    }
}
