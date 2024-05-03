using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool PauseGame;
    public GameObject pauseMenu,EndMenu,Director;
    public AudioSource[] soundtracks;
    public GameObject[] vehicles;
    public RCC_Settings rcc_sound;
    public AudioMixerGroup mixer;
   

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&Director.GetComponent<PlayableDirector>().time<16)
        {
            Director.GetComponent<PlayableDirector>().time =16;
        }
        if (Input.GetKeyDown(KeyCode.Escape)&&!EndMenu.active)
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resstart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Resume()
    {
        
        foreach (GameObject vehicle in vehicles)
        {

            vehicle.GetComponent<Checkpoint_Car>().sound_gav.SetActive(true);
        }
        rcc_sound.foldSFX = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
        foreach (AudioSource soundtrack in soundtracks)
        {
            soundtrack.Play();
        }
       
    }

    public void Pause()
    {
       
        foreach (GameObject vehicle in vehicles)
        {
            vehicle.GetComponent<Checkpoint_Car>().sound_gav.SetActive(false);
        }
        pauseMenu.SetActive(true);
       
        Time.timeScale = 0f;
        PauseGame = true;
        foreach (AudioSource soundtrack in soundtracks)
        {
            soundtrack.Pause();
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}