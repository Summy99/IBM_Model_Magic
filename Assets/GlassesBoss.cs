using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Pixelnest.BulletML;
using TMPro;

public class GlassesBoss : MonoBehaviour
{
    private GameObject head, rHand, lHand, rHandPivot, lHandPivot;

    private Rigidbody2D headRB, rHandRB, lHandRB;
    private AnimationManager headAnim, rHandAnim, lHandAnim;
    private BulletSourceScript headSource, rHandSource, lHandSource;
    private AudioSource sfx;
    private Image healthBarR, healthBarL, healthBarMain;
    private TextMeshProUGUI attackWordUI;

    private CircleCollider2D playerCol;
    private PolygonCollider2D[] colsR, colsL; 

    [SerializeField]
    private TextAsset[] patterns;

    [SerializeField]
    private AudioClip intro, loop, hit, headHit, death;

    public Animation[] animations;
    public string[] names;
    private List<Coroutine> coroutinesR = new List<Coroutine>();
    private List<Coroutine> coroutinesL = new List<Coroutine>();

    [SerializeField]
    private Sprite[] enterFrames, idleFrames, handIdleFrames, handWarmUpFrames, grinFrames, gunFrames, palmFrames, handDyingFrames, headVulnFrames, headShockFrames, headDamageFrames;

    [SerializeField]
    private Sprite[] mainHealthBarSprites;

    private bool started = false, entered = false, handsIn = false, switchingR = false, switchingL = false, deadR = false, deadL = false,
                 spinningR = false, spinningL = false, attackStarted = false, flashingR = false, flashingL = false, headVulnearble = false,
                 SAPR = false, SAPL = false, BCR =false, BCL = false;

    public bool curtain = false;
    public bool activated = false;
    private int headHP = 0;
    public float rHandHP = 0, lHandHP = 0;
    private float handsRate = 0.01f;
    public string attackWord = "";
    public readonly string[] attackWords = new string[] {"IDENTICAL", "CHOCOLATE", "BEAUTIFUL",  "HAPPINESS", "WEDNESDAY", "CHALLENGE", "CELEBRATE", "ADVENTURE",
        "IMPORTANT", "CONSONANT", "DANGEROUS", "KNOWLEDGE", "POLLUTION", "PINEAPPLE", "ADJECTIVE", "UNDEFINED", "AFFECTION", "CONGRUENT", "ALLIGATOR", "AMBULANCE",
        "COMMUNITY", "DIFFERENT", "VEGETABLE", "INFLUENCE", "STRUCTURE", "INVISIBLE", "WONDERFUL", "PACKAGING", "PROVOKING", "NUTRITION", "CROCODILE", "EDUCATION",
        "ABOUNDING", "BEGINNING", "BOULEVARD", "WITHERING", "BREATHING", "SOPHOMORE", "SEPTEMBER", "WORTHLESS", "IMPERFECT", "BREAKFAST", "XYLOPHONE", "HAMBURGER",
        "INTEGRITY", "CHARACTER", "BLESSINGS", "ADVERSITY", "CHARLOTTE", "CONFUSION", "ABDUCTING", "AFTERLIFE", "SUFFERING", "EVERYBODY", "CURIOSITY", "LOUISIANA",
        "CELEBRITY", "DELICIOUS", "TURQUOISE", "ATTENTION", "COMPANION", "ELOCUTION", "WHIMSICAL", "DIFFICULT", "AGITATION", "NECESSARY", "LIGHTNING", "CHEMISTRY",
        "RECYCLING", "TREATMENT", "SPAGHETTI", "BILLBOARD", "AGREEMENT", "TERRITORY", "AMENDMENT", "ARCHITECT", "FLEDGLING", "ECOSYSTEM", "MAGNESIUM", "TWENTIETH",
        "DECEPTION", "CARIBBEAN", "GENERATOR", "JEFFERSON", "PERIMETER", "AMPHIBIAN", "ADDICTION", "RADIATION", "ORANGUTAN", "INNOCENCE", "DANDELION", "NIGHTMARE",
        "COMMODITY", "ABUNDANCE", "DIRECTION", "DIVERGENT", "REFERENCE", "SUNFLOWER", "AUTHORITY", "ABDUCTION", "MOUSTACHE", "INCEPTION", "FIREWORKS", "HAPPENING",
        "AWARENESS", "HURRICANE", "LISTENING", "PREJUDICE", "PRESCHOOL", "CRITICISM", "TRADITION", "SCORCHING", "PROFESSOR", "CHAMELEON", "GATHERING", "ANONYMOUS",
        "SCIENTIST", "ASTRONAUT", "ACCORDION", "BRILLIANT", "EMPTINESS", "FANTASTIC", "AWAKENING", "CLEVELAND", "TANGERINE", "LEGENDARY", "WATERFALL", "DEDICATED",
        "INJUSTICE", "ADMIRABLE", "JELLYFISH", "BALLISTIC", "BUTTERFLY", "FORGOTTEN", "SLEEPOVER", "TREASURER", "SANCTUARY", "SIGNATURE", "SHRIEKING", "FAIRYTALE",
        "MECHANISM", "SENSATION", "DESPERATE", "HOUSEWIFE", "PENINSULA", "HALITOSIS", "APHRODITE", "SAXOPHONE", "ADVERTISE", "YOUNGSTER", "CLOSENESS", "MARGARITA",
        "DISCOVERY", "ABANDONED", "SUFFOCATE", "TINKERING", "CARABINER", "COUNTDOWN", "CHAMPLAIN", "DEMOCRACY", "REARRANGE", "ELEVATION", "ABORIGINE", "ANIMATION"};

