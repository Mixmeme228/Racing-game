using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject Wheel;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float smooth = 5.0f;
    float tiltAngle = 1.0f;
    float tiltAroundZ = 0;
    float tiltAroundX = 0;
    // Update is called once per frame
    void Update()
    {
        
        Rigidbody rb = GetComponent<Rigidbody>();
        if((GetComponent<Rigidbody>().velocity.x) * (GetComponent<Rigidbody>().velocity.x) + (GetComponent<Rigidbody>().velocity.z) * (GetComponent<Rigidbody>().velocity.z)<=60)
        rb.velocity += new Vector3(1f * Input.GetAxis("Vertical"), 0f, 0f );
        
     
    }
}

