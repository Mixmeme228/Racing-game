using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speed : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponentInParent<RCC_CarControllerV3>().maxEngineTorque = other.gameObject.GetComponentInParent<RCC_CarControllerV3>().maxEngineTorque+300f;
            
        }
        Debug.Log(other.name);
    }
}