    private Transform rHandStart;

    private void Start()
    {
        head = transform.Find("Head").gameObject;
        rHandPivot = transform.Find("RightHandPivot").gameObject;
        lHandPivot = transform.Find("LeftHandPivot").gameObject;

        rHand = rHandPivot.transform.Find("RightHand").gameObject;
        lHand = lHandPivot.transform.Find("LeftHand").gameObject;

        headAnim = head.GetComponent<AnimationManager>();
        rHandAnim = rHand.GetComponent<AnimationManager>();
        lHandAnim = lHand.GetComponent<AnimationManager>();

        headRB = head.GetComponent<Rigidbody2D>();
        rHandRB = rHand.GetComponent<Rigidbody2D>();
        lHandRB = lHand.GetComponent<Rigidbody2D>();

        headSource = head.GetComponent<BulletSourceScript>();
        rHandSource = rHand.transform.Find("RHSource").GetComponent<BulletSourceScript>();
        lHandSource = lHand.transform.Find("LHSource").GetComponent<BulletSourceScript>();

        attackWordUI = head.transform.Find("Canvas").Find("word").GetComponent<TextMeshProUGUI>();

        healthBarMain = head.transform.Find("Canvas").Find("hp").GetComponent<Image>();
        healthBarR = rHand.transform.Find("Canvas").Find("HealthBar").GetComponent<Image>();
        healthBarL = lHand.transform.Find("Canvas").Find("HealthBar").GetComponent<Image>();

        playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();
        sfx = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        colsR = rHand.GetComponents<PolygonCollider2D>();
        colsL = lHand.GetComponents<PolygonCollider2D>();

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = intro;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().loop = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();

        animations = new Animation[]
        {
            new Animation()
            {
                Frames = enterFrames,
                Speed = 0.2f,
                Looping = false
            },
            new Animation()
            {
                Frames = idleFrames,
                Speed = 0.2f,
                Looping = true
            },
            new Animation()
            {
                Frames = handIdleFrames,
                Speed = 0.2f,
                Looping = true
            },
            new Animation()
            {
                Frames = handWarmUpFrames,
                Speed = 0.2f,
                Looping = false
            },
            new Animation()
            {
                Frames = grinFrames,
                Speed = 0.2f,
                Looping = true
            },
            new Animation()
            {
                Frames = gunFrames,
                Speed = 0.2f,
                Looping = true
            },
            new Animation()
            {
                Frames = palmFrames,
                Speed = 0.2f,
                Looping = true
            },
            new Animation()
            {
                Frames = handDyingFrames,
                Speed = 0.2f,
                Looping = true
            },
            new Animation()
            {
                Frames = headVulnFrames,
                Speed = 0.2f,
                Looping = true
            },
            new Animation()
            {
                Frames = headShockFrames,
                Speed = 0.2f,
                Looping = true
            },
            new Animation()
            {
                Frames = headDamageFrames,
                Speed = 0.2f,
                Looping = true
            },
        };
        names = new string[] { "enter", "idle", "handIdle", "handWarmUp", "grin", "gun", "palm", "handDying", "headVuln", "headShock", "headDamage" };

        headAnim.PopulateDictionary(names, animations);
        rHandAnim.PopulateDictionary(names, animations);
        lHandAnim.PopulateDictionary(names, animations);

        rHandAnim.PlayAnimation("handIdle");
        ManageColliders("r", 0);
        lHandAnim.PlayAnimation("handIdle");
        ManageColliders("l", 0);

        rHandHP = 0;
        lHandHP = 0;

        StartCoroutine("Initiate");
    }

