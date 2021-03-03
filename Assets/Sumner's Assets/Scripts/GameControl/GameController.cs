using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static int lives = 6;
    public static int level = 1;

    public bool tutorial = false;
    private bool tutorialIncrementing = false;
    private bool enemySpawned = false;
    public int tutorialStage = 0;
    public int keycaps = 0;

    private GameObject player;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TextMeshProUGUI tutorialMsg;
    [SerializeField] private Transform spawn;
    [SerializeField] private GameObject tutorialEnemy, trackball, prompt, skipButton;
    [SerializeField] private AudioClip mainTheme;

    public static Dictionary<string, bool> words = new Dictionary<string, bool>();
    public bool[] letters = new bool[] { 
        /*A*/true, /*B*/true, /*C*/true, /*D*/true, /*E*/true, /*F*/true, /*G*/true, 
        /*H*/true, /*I*/true, /*J*/true, /*K*/true, /*L*/true, /*M*/true, /*N*/true,  
        /*O*/true, /*P*/true, /*Q*/true, /*R*/true, /*S*/true, /*T*/true, /*U*/true,
        /*V*/true, /*W*/true, /*X*/true, /*Y*/true, /*Z*/true };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(level == 1)
        {
            words.Add("BOMB", false);
            words.Add("SHIELD", false);
            words.Add("ERASE", false);
            words.Add("CLEAR", false);
            words.Add("BOOM", false);
            words.Add("AUTO", false);
            words.Add("SPREAD", false);
            words.Add("SHOTGUN", false);
            words.Add("BLOCK", false);
            words.Add("RAPID", false);
            words.Add("SPEED", false);
            words.Add("FAST", false);
            words.Add("OTHER", false);
            words.Add("SOMETHING", false);
            words.Add("RANDOM", false);
            words.Add("ANYTHING", false);
            words.Add("EXPLOSION", false);
            words.Add("BIG", false);
            words.Add("GIANT", false);

            tutorial = true;
            tutorialMsg.text = "Hello! I'm Trackball the mouse, welcome to IBM Model Magic!";
            prompt.SetActive(true);
            Typing.maxSlowDown = 15;
            player.GetComponent<Typing>().slowDown = 15;
        }

        if(level == 2)
        {
            foreach(KeyValuePair<string, bool> entry in words)
            {
                if (entry.Value)
                {
                    gameObject.GetComponent<UI>().AddWord(entry.Key);
                }
            }

            player.GetComponent<Typing>().slowDown = Typing.maxSlowDown;

            message.text = "Level 2";
            StartCoroutine("BlankMessage");
        }

        if (level == 3)
        {
            foreach (KeyValuePair<string, bool> entry in words)
            {
                if (entry.Value)
                {
                    gameObject.GetComponent<UI>().AddWord(entry.Key);
                }
            }

            player.GetComponent<Typing>().slowDown = Typing.maxSlowDown;

            message.text = "Level 3";
            StartCoroutine("BlankMessage");
        }
    }

    void Update()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume = 0.5f * MenuScript.MasterVolume * MenuScript.MusicVolume;

        if (lives > 6)
        {
            lives = 6;
        }

        if(tutorial)
        {
            if(tutorialStage == 0 && !tutorialIncrementing)
            {
                if(Input.GetKeyDown(KeyCode.Z))
                {
                    StartCoroutine("IncrementTutorial", 0.1f);
                }
            }
            if(tutorialStage == 1)
            {
                tutorialMsg.text = "You can move with the arrow keys. Try it!";
                prompt.SetActive(false);
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    tutorialStage = 2;
                }
            }

            if(tutorialStage == 2 && !tutorialIncrementing)
            {
                tutorialMsg.text = "If you hold shift, you can slow your movement and see your hitbox.";
                prompt.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    StartCoroutine("IncrementTutorial", 0.1f);
                }
            }

            if (tutorialStage == 3 && !tutorialIncrementing)
            {
                tutorialMsg.text = "Emojis are invading! You can fight back with your magical IBM Model M keyboard.";
                prompt.SetActive(true);
                prompt.GetComponent<PromptFlash>().StartCoroutine("Flash");
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    StartCoroutine("IncrementTutorial", 0.1f);
                }
            }

            if(tutorialStage == 4)
            {
                tutorialMsg.text = "Here comes one now! Press space or enter to slow time and start typing.";
                prompt.SetActive(false);
                if (!enemySpawned)
                {
                    Instantiate(tutorialEnemy, spawn.position, Quaternion.identity);
                    enemySpawned = true;
                }

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    tutorialStage = 5;
                }
            }

            if(tutorialStage == 5)
            {
                tutorialMsg.text = "Type in a word here to shoot it! Press space or enter to fire when you're done.";
                prompt.SetActive(false);
            }

            if (tutorialStage == 6 && !tutorialIncrementing)
            {
                tutorialMsg.text = "That one dropped a keycap, make sure to pick it up. You can spend them later on upgrades.";
                prompt.SetActive(true);
                prompt.GetComponent<PromptFlash>().StartCoroutine("Flash");
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    StartCoroutine("IncrementTutorial", 0.1f);
                }
            }

            if (tutorialStage == 7)
            {
                tutorialMsg.text = "There are certain words of power that have special effects. Try putting in \"AUTO.\"";
                prompt.SetActive(false);
            }

            if (tutorialStage == 8 && !tutorialIncrementing)
            {
                tutorialMsg.text = "There's plenty other words! Experiment and try to find them all!";
                prompt.SetActive(true);
                prompt.GetComponent<PromptFlash>().StartCoroutine("Flash");
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    StartCoroutine("IncrementTutorial", 0.1f);
                }
            }

            if(tutorialStage == 9)
            {
                tutorialMsg.text = "... Okay fine, you can have one more for free. Try \"SHIELD.\"";
                prompt.SetActive(false);
            }

            if (tutorialStage == 10 && !tutorialIncrementing)
            {
                tutorialMsg.text = "Careful with that one; you can't type while it's active!";
                prompt.SetActive(true);
                prompt.GetComponent<PromptFlash>().StartCoroutine("Flash");
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    StartCoroutine("IncrementTutorial", 0.1f);
                }
            }

            if (tutorialStage == 11 && !tutorialIncrementing)
            {
                tutorialMsg.text = "Here come more emojis, get ready!";
                prompt.SetActive(false);
                StartCoroutine("IncrementTutorial", 2);
            }

            if(tutorialStage == 12)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = mainTheme;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
                tutorialMsg.text = "";
                tutorial = false;
                Typing.maxSlowDown = 3;
                trackball.SetActive(false);
                prompt.SetActive(false);
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

    public void SkipTutorial()
    {
        tutorialStage = 11;
        skipButton.SetActive(false);
    }

    private IEnumerator BlankMessage()
    {
        yield return new WaitForSeconds(1);
        message.text = "";
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
