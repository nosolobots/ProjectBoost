using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay;
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                //Debug.Log("You've finished the level!");
                Finish();
                break;       
            default:
                //Debug.Log("This thing is not friendly");
                //ReloadLevel();
                Crash();
                break;
        }
    }

    void Crash()
    {
        // disable player controls
        GetComponent<Movement>().enabled = false;
        
        // reload level
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void Finish()
    {
        // disable player controls
        GetComponent<Movement>().enabled = false;
        
        // load next level
        Invoke("NextLevel", levelLoadDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                
        // last scene?
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            // load first scene
            SceneManager.LoadScene(0);
        }
        else
        {   
            // load next scene
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}

