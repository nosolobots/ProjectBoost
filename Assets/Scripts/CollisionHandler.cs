using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip finishSound;
    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private ParticleSystem successParticles;

    AudioSource audioSource;
    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                isTransitioning = true;
                Finish();
                break;       
            default:
                isTransitioning = true;
                Crash();
                break;
        }
    }

    void Crash()
    {
        // disable player controls
        GetComponent<Movement>().enabled = false;
        
        // play crash particles effect          
        crashParticles.Play();

        // play crash sound
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);

        // reload level
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void Finish()
    {
        // disable player controls
        GetComponent<Movement>().enabled = false;

        // play success particles effect
        successParticles.Play();

        // play finish sound
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        
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

