﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public bool dead = false;
    private bool starterWord = true;
    public int tutorialStage = 0;
    public static int keycaps = 0;

    private GameObject player;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TextMeshProUGUI tutorialMsg;
    [SerializeField] private Transform spawn;
    [SerializeField] private GameObject pauseMenu, settingsMenu, gameOver;
    [SerializeField] private GameObject meme;
    [SerializeField] private GameObject tutorialEnemy, trackball, prompt, skipButton, arrow, arrow2;
    [SerializeField] private AudioClip mainTheme, death;
    [SerializeField] private Slider master, music, sfx;

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
                    MaxCool = 5f
                   },

        new Word() {
                    Name = "SHIELD",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 5f
                   },

        new Word() {
                    Name = "CLEAR",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 20f
                   },

        new Word() {
                    Name = "ERASE",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 20f
                   },

        new Word() {
                    Name = "BOOM",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 5f
                   },

        new Word() {
                    Name = "AUTO",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
                   },

        new Word() {
                    Name = "SHOOT",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
                   },

        new Word() {
                    Name = "FIRE",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
                   },

        new Word() {
                    Name = "SPREAD",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 10f
                   },

        new Word() {
                    Name = "SCATTER",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 10f
                   },

        new Word() {
                    Name = "SHOTGUN",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 10f
                   },

        new Word() {
                    Name = "BLOCK",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 5f
                   },

        new Word() {
                    Name = "SOMETHING",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 5f
                   },

        new Word() {
                    Name = "RANDOM",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 5f
                   },

        new Word() {
                    Name = "EXPLOSION",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
                   },

        new Word() {
                    Name = "BIG",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
                   },

        new Word() {
                    Name = "GIANT",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
                   },

        new Word() {
                    Name = "HOMING",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
                   },

        new Word() {
                    Name = "SEARCH",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
                   },

        new Word() {
                    Name = "TRACKING",
                    Unlocked = false,
                    CurCool = 0f,
                    MaxCool = 15f
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

        master.value = MenuScript.MasterVolume;
        sfx.value = MenuScript.SFXVolume;
        music.value = MenuScript.MusicVolume;

        if (Level == 1)
        {
            if (!UI.started)
            {
                UI.started = true;

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
                starterWord = false;
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

        if (!starterWord && Level == 1 && player.GetComponent<Typing>().wordToBeUnlocked != "")
        {
            player.GetComponent<Typing>().UnlockWord(player.GetComponent<Typing>().wordToBeUnlocked, false);
            starterWord = true;
        }

        if(tutorial)
        {
            Tutorial();
        }

        ManageCooldowns();

        if(Input.GetKeyDown(KeyCode.Return) && dead)
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && player.GetComponent<Typing>().mode == "shooting" && !dead)
        {
            if (paused)
            {
                if (!settingsMenu.activeSelf)
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
                    settingsMenu.SetActive(false);
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
        settingsMenu.SetActive(true);
    }

    public void Quit()
    {
        Level = 1;
        lives = 6;
        keycaps = 0;
        RemoveWords();
        UI.started = false;
        LevelTracker.LevelToLoad = 0;
        SceneManager.LoadScene(4);
    }

    public void QTD()
    {
        Application.Quit();
    }

    public void Back()
    {
        settingsMenu.SetActive(false);
    }

    public void FullscreenToggle()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1024, 768, FullScreenMode.Windowed);
        }
        else
        {
            Screen.SetResolution(1920, 1440, FullScreenMode.FullScreenWindow);
        }
    }

    public void UpdateVolume()
    {
        MenuScript.MasterVolume = master.value;
        MenuScript.SFXVolume = sfx.value;
        MenuScript.MusicVolume = music.value;
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
        player.GetComponent<Typing>().UnlockWord(player.GetComponent<Typing>().wordToBeUnlocked, false);
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
        dead = true;
        Destroy(player);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(death);
        gameOver.SetActive(true);
    }

    public void Restart()
    {
        RemoveWords();
        dead = false;
        gameOver.SetActive(false);
        lives = 6;
        keycaps = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
