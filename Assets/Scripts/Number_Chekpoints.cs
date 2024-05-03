using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number_Chekpoints : MonoBehaviour
{
    [SerializeField] GameObject[] Check;
    void Start()
    {
        for(int i=0; i < Check.Length; i++)
        {
            Check[i].name=i.ToString();
        }
    } 
}
