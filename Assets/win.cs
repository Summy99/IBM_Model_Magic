using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour
{
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
