using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int lives = 6;


    public Dictionary<string, bool> words = new Dictionary<string, bool>();
    public bool[] letters = new bool[] { 
        /*A*/true, /*B*/true, /*C*/true, /*D*/true, /*E*/true, /*F*/true, /*G*/true, 
        /*H*/true, /*I*/true, /*J*/true, /*K*/true, /*L*/true, /*M*/true, /*N*/true,  
        /*O*/true, /*P*/true, /*Q*/true, /*R*/true, /*S*/true, /*T*/true, /*U*/true,
        /*V*/true, /*W*/true, /*X*/true, /*Y*/true, /*Z*/true };

    void Start()
    {
        words.Add("BOMB", true);
        words.Add("SHIELD", true);
        words.Add("ERASE", true);
        words.Add("SLOW", true);
        words.Add("BOOM", false);
        words.Add("AUTO", false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
