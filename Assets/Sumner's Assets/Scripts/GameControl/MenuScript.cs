using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public static float MasterVolume = 1;
    public static float SFXVolume = 1;
    public static float MusicVolume = 1;

    [SerializeField] private Slider master, SFX, music;
    [SerializeField] private GameObject settings, main;
    [SerializeField] private AudioSource musicSrc;

    void Update()
    {
        musicSrc.volume = 0.5f * MusicVolume * MasterVolume;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartGame(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartGame(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartGame(3);
        }
    }

    public void StartGame(int level)
    {
        GameController.level = level;
        LevelTracker.LevelToLoad = level;
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        main.SetActive(false);
        settings.SetActive(true);
    }

    public void Back()
    {
        main.SetActive(true);
        settings.SetActive(false);
    }

    public void UpdateVolume()
    {
        MasterVolume = master.value;
        SFXVolume = SFX.value;
        MusicVolume = music.value;
    }
}
