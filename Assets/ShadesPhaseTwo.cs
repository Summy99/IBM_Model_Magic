using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelnest.BulletML;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShadesPhaseTwo : MonoBehaviour
{
    private Rigidbody2D rb;
    private AnimationManager anim;
    private BulletSourceScript bml;
    private AudioSource sfx;
    private Image healthBar;
    private CircleCollider2D playerCol;

    [SerializeField] private Sprite brokenClock;
    [SerializeField] private Sprite[] introFrames, idleFrames, deathFrames, spawnFrames, predeathFrames;
    [SerializeField] private TextAsset[] patterns;
    [SerializeField] private AudioClip music, glass1, glass2, glass3, hit, death, boom;

    [SerializeField] private GameObject glassDecal;

    public Animation[] animations;
    public string[] names;

    public float health = 200;
    private float laserDamageCool = 0;
    private bool activated = false, bounceStarted = false, flashing = false, iced = false, spawningIce = false;
    /*
    public readonly string[] attackWords = new string[] {   "IDENTICAL", "CHOCOLATE", "BEAUTIFUL", "HAPPINESS", "WEDNESDAY", "CHALLENGE", "CELEBRATE", "ADVENTURE",
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
        "DISCOVERY", "ABANDONED", "SUFFOCATE", "TINKERING", "CARABINER", "COUNTDOWN", "CHAMPLAIN", "DEMOCRACY", "REARRANGE", "ELEVATION", "ABORIGINE", "ANIMATION" };
    */

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<AnimationManager>();
        bml = gameObject.GetComponent<BulletSourceScript>();
        healthBar = transform.Find("Canvas").Find("HealthBar").GetComponent<Image>();
        sfx = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = music;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().loop = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();

        animations = new Animation[]
        {
            new Animation()
            {
                Frames = introFrames,
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
                Frames = deathFrames,
                Speed = 0.2f,
                Looping = false
            },
            new Animation()
            {
                Frames = spawnFrames,
                Speed = 0.2f,
                Looping = true
            },
            new Animation()
            {
                Frames = predeathFrames,
                Speed = 0.2f,
                Looping = true
            },
        };
        names = new string[] { "intro", "idle", "death", "spawn", "predeath" };
        anim.PopulateDictionary(names, animations);
        anim.PlayAnimation("intro");
        health = 0;
        StartCoroutine("Initiate");
        playerCol = GameObject.FindWithTag("Player").GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        transform.Find("Flash").GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        if (activated)
        {
            //if(rb.velocity.magnitude <= 10 && !bounceStarted)
            //{
                //StartCoroutine("Bounce");
            //}
        }

        if (laserDamageCool > 0)
        {
            laserDamageCool -= Time.deltaTime;
        }

        healthBar.fillAmount = health / 200;

        print(rb.velocity.ToString());
    }

    private IEnumerator Initiate()
    {
        yield return new WaitForSeconds(0.5f);
        anim.PlayAnimation("idle");
        while(health < 200)
        {
            health += 2;
            yield return new WaitForSeconds(0.02f);
        }
        activated = true;
        StartCoroutine(Bounce());
        bml.xmlFile = patterns[1];
    }

    private IEnumerator DeathSounds()
    {
        float volume = 1;

        while (volume > 0)
        {
            float delay = Random.Range(0.1f, 0.2f);
            sfx.PlayOneShot(death, volume);
            volume -= delay / 2;
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator Bounce()
    {
        bounceStarted = true;
        yield return new WaitForSeconds(0.5f);
        rb.AddForce((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * 30, ForceMode2D.Impulse);
        bounceStarted = false;
    }

    private void BreakClock()
    {
        sfx.PlayOneShot(glass2);
        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Margins").Find("SlowBar").Find("Icon").GetComponent<Image>().sprite = brokenClock;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PlayerHealth>().shield && collision.collider == playerCol && collision.gameObject.GetComponent<Typing>().modeSwitchCool <= 0 && health > 0)
        {
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<PlayerHealth>().Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Letter") && activated && health > 0)
        {
            TakeDamage(collision.gameObject.GetComponent<LetterController>().damage);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PlayerShot") && activated && health > 0)
        {
            TakeDamage(collision.gameObject.GetComponent<ShotController>().damage);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("HomingShot") && activated && health > 0)
        {
            TakeDamage(collision.gameObject.GetComponent<HomingShotController>().damage + 0.1f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PlayerShotBig") && activated && health > 0)
        {
            float damageToDeal = collision.gameObject.GetComponent<BigShotController>().damage;
            collision.gameObject.GetComponent<BigShotController>().damage -= health;
            TakeDamage(damageToDeal);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("laser") && activated && health > 0)
        {
            if (laserDamageCool <= 0)
            {
                TakeDamage(0.4f);
                laserDamageCool = 0.001f;
            }
        }
    }

    private void TakeDamage(float damage)
    {
        if (!flashing)
        {
            StartCoroutine("Flash");
        }

        health -= damage;

        sfx.PlayOneShot(hit, 0.2f);

        if (health <= 100 && !iced)
        {
            iced = true;
            StartCoroutine("SpawnIce");
        }

        if(health <= 0)
        {
            bml.xmlFile = patterns[0];
            rb.velocity = Vector2.zero;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach(GameObject b in bullets)
            {
                Destroy(b);
            }

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
            StartCoroutine("Die");
        }
    }

    private IEnumerator SpawnIce()
    {
        spawningIce = true;
        rb.velocity = Vector2.zero;
        bml.xmlFile = patterns[0];
        anim.PlayAnimation("spawn");
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Waves>().StartCoroutine("Ice");
        yield return new WaitForSeconds(2f);
        StartCoroutine(Bounce());
        bml.xmlFile = patterns[1];
        anim.PlayAnimation("idle");
        spawningIce = false;
    }

    private IEnumerator Die()
    {
        StartCoroutine(DeathSounds());
        StartCoroutine("Shake");
        anim.PlayAnimation("predeath");
        yield return new WaitForSeconds(3);
        StopCoroutine("Shake");
        anim.PlayAnimation("death");
        StartCoroutine(DeathSounds());
        sfx.PlayOneShot(boom);
        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Margins").GetComponent<ScreenShake>().StartCoroutine("Shake", 2);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(5);
    }

    private IEnumerator Shake()
    {
        Vector3 initPos = transform.position;
        while (true)
        {
            transform.position = new Vector3(initPos.x + Random.Range(-1, 1), initPos.y + Random.Range(-1, 1), 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator Flash()
    {
        flashing = true;
        transform.Find("Flash").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.Find("Flash").gameObject.SetActive(false);
        flashing = false;
    }
}