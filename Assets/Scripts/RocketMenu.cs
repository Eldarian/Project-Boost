using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMenu : MonoBehaviour
{
    [SerializeField] float rcsForce = 100;
    private Rigidbody rigidbody;
    AudioSource audioSource;
    private bool isTranscending = false;




    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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

    







}
