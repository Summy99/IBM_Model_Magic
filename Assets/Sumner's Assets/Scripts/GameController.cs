using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int lives = 6;


    public Dictionary<string, bool> words = new Dictionary<string, bool>();
    public bool[] letters = new bool[] { 
        /*A*/true, /*B*/true, /*C*/false, /*D*/true, /*E*/true, /*F*/false, /*G*/false, 
        /*H*/true, /*I*/true, /*J*/false, /*K*/false, /*L*/true, /*M*/true, /*N*/false,  
        /*O*/true, /*P*/false, /*Q*/false, /*R*/true, /*S*/true, /*T*/false, /*U*/false,
        /*V*/false, /*W*/true, /*X*/false, /*Y*/false, /*Z*/false };

    void Start()
    {
        words.Add("BOMB", true);
        words.Add("SHIELD", true);
        words.Add("ERASE", true);
        words.Add("SLOW", true);
        words.Add("BOOM", false);
    }

    void Update()
    {

    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
