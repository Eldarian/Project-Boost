using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 moveDirection;
    [SerializeField] float magnitude;
    [SerializeField] float period;
    private Vector3 startingPos;
    [SerializeField] bool rotationMode = false;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float rawSin = magnitude * Mathf.Sin((2 * Mathf.PI) / period * Time.time);
        float movementFactor = rawSin * 2f + 0.5f;
        Vector3 offset = moveDirection * movementFactor;
        if (rotationMode)
        {
            transform.Rotate(Vector3.forward, rawSin);
        } 
        else
        {
            transform.position = startingPos + offset;
        }
    }
}
