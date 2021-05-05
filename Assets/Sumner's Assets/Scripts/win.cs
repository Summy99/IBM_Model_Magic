using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume = 0.5f * MenuScript.MasterVolume * MenuScript.MusicVolume;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameController.Level = 1;
            LevelTracker.LevelToLoad = 0;
            SceneManager.LoadScene(4);
        }
    }
}
