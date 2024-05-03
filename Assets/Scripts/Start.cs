using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start1 : MonoBehaviour
{
    public Material mat;
    public int target_lab = 0;
    public GameObject text1;
    private void Start()
    {
        mat.color = Color.red;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
           if(other.gameObject.transform.GetComponentInParent<Checkpoint_Car>().Active)
            {
                if(other.gameObject.transform.GetComponentInParent<Checkpoint_Car>().Checkpoint>=200)
                    {
                    other.gameObject.transform.GetComponentInParent<Checkpoint_Car>().Lap += 1;
                    if(other.gameObject.transform.GetComponentInParent<Checkpoint_Car>().Lap ==target_lab&& other.gameObject.tag == "Player")
                    {
                        text1.GetComponent<lose>().End_race();
                    }
                }else if(other.gameObject.transform.GetComponentInParent<Checkpoint_Car>().Checkpoint <= 150)
                {
                    other.gameObject.transform.GetComponentInParent<Checkpoint_Car>().Active=false;
                    other.gameObject.transform.GetComponentInParent<Checkpoint_Car>().Checkpoint = 0;
                }
            }else
            {
                other.gameObject.transform.GetComponentInParent<Checkpoint_Car>().Active=true;
            }
        }
    }
}
