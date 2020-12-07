using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float rotationSpeed = 100;
    [SerializeField] private float force = 50;
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
            print("Rotating left");
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("Rotating right");
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime * -1);

        }

        rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("Space pressed");
            rigidbody.AddRelativeForce(Vector3.up * force, ForceMode.Force);
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
}