    private void Update()
    {
        rHand.transform.Find("Flash").GetComponent<SpriteRenderer>().sprite = rHand.GetComponent<SpriteRenderer>().sprite;
        lHand.transform.Find("Flash").GetComponent<SpriteRenderer>().sprite = lHand.GetComponent<SpriteRenderer>().sprite;

        attackWordUI.text = attackWord;

        healthBarR.fillAmount = rHandHP / 125;
        healthBarL.fillAmount = lHandHP / 125;
        healthBarMain.sprite = mainHealthBarSprites[headHP];

        if (transform.position.y < 27.5 && !entered)
        {
            entered = true;
            StartCoroutine(HealHead());
            headAnim.PlayAnimation("enter");
        }

        if(!headAnim.isAnimating && headAnim.curAnim == animations[0])
        {
            headAnim.PlayAnimation("idle");
        }

        if(Vector3.Distance(head.transform.position, rHand.transform.position) <= 10 && Vector3.Distance(head.transform.position, lHand.transform.position) <= 10 && !handsIn)
        {
            handsIn = true;
            StartCoroutine("WarmHands");
        }

        if(!GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().isPlaying && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip == intro)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = loop;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().loop = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
        }

        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(ShootAtPlayer("r"));
            }

            if (rHandSource.IsEnded && !switchingR && !spinningR && !deadR)
            {
                coroutinesR.Add(StartCoroutine(RotateHand(rHandPivot, 360, 0.5f)));
                rHand.transform.rotation = Quaternion.identity;
                rHandSource.xmlFile = patterns[0];
                rHandAnim.PlayAnimation("handIdle");
                ManageColliders("r", 0);
                switchingR = true;
                coroutinesR.Add(StartCoroutine(SwitchAttacks("r")));
            }

            if (lHandSource.IsEnded && !switchingL && !spinningL && !deadL)
            {
                coroutinesL.Add(StartCoroutine(RotateHand(lHandPivot, 360, 0.5f)));
                lHand.transform.rotation = Quaternion.identity;
                lHandSource.xmlFile = patterns[0];
                lHandAnim.PlayAnimation("handIdle");
                ManageColliders("l", 0);
                switchingL = true;
                coroutinesL.Add(StartCoroutine(SwitchAttacks("l")));
            }
            //if(rHandSource.xmlFile == patterns[2])
        }

        if (attackWord == "" && headVulnearble)
        {
            PickAttackWord();
        }

        //if (Input.GetKeyDown(KeyCode.PageDown))
        //{
            //StartCoroutine(DamageHead());
        //}
    }

    private void FixedUpdate()
    {
        if (started && !entered)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }

        if(entered && !handsIn && headAnim.curAnim == animations[1])
        {
            handsRate *= 1.1f;
            rHand.transform.position = new Vector3(rHand.transform.position.x - handsRate, rHand.transform.position.y, rHand.transform.position.z);
            lHand.transform.position = new Vector3(lHand.transform.position.x + handsRate, lHand.transform.position.y, lHand.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.GetComponent<PlayerHealth>().shield && collision == playerCol)
        {
            collision.GetComponent<CircleCollider2D>().enabled = false;
            collision.GetComponent<PlayerHealth>().Die();
        }
        else if (collision.gameObject.CompareTag("Player") && collision.GetComponent<PlayerHealth>().shield && collision == playerCol)
        {
            Destroy(gameObject);
        }
    }

    private void ManageColliders(string hand, int collider)
    {
        if(hand == "r")
        {
            for (int i = 0; i < colsR.Length; i++)
            {
                if (i == collider)
                {
                    colsR[i].enabled = true;
                }
                else
                {
                    colsR[i].enabled = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < colsL.Length; i++)
            {
                if (i == collider)
                {
                    colsL[i].enabled = true;
                }
                else
                {
                    colsL[i].enabled = false;
                }
            }
        }
    }

    private IEnumerator HealHead()
    {
        while(headHP < 15)
        {
            headHP++;
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void TakeDamage(string hand, float damage)
    {
        if(hand == "r")
        {
            if (!flashingR)
            {
                StartCoroutine(Flash("r"));
            }

            rHandHP -= damage;

            sfx.PlayOneShot(hit, 0.2f);

            if (rHandHP <= 0)
            {
                StartCoroutine(KillHand("r"));
            }
        }
        else
        {
            if (!flashingL)
            {
                StartCoroutine(Flash("l"));
            }

            lHandHP -= damage;

            sfx.PlayOneShot(hit, 0.2f);

            if (lHandHP <= 0)
            {
                StartCoroutine(KillHand("l"));
            }
        }
    }

    private IEnumerator KillHand(string hand)
    {
        sfx.PlayOneShot(death);
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject b in bullets)
        {
            Destroy(b);
        }

        if (hand == "r")
        {
            deadR = true;

            foreach (Coroutine c in coroutinesR)
            {
                StopCoroutine(c);
            }
            coroutinesR.Clear();
            spinningR = false;
            switchingR = false;

            headAnim.PlayAnimation("headShock");

            if (deadL)
            {
                StartCoroutine(AttackHead());
            }

            rHandSource.xmlFile = patterns[0];

            rHandPivot.transform.rotation = Quaternion.identity;
            rHand.transform.rotation = Quaternion.identity;

            rHandAnim.PlayAnimation("handDying");

            while(rHand.GetComponent<SpriteRenderer>().color.a > 0)
            {
                rHand.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, rHand.GetComponent<SpriteRenderer>().color.a - 0.01f);
                rHand.transform.localRotation = Quaternion.Euler(0, 0, rHand.transform.localRotation.eulerAngles.z + 3);
                yield return new WaitForSeconds(0.02f);
            }

            rHandPivot.transform.localRotation = Quaternion.identity;
            rHand.transform.localRotation = Quaternion.identity;
            rHand.transform.localPosition = new Vector3(50, 0, 0);
            rHand.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            if(!(deadR && deadL))
            {
                headAnim.PlayAnimation("idle");
            }
        }
        else
        {
            deadL = true;

            foreach (Coroutine c in coroutinesL)
            {
                StopCoroutine(c);
            }
            coroutinesL.Clear();
            spinningL = false;
            switchingL = false;

            headAnim.PlayAnimation("headShock");

            lHandSource.xmlFile = patterns[0];

            if (deadR)
            {
                StartCoroutine(AttackHead());
            }

            lHandPivot.transform.rotation = Quaternion.identity;
            lHand.transform.rotation = Quaternion.identity;

            lHandAnim.PlayAnimation("handDying");

            while (lHand.GetComponent<SpriteRenderer>().color.a > 0)
            {
                lHand.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, lHand.GetComponent<SpriteRenderer>().color.a - 0.04f);
                lHand.transform.localRotation = Quaternion.Euler(0, 0, lHand.transform.localRotation.eulerAngles.z + 3);
                yield return new WaitForSeconds(0.02f);
            }

            lHandPivot.transform.localRotation = Quaternion.identity;
            lHand.transform.localRotation = Quaternion.identity;
            lHand.transform.localPosition = new Vector3(-50, 0, 0);
            lHand.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            if (!(deadR && deadL))
            {
                headAnim.PlayAnimation("idle");
            }
        }
    }

    private IEnumerator AttackHead()
    {
        activated = false;
        headVulnearble = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Typing>().finalBossAttacking = true;
        headAnim.PlayAnimation("headVuln");
        yield return new WaitForSeconds(10);
        StopCoroutine("DamageHead");
        headVulnearble = false;
        print("yo");
        attackWord = "";
        GameObject.FindGameObjectWithTag("Player").GetComponent<Typing>().finalBossAttacking = false;
        headAnim.PlayAnimation("idle");
        handsIn = false;
        deadR = false;
        deadL = false;
        handsRate = 0.01f;
    }

    private void PickAttackWord()
    {
        attackWord = attackWords[Mathf.FloorToInt(Random.Range(0, attackWords.Length))];
    }

    public IEnumerator DamageHead()
    {
        sfx.PlayOneShot(headHit);
        headHP--;

        if(headHP <= 0)
        {
            KillBoss();
        }

        if(headAnim.curAnim != animations[10])
        {
            headAnim.PlayAnimation("headDamage");
            yield return new WaitForSeconds(0.5f);
            headAnim.PlayAnimation("headVuln");
        }
        attackWord = "";
    }

    private void KillBoss()
    {
        StopAllCoroutines();
        StartCoroutine(TheEnd());
    }

    private IEnumerator TheEnd()
    {
        Destroy(rHand);
        Destroy(lHand);
        Destroy(healthBarMain);
        attackWordUI.text = "";
        headAnim.PlayAnimation("headDamage");

        StartCoroutine(DeathSounds());
        StartCoroutine(FadeOut());

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();

        while (true)
        {
            transform.position = new Vector3(-20 + Random.Range(-1, 1), 27.4f + Random.Range(-1, 1), 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator FadeOut()
    {
        while(gameObject.transform.Find("Head").GetComponent<SpriteRenderer>().color.a > 0)
        {
            gameObject.transform.Find("Head").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, gameObject.transform.Find("Head").GetComponent<SpriteRenderer>().color.a - 0.01f);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(5);
        Destroy(gameObject);
    }

    private IEnumerator DeathSounds()
    {
        float volume = 1;

        while(volume > 0)
        {
            float delay = Random.Range(0.1f, 0.2f);
            sfx.PlayOneShot(death, volume);
            volume -= delay / 2;
            yield return new WaitForSeconds(delay);
        }
    }

    public IEnumerator Flash(string hand)
    {
        if(hand == "r")
        {
            flashingR = true;
            rHand.transform.Find("Flash").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            rHand.transform.Find("Flash").gameObject.SetActive(false);
            flashingR = false;
        }
        else
        {
            flashingL = true;
            lHand.transform.Find("Flash").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            lHand.transform.Find("Flash").gameObject.SetActive(false);
            flashingL = false;
        }
    }

    private IEnumerator Initiate()
    {
        yield return new WaitForSeconds(5);
        started = true;
    }

    private IEnumerator WarmHands()
    {
        rHandStart = rHand.transform;
        yield return new WaitForSeconds(1.5f);
        headAnim.PlayAnimation("grin");
        StartCoroutine(RotateHand(rHandPivot, 360, 1.5f));
        StartCoroutine(RotateHand(lHandPivot, -360, 1.5f));
        rHandAnim.PlayAnimation("handWarmUp");
        lHandAnim.PlayAnimation("handWarmUp");
        yield return new WaitForSeconds(2);
        rHandAnim.PlayAnimation("handIdle");
        ManageColliders("r", 0);
        lHandAnim.PlayAnimation("handIdle");
        ManageColliders("l", 0);
        StartCoroutine(RotateHand(rHand, 360, 1f));
        StartCoroutine(RotateHand(lHand, -360, 1f));
        yield return new WaitForSeconds(1);
        headAnim.PlayAnimation("idle");
        while(rHandHP < 125)
        {
            rHandHP += 2;
            lHandHP += 2;
            yield return new WaitForSeconds(0.02f);
        }

        rHandHP = 125;
        lHandHP = 125;
        activated = true;

        StartCoroutine(SwitchAttacks("r"));
        switchingR = true;
        StartCoroutine(SwitchAttacks("l"));
        switchingL = true;
    }

    private IEnumerator RotateHand(GameObject hand, float amount, float duration)
    {
        //float origRot = hand.transform.rotation.eulerAngles.z;
        for (float i = 0; i <= 1; i += 0.01f)
        {
            hand.transform.rotation = Quaternion.Euler(0, 0, Easings.EaseInOutBack(i) * amount);
            yield return new WaitForSeconds(0.01f * duration);
        }
    }

    private IEnumerator ShootAtPlayer(string hand)
    {
        if(hand == "r")
        {
            SAPR = true;
            rHandAnim.PlayAnimation("gun");
            ManageColliders("r", 3);
            coroutinesR.Add(StartCoroutine(RotateHand(rHandPivot, -90, 0.5f)));
            coroutinesR.Add(StartCoroutine(RotateHand(rHand, -90, 0.5f)));
            yield return new WaitForSeconds(1f);
            rHandSource.xmlFile = patterns[1];
            rHandSource.Initialize();
            SAPR = false;
        }
        else
        {
            SAPL = true;
            lHandAnim.PlayAnimation("gun");
            ManageColliders("l", 3);
            coroutinesL.Add(StartCoroutine(RotateHand(lHandPivot, 90, 0.5f)));
            coroutinesL.Add(StartCoroutine(RotateHand(lHand, 90, 0.5f)));
            yield return new WaitForSeconds(1f);
            lHandSource.xmlFile = patterns[1];
            lHandSource.Initialize();
            SAPL = false;
        }
    }

    private IEnumerator BulletCurtain(string hand)
    {
        if(hand == "r")
        {
            BCR = true;
            rHandAnim.PlayAnimation("gun");
            ManageColliders("r", 3);
            coroutinesR.Add(StartCoroutine(RotateHand(rHandPivot, 60, 0.5f)));
            coroutinesR.Add(StartCoroutine(RotateHand(rHand, 60, 0.5f)));
            yield return new WaitForSeconds(1f);
            rHandSource.xmlFile = patterns[2];
            rHandSource.Initialize();
            BCR = false;
        }
        else
        {
            BCL = true;
            lHandAnim.PlayAnimation("gun");
            ManageColliders("l", 3);
            coroutinesL.Add(StartCoroutine(RotateHand(lHandPivot, -75, 0.5f)));
            coroutinesL.Add(StartCoroutine(RotateHand(lHand, -110, 0.5f)));
            yield return new WaitForSeconds(1f);
            lHandSource.xmlFile = patterns[2];
            lHandSource.Initialize();
            BCL = false;
        }

        yield return new WaitForSeconds(40);

        curtain = false;
    }

    private IEnumerator SpinHand(string hand)
    {
        if(hand == "r")
        {
            spinningR = true;

            float tempHandRate = 0.01f;

            rHandAnim.PlayAnimation("palm");
            ManageColliders("r", 4);

            for (float i = 0; i <= 1; i += 0.0025f)
            {
                rHandPivot.transform.rotation = Quaternion.Euler(0, 0, Easings.EaseInQuad(i) * 1080);
                rHand.transform.localPosition = new Vector3(Easings.EaseInOutQuad(i * 0.5f) * 130 + 9.6f, 0, 0);
                yield return new WaitForSeconds(0.0075f);
            }

            while(rHand.transform.localPosition.x > 10)
            {
                tempHandRate *= 1.1f;
                rHand.transform.localPosition = new Vector3(rHand.transform.localPosition.x - tempHandRate, 0, 0);
                yield return new WaitForSeconds(0.02f);
            }

            spinningR = false;
        }
        else
        {
            spinningL = true;

            float tempHandRate = 0.01f;

            lHandAnim.PlayAnimation("palm");
            ManageColliders("l", 4);

            for (float i = 0; i <= 1; i += 0.0025f)
            {
                lHandPivot.transform.rotation = Quaternion.Euler(0, 0, Easings.EaseInQuad(i) * 1080);
                lHand.transform.localPosition = new Vector3(-(Easings.EaseInOutQuad(i * 0.5f) * 130 + 9.6f), 0, 0);
                yield return new WaitForSeconds(0.0075f);
            }

            handsRate = 0.01f;

            while (lHand.transform.localPosition.x < -10)
            {
                tempHandRate *= 1.1f;
                lHand.transform.localPosition = new Vector3(lHand.transform.localPosition.x + tempHandRate, 0, 0);
                yield return new WaitForSeconds(0.02f);
            }

            spinningL = false;
        }
    }

    private IEnumerator SwitchAttacks(string hand)
    {
        int attack;
        yield return new WaitForSeconds(Random.Range(2, 5));

        if (!curtain)
        {
            attack = Mathf.FloorToInt(Random.Range(0, 4));

        }
        else
        {
            attack = Mathf.FloorToInt(Random.Range(0, 3));
        }

        switch (attack)
        {
            case 0:
                break;

            case 1:
                if(hand == "r")
                {
                    coroutinesR.Add(StartCoroutine(ShootAtPlayer("r")));
                }
                else
                {
                    coroutinesL.Add(StartCoroutine(ShootAtPlayer("l")));
                }
                break;

            case 2:
                if (hand == "r")
                {
                    coroutinesR.Add(StartCoroutine(SpinHand("r")));
                }
                else
                {
                    coroutinesL.Add(StartCoroutine(SpinHand("l")));
                }
                break;

            case 3:
                curtain = true;
                if (hand == "r")
                {
                    coroutinesR.Add(StartCoroutine(BulletCurtain("r")));
                }
                else
                {
                    coroutinesL.Add(StartCoroutine(BulletCurtain("l")));
                }
                break;
        }

        if (hand == "r")
        {
            yield return new WaitForSeconds(1.5f);
            switchingR = false;
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            switchingL = false;
        }
    }
}
