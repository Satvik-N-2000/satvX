using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    
   

    [SerializeField] AudioClip mainAudio;
    
    [SerializeField] AudioClip deathAudio;
    enum State { Alive, Dying, Transceding };
    State state = State.Alive;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (state == State.Alive)
        {
            Thrust();
            Sideways();
            
        }

    }

      void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }

        else
        {
            switch (collision.gameObject.tag)
            {
                case "Blocker1 ":
                    DeathProcess();

                    break;
                case "Finish":
                    state = State.Transceding;
                    
                    successParticles.Play();
                    
                    Invoke("LoadNextLevel", 1f);
                    break;

                
                    
                     
                default:
                    //do nothing
                    break;
            }
        }
    }

     

    void DeathProcess()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathAudio);
        deathParticles.Play();
        Invoke("LoadSameScene", 1f);
    }



    void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

      void LoadSameScene()
    {
        SceneManager.LoadScene(0);
    }


    

    private void Sideways()
    {
        rigidBody.freezeRotation = true;//taaking manual control of the rotation

        if (Input.GetKey(KeyCode.A))
        {
            float rcsThrust = 90f;//changing th espeeed

            float rotationframe = rcsThrust * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationframe);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            float rcsThrust = 90f;
            float rotationframe = rcsThrust * Time.deltaTime;
            transform.Rotate(-Vector3.forward * rotationframe);
        }

        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        float thrustup = 190f;
        float thrustframe = thrustup * Time.deltaTime;

        if (Input.GetKey(KeyCode.W)) // can thrust while rotating
        {
            ApplyThrust(thrustframe);

        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust(float thrustframe)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustframe);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainAudio);
            mainEngineParticles.Play();
        }

    }
}