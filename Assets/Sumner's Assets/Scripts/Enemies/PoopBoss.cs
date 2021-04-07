using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelnest.BulletML;
using UnityEngine.UI;

public class PoopBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    private AnimationManager anim;
    private GameObject[] topSpawns;
    private Image healthBar;

    [SerializeField] private GameObject spawnerHolder;
    [SerializeField] private GameObject poopWallPrefab;
    private BulletSourceScript bml;
    private AudioSource plyrsrc;

    [SerializeField]
    private AudioClip hit;

    [SerializeField]
    private TextAsset pattern;

    public Animation[] animations;
    public string[] names;

    [SerializeField] Sprite[] idleSmallFrames, enterFrames, idleFrames, yellFrames, deathFrames;

    private bool activated = false;
    private bool positioned = false;
    private bool switching = false;
    private bool flashing = false;
    private bool animStarted = false;
    private string direction = "right";
    public float moveSpeed = 10f;
    private float laserDamageCool;
    public float health = 400f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bml = gameObject.GetComponent<BulletSourceScript>();
        anim = gameObject.GetComponent<AnimationManager>();
        healthBar = gameObject.transform.Find("Canvas").Find("HealthBar").GetComponent<Image>();
        plyrsrc = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        spawnerHolder = GameObject.Find("SpawnPoints");

        topSpawns = GetAllChildren(spawnerHolder.transform.Find("Top"));

        animations = new Animation[]
{
            new Animation()
            {
                Frames = idleSmallFrames,
                Speed = 0.2f,
                Looping = true
            },
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
                Frames = yellFrames,
                Speed = 0.2f,
                Looping = true
            },
            new Animation()
            {
                Frames = deathFrames,
                Speed = 0.2f,
                Looping = false
            }
};

        names = new string[] { "idleSmall", "enter", "idle", "yell", "death" };

        anim.PopulateDictionary(names, animations);

        anim.PlayAnimation("idleSmall");
    }

    void Update()
    {
        transform.Find("Flash").GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        if (laserDamageCool > 0)
        {
            laserDamageCool -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            health = 2;
        }

        healthBar.fillAmount = health / 400;

        if (!activated && !positioned)
        {
            health = 0;
            rb.velocity = -(transform.up * moveSpeed);

            if(transform.position.y <= 24)
            {
                rb.velocity = Vector2.zero;
                positioned = true;
            }
        }
        else if (activated)
        {
            switch (direction)
            {
                case "right":
                    rb.velocity = transform.right * moveSpeed;
                    break;

                case "left":
                    rb.velocity = -(transform.right * moveSpeed);
                    break;

                case "stop":
                    rb.velocity = Vector2.zero;
                    break;
            }

            if (transform.position.x >= 15 && direction == "right")
            {
                direction = "left";
            }

            if (transform.position.x <= -57.5f && direction == "left")
            {
                direction = "right";
            }

            if (bml.IsEnded && !switching && GameObject.FindGameObjectsWithTag("Bullet").Length == 0)
            {
                StartCoroutine("SwitchAttacks");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!activated && positioned && health < 1000)
        {
            health += 5;
            
            if (!animStarted)
            {
                anim.PlayAnimation("enter");
                animStarted = true;
            }
        }

        if (!activated && positioned && health >= 1000)
        {
            health = 400;
            anim.PlayAnimation("idle");
            activated = true;
            bml.xmlFile = pattern;
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
            TakeDamage(collision.gameObject.GetComponent<HomingShotController>().damage);
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

        plyrsrc.PlayOneShot(hit, 0.2f);

        if (health <= 0)
        {
            bml.xmlFile = null;

            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject b in bullets)
            {
                Destroy(b);
            }

            StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        direction = "stop";
        yield return new WaitForSeconds(1);
        anim.PlayAnimation("death");
        yield return new WaitForSeconds(3);
        GameController.Level = 3;
        LevelTracker.LevelToLoad = 3;
        SceneManager.LoadScene(4);
    }

    private IEnumerator SwitchAttacks()
    {
        switching = true;

        anim.PlayAnimation("yell");

        direction = "stop";

        for (int i = 0; i < Mathf.FloorToInt(Random.Range(5, 13)); i++)
        {
            GameObject p = Instantiate(poopWallPrefab, topSpawns[Mathf.FloorToInt(Random.Range(0, topSpawns.Length))].transform.position, Quaternion.identity);
            Destroy(p, 5f);

            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        }

        bml.Initialize();
        direction = "right";
        anim.PlayAnimation("idle");
        switching = false;
    }

    private GameObject[] GetAllChildren(Transform parent)
    {
        GameObject[] children = new GameObject[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i).gameObject;
        }

        return children;
    }

    public IEnumerator Flash()
    {
        flashing = true;
        transform.Find("Flash").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.Find("Flash").gameObject.SetActive(false);
        flashing = false;
    }
}