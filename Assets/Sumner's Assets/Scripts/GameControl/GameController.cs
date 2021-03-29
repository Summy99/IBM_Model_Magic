using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Word
{
    public string Name { get; set; }
    public bool Unlocked { get; set; }
    public float CurCool { get; set; }
    public float MaxCool { get; set; }
}

public class GameController : MonoBehaviour
{
    public static int lives = 6;
    public static int Level = 1;

    public bool tutorial = false;
    public bool paused = false;
    private bool tutorialIncrementing = false;
    private bool enemySpawned = false;
    public int tutorialStage = 0;
    public static int keycaps = 0;

    private GameObject player;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TextMeshProUGUI tutorialMsg;
    [SerializeField] private Transform spawn;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject meme;
    [SerializeField] private GameObject tutorialEnemy, trackball, prompt, skipButton, arrow, arrow2;
    [SerializeField] private AudioClip mainTheme;

    public static List<Word> Words = new List<Word>() {
        new Word() {
                    Name = "TEST",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },
        new Word() {
                    Name = "BOMB",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "SHIELD",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "CLEAR",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 5f
                   },

        new Word() {
                    Name = "ERASE",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 5f
                   },

        new Word() {
                    Name = "BOOM",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "AUTO",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "SHOOT",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "FIRE",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "SPREAD",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "SCATTER",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "SHOTGUN",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "BLOCK",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "SPEED",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "FAST",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "SOMETHING",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "RANDOM",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "EXPLOSION",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "BIG",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "GIANT",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "HOMING",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "SEARCH",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "TRACKING",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "LASER",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
                   },

        new Word() {
                    Name = "BEAM",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
                   },

        new Word() {
                    Name = "GRAB",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   },

        new Word() {
                    Name = "COLLECT",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                   }};

    public bool[] letters = new bool[] { 
        /*A*/true, /*B*/true, /*C*/true, /*D*/true, /*E*/true, /*F*/true, /*G*/true, 
        /*H*/true, /*I*/true, /*J*/true, /*K*/true, /*L*/true, /*M*/true, /*N*/true,  
        /*O*/true, /*P*/true, /*Q*/true, /*R*/true, /*S*/true, /*T*/true, /*U*/true,
        /*V*/true, /*W*/true, /*X*/true, /*Y*/true, /*Z*/true };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(Level == 1)
        {
            if (!UI.started)
            {
                UI.started = true;

                /* obsolete code
                words.Add(new Word()
                {
                    Name = "BOMB",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                });

                words.Add(new Word()
                {
                    Name = "SHIELD",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 1f
                });
                words.Add("ERASE", false);
                words.Add("CLEAR", false);
                words.Add("BOOM", false);
                words.Add("AUTO", true);
                words.Add("SHOOT", false);
                words.Add("FIRE", false);
                words.Add("SPREAD", false);
                words.Add("SCATTER", true);
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
                words.Add("GRAB", false);
                words.Add("COLLECT", false);
                */

                tutorial = true;
                tutorialMsg.text = "Hello! I'm Trackball the mouse, welcome to IBM Model Magic!";
                prompt.SetActive(true);
                Typing.maxSlowDown = 15;
                player.GetComponent<Typing>().slowDown = 15;
            }
            else
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = mainTheme;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
                tutorialMsg.text = "";
                tutorial = false;
                Typing.maxSlowDown = 2.25f;
                player.GetComponent<Typing>().slowDown = Typing.maxSlowDown;
                trackball.SetActive(false);
                prompt.SetActive(false);
                skipButton.SetActive(false);
            }
        }

        if(Level == 2)
        {
            player.GetComponent<Typing>().slowDown = Typing.maxSlowDown;
            RemoveWords();

            message.text = "Level 2";
            StartCoroutine("BlankMessage");
        }

        if (Level == 3)
        {
            player.GetComponent<Typing>().slowDown = Typing.maxSlowDown;
            RemoveWords();

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
            Tutorial();
        }

        ManageCooldowns();

        if (Input.GetKeyDown(KeyCode.Escape) && player.GetComponent<Typing>().mode == "shooting")
        {
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                if (!meme.activeSelf)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                paused = true;
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();
            }
        }
    }

    public void Tutorial()
    {
        if (tutorialStage == 0 && !tutorialIncrementing)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine("IncrementTutorial", 0.1f);
            }
        }
        if (tutorialStage == 1)
        {
            tutorialMsg.text = "You can move with the arrow keys. Try it!";
            prompt.SetActive(false);
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                tutorialStage = 2;
            }
        }

        if (tutorialStage == 2 && !tutorialIncrementing)
        {
            tutorialMsg.text = "If you hold shift, you can slow your movement and see your hitbox.";
            prompt.SetActive(false);
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine("IncrementTutorial", 1f);
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

        if (tutorialStage == 4)
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

        if (tutorialStage == 5)
        {
            tutorialMsg.text = "Type in a word here to shoot it! Press space or enter to fire when you're done.";
            prompt.SetActive(false);
        }

        if (tutorialStage == 6 && !tutorialIncrementing)
        {
            tutorialMsg.text = "That one dropped a keycap, make sure to pick it up. You can unlock special abilities with them!";
            prompt.SetActive(true);
            prompt.GetComponent<PromptFlash>().StartCoroutine("Flash");
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine("IncrementTutorial", 0.1f);
            }
        }

        if (tutorialStage == 7 && !tutorialIncrementing)
        {
            tutorialMsg.text = "There are certain words of power that have special effects. The word you are currently learning is displayed here.";
            arrow.SetActive(true);
            prompt.SetActive(true);
            prompt.GetComponent<PromptFlash>().StartCoroutine("Flash");
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine("IncrementTutorial", 0.1f);
            }
        }

        if (tutorialStage == 8 && !tutorialIncrementing)
        {
            tutorialMsg.text = "Every 6 keycaps you collect will reveal another letter; you may attempt to solve it at any time by entering what you think the word is.";
            arrow.SetActive(false);
            arrow2.SetActive(true);
            prompt.SetActive(true);
            prompt.GetComponent<PromptFlash>().StartCoroutine("Flash");
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine("IncrementTutorial", 0.1f);
            }
        }

        if (tutorialStage == 9)
        {
            tutorialMsg.text = "... Okay fine, I'll give you this first one. It's \"" + player.GetComponent<Typing>().wordToBeUnlocked + "\"";
            arrow2.SetActive(false);
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

        if (tutorialStage == 12)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = mainTheme;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
            tutorialMsg.text = "";
            tutorial = false;
            Typing.maxSlowDown = 2.25f;
            trackball.SetActive(false);
            prompt.SetActive(false);
            skipButton.SetActive(false);
        }
    }

    public void ManageCooldowns()
    {
        foreach(Word w in Words)
        {
            if(w.CurCool > 0)
            {
                w.CurCool -= Time.deltaTime;
            }
        }
    }

    public void RemoveWords()
    {
        foreach(Word w in Words)
        {
            w.Unlocked = false;
        }
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);

        if (!meme.activeSelf)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
        }      
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Level = 1;
        LevelTracker.LevelToLoad = 0;
        SceneManager.LoadScene(4);
    }

    public void QTD()
    {
        Application.Quit();
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
        player.GetComponent<Typing>().UnlockWord(player.GetComponent<Typing>().wordToBeUnlocked);
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
        lives = 6;
        keycaps = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
