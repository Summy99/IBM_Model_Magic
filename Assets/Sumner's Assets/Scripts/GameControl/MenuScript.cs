using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public static float MasterVolume = 1;
    public static float SFXVolume = 1;
    public static float MusicVolume = 1;

    [SerializeField] private Slider master, SFX, music;
    [SerializeField] private GameObject settings, main;
    [SerializeField] private AudioSource musicSrc;
    [SerializeField] private TMP_Dropdown resolutionDrop;

    private void Awake()
    {
        resolutionDrop.value = 3;
    }

    void Update()
    {
        musicSrc.volume = 0.5f * MusicVolume * MasterVolume;
    }

    public void StartGame(int level)
    {
        GameController.Level = level;
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

    public void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }

    public void UpdateResolution()
    {
        switch (resolutionDrop.value)
        {
            case 0:
                Screen.SetResolution(640, 480, Screen.fullScreenMode);
                break;

            case 1:
                Screen.SetResolution(800, 600, Screen.fullScreenMode);
                break;

            case 2:
                Screen.SetResolution(960, 720, Screen.fullScreenMode);
                break;

            case 3:
                Screen.SetResolution(1024, 768, Screen.fullScreenMode);
                break;

            case 4:
                Screen.SetResolution(1280, 960, Screen.fullScreenMode);
                break;

            case 5:
                Screen.SetResolution(1400, 1050, Screen.fullScreenMode);
                break;

            case 6:
                Screen.SetResolution(1600, 1200, Screen.fullScreenMode);
                break;

            case 7:
                Screen.SetResolution(1856, 1392, Screen.fullScreenMode);
                break;

            case 8:
                Screen.SetResolution(1920, 1440, Screen.fullScreenMode);
                break;

            case 9:
                Screen.SetResolution(2048, 1536, Screen.fullScreenMode);
                break;
        }
    }
}
