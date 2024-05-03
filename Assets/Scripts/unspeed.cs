using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unspeed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponentInParent<RCC_CarControllerV3>().maxEngineTorque = other.gameObject.GetComponentInParent<RCC_CarControllerV3>().maxEngineTorque - 300f;
            other.gameObject.GetComponentInParent<RCC_AICarController>().useRaycasts = true;
        }
        Debug.Log(other.name);
    }
}
