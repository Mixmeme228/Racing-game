using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

public class Place : MonoBehaviour
{
    public GameObject[] Cars;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Cars.Length - 1; i++)
        { for (int j = i + 1; j < Cars.Length; j++){
            if (Cars[i].GetComponent<Checkpoint_Car>().Lap == Cars[j].GetComponent<Checkpoint_Car>().Lap)
            {
                if (Cars[i].GetComponent<Checkpoint_Car>().Checkpoint < Cars[j].GetComponent<Checkpoint_Car>().Checkpoint)
                {
                    GameObject car = Cars[i];
                    Cars[i] = Cars[j];
                    Cars[j] = car;
                }
            }
            else if (Cars[i].GetComponent<Checkpoint_Car>().Lap < Cars[j].GetComponent<Checkpoint_Car>().Lap)
            {
                GameObject car = Cars[i];
                Cars[i] = Cars[j];
                Cars[j] = car;
            }
                
        }
        }
        
        for(int i = 0; i < Cars.Length ; i++)
        {
            if (Cars[i].tag == "Player")
            {
                transform.GetComponent<TextMeshProUGUI>().text = "Pos: "+(i+1).ToString()+"/5"+"\n"+"Lap:"+ (Cars[i].GetComponent<Checkpoint_Car>().Lap+1).ToString()+"/5";
            }
        }
    }
}
