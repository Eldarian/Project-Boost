using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFall : MonoBehaviour
{
    [SerializeField] GameObject block;
    [SerializeField] float offset = -2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(block.transform.position.x, block.transform.position.y, block.transform.position.z + offset);
    }
}
