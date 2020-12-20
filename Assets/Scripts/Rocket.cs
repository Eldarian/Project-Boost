using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrustForce = 50;
    [SerializeField] float rcsForce = 100;
    private Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip levelLoadSFX;

    [SerializeField] ParticleSystem mainEngineVFX;
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] ParticleSystem levelLoadVFX;
    private bool isTranscending = false;




    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        levelLoadVFX = GameObject.Find("Success Particles").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        rigidbody.angularVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.A) && !isTranscending)
        {
            transform.Rotate(Vector3.forward, rcsForce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) && !isTranscending)
        {
            transform.Rotate(Vector3.forward, rcsForce * Time.deltaTime * -1);
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space) && !isTranscending)
        {
            rigidbody.AddRelativeForce(Vector3.up * thrustForce, ForceMode.Force);
            mainEngineVFX.Play();
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSFX);
            }

        }
        if (Input.GetKeyUp(KeyCode.Space) && !isTranscending)
        {
            audioSource.Stop();
            mainEngineVFX.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                if (!isTranscending)
                {
                    Win();
                }
                break;
            case "Fuel":
                Debug.Log("Fuel"); //TODO remove
                break;
            default:
                if (!isTranscending)
                {
                    Explode();
                }

                break;

        }
    }

    private void Win()
    {
        Debug.Log("Well Done!"); //TODO remove
        isTranscending = true;
        mainEngineVFX.Stop();
        levelLoadVFX.Play();
        audioSource.PlayOneShot(levelLoadSFX);
        Invoke("NextScene", 1.5f);
    }

    private void Explode()
    {
        isTranscending = true;
        mainEngineVFX.Stop();
        Debug.Log("Dead"); //TODO remove
        audioSource.PlayOneShot(explosionSFX);
        explosionVFX.Play();
        Invoke("RestartScene", 1.5f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void NextScene()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int levelsCount = SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene((currentLevel + 1)%levelsCount);
    }
}
