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
    private GameObject letterPrefab, explosionPrefab;

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
            case "BOMB":
                Bomb();
                break;

            case "BOOM":
                Bomb();
                break;

            case "BLOCK":
                StartCoroutine("Shield");
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

            case "SHOTGUN":
                if (true)
                {
                    SpreadShot();
                }
                break;

            case "RAPID":
                StartCoroutine("Speed");
                break;

            case "SPEED":
                StartCoroutine("Speed");
                break;

            case "FAST":
                StartCoroutine("Speed");
                break;

            case "OTHER":
                RandomWord();
                break;

            case "SOMETHING":
                RandomWord();
                break;

            case "RANDOM":
                RandomWord();
                break;

            case "ANYTHING":
                RandomWord();
                break;

            case "EXPLOSION":
                ExplosionWord();
                break;

            case "BIG":
                BigShot();
                break;

            case "GIANT":
                BigShot();
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
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
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
        yield return new WaitForSeconds(2.5f);
        gameObject.transform.Find("Shield").gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.Find("Shield").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.Find("Shield").gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.Find("Shield").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
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

    private IEnumerator Speed()
    {
        gameObject.GetComponent<PlayerMovement>().moveSpeed = 100f;
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<PlayerMovement>().moveSpeed = 50f;
    }

    private void ExplosionWord()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject e in enemies)
        {
            e.GetComponent<EnemyController>().TakeDamage(10);
        }
    }

    private void BigShot()
    {
        patternRunning = true;
        bml.xmlFile = patterns[3];
        bml.Initialize();
    }

    private void RandomWord()
    {
        int effect = Mathf.FloorToInt(Random.Range(0, 8));

        switch (effect)
        {
            case 0:
                Bomb();
                break;

            case 1:
                StartCoroutine("Shield");
                break;

            case 2:
                Erase();
                break;

            case 3:
                AutoFire();
                break;

            case 4:
                SpreadShot();
                break;

            case 5:
                StartCoroutine("Speed");
                break;

            case 6:
                ExplosionWord();
                break;

            case 7:
                BigShot();
                break;
        }
    }
}