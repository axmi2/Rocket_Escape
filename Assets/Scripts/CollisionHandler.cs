
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float CrashTime = 1f;
    [SerializeField] AudioClip Crash1;
    [SerializeField] AudioClip Crash2;
    [SerializeField] AudioClip ReachPad;

    [SerializeField] ParticleSystem CrashParticles;
    
    [SerializeField] ParticleSystem ReachPadParticles;

    AudioSource MyAudioSource;


    bool IsTransitioning = false;
    bool CollisionDisable = false;

    void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DisableCollison();
        LoadNextLevelDebug();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (IsTransitioning || CollisionDisable) { return; }
        
        switch (collision.gameObject.tag)
        {
            case "Friendly":

                Debug.Log("Collided with Friendly");
                break;


            case "Finish":
                
                    IsTransitioning = true;
                    SuccessSequence();
                
                break;


            default:
                
                    IsTransitioning = true;
                    CrashSequence();
                
                break;

        }
    }

    void SuccessSequence()
    {
        GetComponent<Movement>().enabled = false;
        MyAudioSource.Stop();
        MyAudioSource.PlayOneShot(ReachPad);
        ReachPadParticles.Play();
        Invoke("LoadNextLevel", 1f);
    }


    void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings-1 > SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        MyAudioSource.Stop();
        MyAudioSource.PlayOneShot(Crash1);
        CrashParticles.Play();
        MyAudioSource.PlayOneShot(Crash2);
        //TODO Add sound and particle effect
        Invoke("ReloadLevel", CrashTime);

    }

    void DisableCollison()
    {
        if (Input.GetKey(KeyCode.C))
        {
            CollisionDisable = !CollisionDisable;
            Debug.Log("Collision Disable = "+ CollisionDisable.ToString() );
        }
    }

    void LoadNextLevelDebug()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (SceneManager.sceneCountInBuildSettings - 1 > SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

    }

}
