using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_switch : MonoBehaviour
{
    private bool swicth=false;
    public GameObject camera0;
    public GameObject camera2;
    public GameObject camera3;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            swicth = !swicth;
            if(swicth)
            {
                camera0.SetActive(true);
                camera2.SetActive(true);
                camera3.SetActive(false);
            }
            else
            {
                camera0.SetActive(false);
                camera2.SetActive(false);
                camera3.SetActive(true);
            }
        }
        if(swicth && Input.GetKeyDown(KeyCode.Mouse1))
        {
            camera2.GetComponent<CinemachineFreeLook>().m_BindingMode = CinemachineOrbitalTransposer.BindingMode.LockToTarget;
                camera2.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 300;
                
        }
        if(swicth && Input.GetKeyUp(KeyCode.Mouse1))
        {
            camera2.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 0;
            camera2.GetComponent<CinemachineFreeLook>().m_XAxis.Value = 0;
            camera2.GetComponent<CinemachineFreeLook>().m_BindingMode = CinemachineOrbitalTransposer.BindingMode.SimpleFollowWithWorldUp;
        }
    }
}
