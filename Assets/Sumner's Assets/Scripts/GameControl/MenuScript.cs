using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
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
}
