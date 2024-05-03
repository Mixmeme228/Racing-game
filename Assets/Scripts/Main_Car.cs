using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Car : MonoBehaviour
{
    public GameObject car;
    private float max=200;
    private void Start()
    {
        max = transform.GetComponent<RCC_CarControllerV3>().maxEngineTorque;
    }
    void Update()
    {
        if(car.GetComponent<Checkpoint_Car>().Srav- transform.GetComponent<Checkpoint_Car>().Srav>=6)
        {
            transform.GetComponent<RCC_CarControllerV3>().maxEngineTorque = 1.5f*max;
            transform.GetComponent<RCC_CarControllerV3>().maxspeed = 300;
        }
        else if (car.GetComponent<Checkpoint_Car>().Srav - transform.GetComponent<Checkpoint_Car>().Srav >= 2)
        {
            transform.GetComponent<RCC_CarControllerV3>().maxEngineTorque = 1.2f * max;
            transform.GetComponent<RCC_CarControllerV3>().maxspeed = 240;
        }
        else if (car.GetComponent<Checkpoint_Car>().Srav - transform.GetComponent<Checkpoint_Car>().Srav <= -2)
        {
            transform.GetComponent<RCC_CarControllerV3>().maxspeed = 175;
        }
        else if (car.GetComponent<Checkpoint_Car>().Srav - transform.GetComponent<Checkpoint_Car>().Srav <= -18)
        {
            transform.GetComponent<RCC_CarControllerV3>().maxspeed = 80;
        }
        else
        {
            transform.GetComponent<RCC_CarControllerV3>().maxEngineTorque=max;
            transform.GetComponent<RCC_CarControllerV3>().maxspeed = 200;
        }

    }
}
