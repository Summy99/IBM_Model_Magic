using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private AudioClip shot, wordSuccess, wordFail, enterSlow, bomb, bigBomb, wordUnlocked;

    [SerializeField]
    private AudioClip badAppleEarrape;

    [SerializeField]
    private GameObject letterPrefab, explosionPrefab;

    [SerializeField]
    private GameObject mema;

    [SerializeField]
    public Sprite[] letterSprites;

    [SerializeField]
    public Sprite yukkuriReimu, yukkuriFlan;

    public float shootCooldown = 0.1f;
    private float curShootCooldown = 0;
    public float slowRecoveryRate = 0.1f;
    private bool patternRunning = false;

    public string mode = "shooting";
    public int letterProgress = 0;
    public float slowDown = 5;
    public static float maxSlowDown = 2.25f;
    public string word = "";
    public string wordToBeUnlocked = "";
    public bool touhou = false;

    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        ui = GameObject.FindWithTag("GameController").GetComponent<UI>();
        bml = gameObject.GetComponent<BulletSourceScript>();
        sfx = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(wordToBeUnlocked == "")
        {
            PickFirstWord();
            
            if(GameController.Level != 1)
            {
                UnlockWord(wordToBeUnlocked);
            }
        }

        sfx.volume = 0.5f * MenuScript.MasterVolume * MenuScript.SFXVolume;

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

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject g in enemies)
            {
                Destroy(g);
            }
        }

        if (gc.tutorial)
        {
            if(gc.tutorialStage == 4 && slowDown <= 0)
            {
                slowDown = maxSlowDown;
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
                ConfirmWord();
            }
        }

        if(slowDown < maxSlowDown && mode == "shooting" && !gc.paused)
        {
            slowDown += Time.deltaTime * slowRecoveryRate * maxSlowDown;
        }

        ShootingInput();

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Tab)) && mode == "shooting" && !gameObject.GetComponent<PlayerHealth>().shield && !gc.paused)
        {
            sfx.PlayOneShot(enterSlow);
            mode = "typing";
        }

        else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && mode == "typing" && !gc.paused)
        {
            ConfirmWord();
        }

        if (mode == "typing" && !gc.paused)
        {
            Time.timeScale = 0.2f;
        }

        if (mode == "shooting" && !gc.paused)
        {
            Time.timeScale = 1f;
        }

        if (mode == "typing" && slowDown > 0 && !gc.paused)
        {
            slowDown -= Time.unscaledDeltaTime;
        }

        if(curShootCooldown > 0 && !gc.paused)
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
                word = word.Substring(0, word.Length - 1);
            }

            if (Input.GetKeyDown(KeyCode.A) && gc.letters[0] && word.Length < 22)
            {
                word += "A";
            }

            if (Input.GetKeyDown(KeyCode.B) && gc.letters[1] && word.Length < 22)
            {
                word += "B";
            }

            if (Input.GetKeyDown(KeyCode.C) && gc.letters[2] && word.Length < 22)
            {
                word += "C";
            }

            if (Input.GetKeyDown(KeyCode.D) && gc.letters[3] && word.Length < 22)
            {
                word += "D";
            }

            if (Input.GetKeyDown(KeyCode.E) && gc.letters[4] && word.Length < 22)
            {
                word += "E";
            }

            if (Input.GetKeyDown(KeyCode.F) && gc.letters[5] && word.Length < 22)
            {
                word += "F";
            }

            if (Input.GetKeyDown(KeyCode.G) && gc.letters[6] && word.Length < 22)
            {
                word += "G";
            }

            if (Input.GetKeyDown(KeyCode.H) && gc.letters[7] && word.Length < 22)
            {
                word += "H";
            }

            if (Input.GetKeyDown(KeyCode.I) && gc.letters[8] && word.Length < 22)
            {
                word += "I";
            }

            if (Input.GetKeyDown(KeyCode.J) && gc.letters[9] && word.Length < 22)
            {
                word += "J";
            }

            if (Input.GetKeyDown(KeyCode.K) && gc.letters[10] && word.Length < 22)
            {
                word += "K";
            }

            if (Input.GetKeyDown(KeyCode.L) && gc.letters[11] && word.Length < 22)
            {
                word += "L";
            }

            if (Input.GetKeyDown(KeyCode.M) && gc.letters[12] && word.Length < 22)
            {
                word += "M";
            }

            if (Input.GetKeyDown(KeyCode.N) && gc.letters[13] && word.Length < 22)
            {
                word += "N";
            }

            if (Input.GetKeyDown(KeyCode.O) && gc.letters[14] && word.Length < 22)
            {
                word += "O";
            }

            if (Input.GetKeyDown(KeyCode.P) && gc.letters[15] && word.Length < 22)
            {
                word += "P";
            }

            if (Input.GetKeyDown(KeyCode.Q) && gc.letters[16] && word.Length < 22)
            {
                word += "Q";
            }

            if (Input.GetKeyDown(KeyCode.R) && gc.letters[17] && word.Length < 22)
            {
                word += "R";
            }

            if (Input.GetKeyDown(KeyCode.S) && gc.letters[18] && word.Length < 22)
            {
                word += "S";
            }

            if (Input.GetKeyDown(KeyCode.T) && gc.letters[19] && word.Length < 22)
            {
                word += "T";
            }

            if (Input.GetKeyDown(KeyCode.U) && gc.letters[20] && word.Length < 22)
            {
                word += "U";
            }

            if (Input.GetKeyDown(KeyCode.V) && gc.letters[21] && word.Length < 22)
            {
                word += "V";
            }

            if (Input.GetKeyDown(KeyCode.W) && gc.letters[22] && word.Length < 22)
            {
                word += "W";
            }

            if (Input.GetKeyDown(KeyCode.X) && gc.letters[23] && word.Length < 22)
            {
                word += "X";
            }

            if (Input.GetKeyDown(KeyCode.Y) && gc.letters[24] && word.Length < 22)
            {
                word += "Y";
            }

            if (Input.GetKeyDown(KeyCode.Z) && gc.letters[25] && word.Length < 22)
            {
                word += "Z";
            }
        }
    }

    private void ConfirmWord()
    {
        int wordIndex = GetWordIndex(word);

        if (gc.tutorial && gc.tutorialStage == 7)
        {
            if(word == "AUTO")
            {
                //GameController.words[word] = true;
                ui.AddWord(word);
                gc.tutorialStage = 8;
            }
            else
            {
                return;
            }
        }

        if (gc.tutorial && gc.tutorialStage == 9)
        {
            if (word == "SHIELD")
            {
                //GameController.words[word] = true;
                ui.AddWord(word);
                gc.tutorialStage = 10;
            }
            else
            {
                return;
            }
        }

        if ((wordIndex != 0 && (GameController.Words[wordIndex].Unlocked || word == wordToBeUnlocked) && GameController.Words[wordIndex].CurCool <= 0) || word == "TOUHOU" || word == "MIMA")
        {
            sfx.PlayOneShot(wordSuccess, 0.5f);

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

                case "SHOOT":
                    if (true)
                    {
                        AutoFire();
                    }
                    break;

                case "FIRE":
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

                case "SCATTER":
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

                case "GRAB":
                    Collect();
                    break;

                case "COLLECT":
                    Collect();
                    break;

                case "TOUHOU":
                    TouhouMeme();
                    break;

                case "MIMA":
                    Memea();
                    break;
            }

            if(word == wordToBeUnlocked)
            {
                UnlockWord(word);
            }
            else if(word != "MIMA" && word != "TOUHOU")
            {
                GameController.Words[wordIndex].CurCool = GameController.Words[wordIndex].MaxCool;
            }
        }

        else if (wordIndex == 0 || !GameController.Words[wordIndex].Unlocked || GameController.Words[wordIndex].CurCool > 0)
        {
            sfx.PlayOneShot(wordFail);

            string[] letters = ToStringArray(word);
            int[] lettersToShoot = new int[letters.Length];

            for (int i = 0; i < letters.Length; i++)
            {
                switch (letters[i])
                {
                    case "A":
                        lettersToShoot[i] = 0;
                        break;

                    case "B":
                        lettersToShoot[i] = 1;
                        break;

                    case "C":
                        lettersToShoot[i] = 2;
                        break;

                    case "D":
                        lettersToShoot[i] = 3;
                        break;

                    case "E":
                        lettersToShoot[i] = 4;
                        break;

                    case "F":
                        lettersToShoot[i] = 5;
                        break;

                    case "G":
                        lettersToShoot[i] = 6;
                        break;

                    case "H":
                        lettersToShoot[i] = 7;
                        break;

                    case "I":
                        lettersToShoot[i] = 8;
                        break;

                    case "J":
                        lettersToShoot[i] = 9;
                        break;

                    case "K":
                        lettersToShoot[i] = 10;
                        break;

                    case "L":
                        lettersToShoot[i] = 11;
                        break;

                    case "M":
                        lettersToShoot[i] = 12;
                        break;

                    case "N":
                        lettersToShoot[i] = 13;
                        break;

                    case "O":
                        lettersToShoot[i] = 14;
                        break;

                    case "P":
                        lettersToShoot[i] = 15;
                        break;

                    case "Q":
                        lettersToShoot[i] = 16;
                        break;

                    case "R":
                        lettersToShoot[i] = 17;
                        break;

                    case "S":
                        lettersToShoot[i] = 18;
                        break;

                    case "T":
                        lettersToShoot[i] = 19;
                        break;

                    case "U":
                        lettersToShoot[i] = 20;
                        break;

                    case "V":
                        lettersToShoot[i] = 21;
                        break;

                    case "W":
                        lettersToShoot[i] = 22;
                        break;

                    case "X":
                        lettersToShoot[i] = 23;
                        break;

                    case "Y":
                        lettersToShoot[i] = 24;
                        break;

                    case "Z":
                        lettersToShoot[i] = 25;
                        break;
                }
            }

            StartCoroutine("ShootLetters", lettersToShoot);
        }

        mode = "shooting";
        word = "";
    }

    public void UnlockWord(string uWord)
    {
        sfx.PlayOneShot(wordUnlocked);

        GameController.Words[GetWordIndex(uWord)].Unlocked = true;
        ui.AddWord(uWord);

        PickNewWord();
    }

    public void PickNewWord()
    {
        int newWord = Mathf.FloorToInt(Random.Range(1, GameController.Words.Count));

        while (GameController.Words[newWord].Unlocked)
        {
            newWord = Mathf.FloorToInt(Random.Range(1, GameController.Words.Count));
        }

        if (!GameController.Words[newWord].Unlocked)
        {
            print(GameController.Words[newWord].Name);
            wordToBeUnlocked = GameController.Words[newWord].Name;
            ui.UpdateWordUnlock();
        }
    }

    public void PickFirstWord()
    {
        int newWord = Mathf.FloorToInt(Random.Range(1, GameController.Words.Count));

        while(GameController.Words[newWord].Name == "COLLECT" ||
              GameController.Words[newWord].Name == "GRAB" ||
              GameController.Words[newWord].Name == "FAST" ||
              GameController.Words[newWord].Name == "RAPID" ||
              GameController.Words[newWord].Name == "SPEED" ||
              GameController.Words[newWord].Name == "SHIELD" ||
              GameController.Words[newWord].Name == "BLOCK" ||
              GameController.Words[newWord].Name == "CLEAR" ||
              GameController.Words[newWord].Name == "ERASE")
        {
            newWord = Mathf.FloorToInt(Random.Range(1, GameController.Words.Count));
        }

        wordToBeUnlocked = GameController.Words[newWord].Name;
        ui.UpdateWordUnlock();
    }

    private void Shoot(int letter)
    {
        sfx.PlayOneShot(shot);
        GameObject l = Instantiate(letterPrefab, gameObject.transform.Find("letterspawn").position, Quaternion.identity);
        l.GetComponent<SpriteRenderer>().sprite = letterSprites[letter];
        Destroy(l, 2);
    }

    private IEnumerator ShootLetters(int[] letters)
    {
        foreach(int i in letters)
        {
            Shoot(i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public string[] ToStringArray(string s)
    {
        string[] a = new string[s.Length];

        for(int i = 0; i < a.Length; i++)
        {
            a[i] = s[i].ToString();
        }

        return a;
    }

    public int GetWordIndex(string word)
    {
        for(int i = 0; i < GameController.Words.Count; i++)
        {
            if(GameController.Words[i].Name == word)
            {
                return i;
            }
        }

        return 0;
    }

    public bool ContainsWord(List<Word> l, string word)
    {
        foreach(Word w in l)
        {
            if(w.Name == word)
            {
                return true;
            }
        }

        return false;
    }

    private void TouhouMeme()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = yukkuriReimu;
        touhou = true;

        GameObject[] e = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject g in e)
        {
            if (g.GetComponent<SpriteRenderer>())
            {
                g.GetComponent<SpriteRenderer>().sprite = yukkuriFlan;
            }
            else
            {
                g.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = yukkuriFlan;
            }
        }

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = badAppleEarrape;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
    }

    private void Memea()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();
        mema.SetActive(true);
    }

    private void Bomb()
    {
        sfx.PlayOneShot(bomb);
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
        bml.xmlFile = patterns[0];
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
        sfx.PlayOneShot(bigBomb);

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

    private void Collect()
    {
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");

        foreach(GameObject c in collectibles)
        {
            c.GetComponent<Collectibles>().activated = true;
        }
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

            case 8:
                Collect();
                break;
        }
    }
}