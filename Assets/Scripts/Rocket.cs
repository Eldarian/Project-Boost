using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrustForce = 50;
    [SerializeField] float rcsForce = 100;
    private Rigidbody rigidbody;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rcsForce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, rcsForce * Time.deltaTime * -1);

        }

        rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrustForce, ForceMode.Force);
            if (!audio.isPlaying)
            {
                audio.Play();
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audio.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("Friend"); //TODO remove
                break;
            case "Finish":
                print("Well Done!"); //TODO remove
                break;
            case "Fuel":
                print("Fuel"); //TODO remove
                break;
            default:
                print("Dead"); //TODO remove
                Destroy(gameObject);
                break;

        }
    }
}
