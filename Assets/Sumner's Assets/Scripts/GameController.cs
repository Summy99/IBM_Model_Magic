using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int lives = 6;

    public bool tutorial = false;
    private bool tutorialIncrementing = false;
    public int tutorialStage = 0;
    public int keycaps = 0;

    private GameObject player;
    [SerializeField] private TextMeshProUGUI message;

    public Dictionary<string, bool> words = new Dictionary<string, bool>();
    public bool[] letters = new bool[] { 
        /*A*/true, /*B*/true, /*C*/true, /*D*/true, /*E*/true, /*F*/true, /*G*/true, 
        /*H*/true, /*I*/true, /*J*/true, /*K*/true, /*L*/true, /*M*/true, /*N*/true,  
        /*O*/true, /*P*/true, /*Q*/true, /*R*/true, /*S*/true, /*T*/true, /*U*/true,
        /*V*/true, /*W*/true, /*X*/true, /*Y*/true, /*Z*/true };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        words.Add("BOMB", false);
        words.Add("SHIELD", false);
        words.Add("ERASE", false);
        words.Add("SLOW", false);
        words.Add("BOOM", false);
        words.Add("AUTO", false);
        words.Add("SPREAD", false);

        tutorial = true;
        message.text = "Move with arrow keys";
        player.GetComponent<Typing>().maxSlowDown = 15;
        player.GetComponent<Typing>().slowDown = 15;
    }

    void Update()
    {
        if(tutorial)
        {
            if(tutorialStage == 0)
            {
                if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    tutorialStage = 1;
                }
            }
            if(tutorialStage == 1)
            {
                message.text = "Press Enter or Space";
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    tutorialStage = 2;
                }
            }

            if (tutorialStage == 2)
            {
                message.text = "Type \"AUTO\" \n Don't worry, we've given you plenty of time.";
            }

            if(tutorialStage == 3)
            {
                message.text = "Press Enter or Space to confirm";
            }

            if(tutorialStage == 4 && !tutorialIncrementing)
            {
                message.text = "You've discovered a new word!";
                StartCoroutine("IncrementTutorial", 3);
            }

            if (tutorialStage == 5 && !tutorialIncrementing)
            {
                message.text = "Try some other words and see what happens!";
                StartCoroutine("IncrementTutorial", 3);
            }

            if (tutorialStage == 6 && !tutorialIncrementing)
            {
                message.text = "Get ready! Here come the enemies.";
                StartCoroutine("IncrementTutorial", 3);
            }

            if (tutorialStage == 7 && !tutorialIncrementing)
            {
                message.text = "";
                player.GetComponent<Typing>().maxSlowDown = 3;
                tutorial = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private IEnumerator IncrementTutorial(float time)
    {
        tutorialIncrementing = true;
        yield return new WaitForSeconds(time);
        tutorialStage++;
        tutorialIncrementing = false;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
