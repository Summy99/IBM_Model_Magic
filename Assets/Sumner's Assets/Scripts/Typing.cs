using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelnest.BulletML;

public class Typing : MonoBehaviour
{
    [SerializeField]
    public TextAsset[] patterns;

    private BulletSourceScript bml;
    private GameController gc;
    private AudioSource sfx;
    private UI ui;

    [SerializeField]
    private AudioClip shot, wordSuccess, wordFail, enterSlow;

    [SerializeField]
    private GameObject letterPrefab;

    [SerializeField]
    public Sprite[] letterSprites;

    public float shootCooldown = 0.1f;
    private float curShootCooldown = 0;
    public float slowRecoveryRate = 0.1f;
    private bool patternRunning = false;

    public string mode = "shooting";
    public float slowDown = 5;
    public float maxSlowDown = 3;
    public string word = "";

    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        ui = GameObject.FindWithTag("GameController").GetComponent<UI>();
        bml = gameObject.GetComponent<BulletSourceScript>();
        sfx = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            maxSlowDown -= 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            maxSlowDown += 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            slowDown += 0.1f * maxSlowDown;
        }

        if (gc.tutorial)
        {
            if(gc.tutorialStage == 2 && slowDown <= 0)
            {
                slowDown = maxSlowDown;
            }

            if(gc.tutorialStage == 2 && word == "AUTO")
            {
                gc.tutorialStage = 3;
            }
        }

        if (!gc.tutorial)
        {
            if (bml.IsEnded)
            {
                patternRunning = false;
            }

            if (slowDown > maxSlowDown)
            {
                slowDown = maxSlowDown;
            }

            if (mode == "typing" && slowDown <= 0)
            {
                word = "";
                mode = "shooting";
            }
        }

        if(slowDown < maxSlowDown && mode == "shooting")
        {
            slowDown += Time.deltaTime * slowRecoveryRate * maxSlowDown;
        }

        ShootingInput();

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Tab)) && mode == "shooting")
        {
            sfx.PlayOneShot(enterSlow);
            mode = "typing";
        }

        else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && mode == "typing")
        {
            ConfirmWord();
        }

        if (mode == "typing")
        {
            Time.timeScale = 0.2f;
        }

        if (mode == "shooting")
        {
            Time.timeScale = 1f;
        }

        if (mode == "typing" && slowDown > 0)
        {
            slowDown -= Time.unscaledDeltaTime;
        }

        if(curShootCooldown > 0)
        {
            curShootCooldown -= Time.deltaTime;
        }
    }

    private void ShootingInput()
    {
        if(mode == "typing")
        {
            if (Input.GetKeyDown(KeyCode.Backspace) && word.Length >= 1)
            {
                print("test");
                word = word.Substring(0, word.Length - 1);
            }

            if (Input.GetKeyDown(KeyCode.A) && gc.letters[0])
            {
                word += "A";
            }

            if (Input.GetKeyDown(KeyCode.B) && gc.letters[1])
            {
                word += "B";
            }

            if (Input.GetKeyDown(KeyCode.C) && gc.letters[2])
            {
                word += "C";
            }

            if (Input.GetKeyDown(KeyCode.D) && gc.letters[3])
            {
                word += "D";
            }

            if (Input.GetKeyDown(KeyCode.E) && gc.letters[4])
            {
                word += "E";
            }

            if (Input.GetKeyDown(KeyCode.F) && gc.letters[5])
            {
                word += "F";
            }

            if (Input.GetKeyDown(KeyCode.G) && gc.letters[6])
            {
                word += "G";
            }

            if (Input.GetKeyDown(KeyCode.H) && gc.letters[7])
            {
                word += "H";
            }

            if (Input.GetKeyDown(KeyCode.I) && gc.letters[8])
            {
                word += "I";
            }

            if (Input.GetKeyDown(KeyCode.J) && gc.letters[9])
            {
                word += "J";
            }

            if (Input.GetKeyDown(KeyCode.K) && gc.letters[10])
            {
                word += "K";
            }

            if (Input.GetKeyDown(KeyCode.L) && gc.letters[11])
            {
                word += "L";
            }

            if (Input.GetKeyDown(KeyCode.M) && gc.letters[12])
            {
                word += "M";
            }

            if (Input.GetKeyDown(KeyCode.N) && gc.letters[13])
            {
                word += "N";
            }

            if (Input.GetKeyDown(KeyCode.O) && gc.letters[14])
            {
                word += "O";
            }

            if (Input.GetKeyDown(KeyCode.P) && gc.letters[15])
            {
                word += "P";
            }

            if (Input.GetKeyDown(KeyCode.Q) && gc.letters[16])
            {
                word += "Q";
            }

            if (Input.GetKeyDown(KeyCode.R) && gc.letters[17])
            {
                word += "R";
            }

            if (Input.GetKeyDown(KeyCode.S) && gc.letters[18])
            {
                word += "S";
            }

            if (Input.GetKeyDown(KeyCode.T) && gc.letters[19])
            {
                word += "T";
            }

            if (Input.GetKeyDown(KeyCode.U) && gc.letters[20])
            {
                word += "U";
            }

            if (Input.GetKeyDown(KeyCode.V) && gc.letters[21])
            {
                word += "V";
            }

            if (Input.GetKeyDown(KeyCode.W) && gc.letters[22])
            {
                word += "W";
            }

            if (Input.GetKeyDown(KeyCode.X) && gc.letters[23])
            {
                word += "X";
            }

            if (Input.GetKeyDown(KeyCode.Y) && gc.letters[24])
            {
                word += "Y";
            }

            if (Input.GetKeyDown(KeyCode.Z) && gc.letters[25])
            {
                word += "Z";
            }
        }
    }

    private void ConfirmWord()
    {
        if (gc.tutorial && gc.tutorialStage == 3)
        {
            if(word == "AUTO")
            {
                gc.tutorialStage = 4;
            }
            else
            {
                return;
            }
        }

        switch (word)
        {
            case "A":
                Shoot(0);
                break;

            case "B":
                Shoot(1);
                break;

            case "C":
                Shoot(2);
                break;

            case "D":
                Shoot(3);
                break;

            case "E":
                Shoot(4);
                break;

            case "F":
                Shoot(5);
                break;

            case "G":
                Shoot(6);
                break;

            case "H":
                Shoot(7);
                break;

            case "I":
                Shoot(8);
                break;

            case "J":
                Shoot(9);
                break;

            case "K":
                Shoot(10);
                break;

            case "L":
                Shoot(11);
                break;

            case "M":
                Shoot(12);
                break;

            case "N":
                Shoot(13);
                break;

            case "O":
                Shoot(14);
                break;

            case "P":
                Shoot(15);
                break;

            case "Q":
                Shoot(16);
                break;

            case "R":
                Shoot(17);
                break;

            case "S":
                Shoot(18);
                break;

            case "T":
                Shoot(19);
                break;

            case "U":
                Shoot(20);
                break;

            case "V":
                Shoot(21);
                break;

            case "W":
                Shoot(22);
                break;

            case "X":
                Shoot(23);
                break;

            case "Y":
                Shoot(24);
                break;

            case "Z":
                Shoot(25);
                break;

            case "BOMB":
                Bomb();
                break;

            case "BOOM":
                Bomb();
                break;

            case "SHIELD":
                StartCoroutine("Shield");
                break;

            case "ERASE":
                Erase();
                break;

            case "CLEAR":
                Erase();
                break;

            case "AUTO":
                if (true)
                {
                    AutoFire();
                }
                break;

            case "SPREAD":
                if (true)
                {
                    SpreadShot();
                }
                break;
        }

        if (gc.words.ContainsKey(word) && !gc.words[word])
        {
            gc.words[word] = true;
            ui.AddWord(word);
        }

        if (!gc.words.ContainsKey(word))
        {
            sfx.PlayOneShot(wordFail);
        }
        else
        {
            sfx.PlayOneShot(wordSuccess);
        }

        mode = "shooting";
        word = "";
    }

    private void Shoot(int letter)
    {
        sfx.PlayOneShot(shot);
        GameObject l = Instantiate(letterPrefab, gameObject.transform.Find("letterspawn").position, Quaternion.identity);
        l.GetComponent<SpriteRenderer>().sprite = letterSprites[letter];
        Destroy(l, 2);
    }

    private void Bomb()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().TakeDamage(10);
        }
    }

    private void Erase()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }
    }

    private IEnumerator Shield()
    {
        gameObject.transform.Find("Shield").gameObject.SetActive(true);
        gameObject.GetComponent<PlayerHealth>().shield = true;
        yield return new WaitForSeconds(3);
        gameObject.transform.Find("Shield").gameObject.SetActive(false);
        gameObject.GetComponent<PlayerHealth>().shield = false;
    }

    private void AutoFire()
    {
        patternRunning = true;
        bml.xmlFile = patterns[1];
        bml.Initialize();
    }

    private void SpreadShot()
    {
        patternRunning = true;
        bml.xmlFile = patterns[2];
        bml.Initialize();
    }
}
