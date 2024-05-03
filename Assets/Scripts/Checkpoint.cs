using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")&& other.gameObject.transform.GetComponentInParent<Checkpoint_Car>().Active)
        {
            other.gameObject.transform.GetComponentInParent<Checkpoint_Car>().Checkpoint = int.Parse(transform.name);
        }
    }      
}
