using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Car : MonoBehaviour
{
    public bool Active = false;
    public float Distance ;
    public int Checkpoint = 0,Lap = 0,Col_Lab=0,Srav=0;
    public GameObject[] Checkpoints;
    public GameObject sound_gav;
    public bool Debug1=false;
    // Update is called once per frame
    private void Start()
    {
        sound_gav = transform.Find("All Audio Sources").gameObject;
    }
    private void Update()
    {
        Srav = Checkpoint + ((Checkpoints.Length - 1) * Lap);
    }
}
