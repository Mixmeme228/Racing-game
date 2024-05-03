using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lose : MonoBehaviour
{
   public  GameObject Place,Player_camera,Player_Virtual_camera,Move_Target_camera,Map,Pos_text,Speedometer;
    public Material mat;
    GameObject[] Cars;
    public void sec_3()
    {
        transform.GetComponent<Text>().text = "3";
       
    }
    public void sec_2()
    {
        transform.GetComponent<Text>().text = "2";
        mat.color = Color.yellow;
    }
    public void sec_1()
    {
        transform.GetComponent<Text>().text = "1";
        mat.color = Color.green;
    }
    public void Go()
    {
        transform.GetComponent<Text>().text = "GO!!";
        for (int i = 0; i < Cars.Length; i++)
        {
                Cars[i].GetComponent<RCC_CarControllerV3>().canControl = true;
        }
            Invoke("off", 0.5f);
    }
    public void off()
    {
        transform.GetComponent<Text>().enabled = false;
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 137, 0);
    }
    public void Start()
    {
        Cars = Place.GetComponent<Place>().Cars;
        mat.color = Color.red;
        Invoke("sec_3", 1f);
        Invoke("sec_2", 2f);
        Invoke("sec_1", 3f);
        Invoke("Go", 4f);
    }
    // Update is called once per frame
    public void End_race()
    {
       for (int i=0;i< Cars.Length;i++)
        {
            if (Cars[i].tag=="Player")
            {
                switch(i)
                {
                    case 0:
                        transform.GetComponent<Text>().text = "You win!!!";
                        transform.GetComponent<Text>().color = Color.green;
                        break;
                    case 1:
                        transform.GetComponent<Text>().text = "Nice try!";
                        transform.GetComponent<Text>().color = Color.yellow;
                        break;
                    case 3:
                        transform.GetComponent<Text>().text = "Nice try!";
                        transform.GetComponent<Text>().color = Color.yellow;
                        break;
                    case 4:
                        transform.GetComponent<Text>().text = "You lose!";
                        transform.GetComponent<Text>().color = Color.red;
                        break;

                }
                Cars[i].GetComponent<RCC_AICarController>().enabled=true;
                Player_camera.SetActive(true);
                Player_Virtual_camera.SetActive(true);
                Move_Target_camera.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(true);
                Map.SetActive(false);
                Speedometer.SetActive(false);
                Pos_text.SetActive(false);
            }else
            {
                Cars[i].GetComponent<RCC_AICarController>().maximumSpeed = 100;
            }
        }
        transform.GetComponent<Text>().enabled = true;
    }
}
