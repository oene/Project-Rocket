using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            case "Finish":
                Debug.Log("Finish");
                break;
            default:
                Debug.Log("Other...");
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel()
    {
        // Index, scene string or actice scene build index
        // SceneManager.LoadScene(0);
        // SceneManager.LoadScene("Sandbox");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
