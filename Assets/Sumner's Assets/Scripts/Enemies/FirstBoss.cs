using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Pixelnest.BulletML;

public class FirstBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BulletSourceScript bmll;
    private BulletSourceScript bmlr;
    private BulletSourceScript bml;
    private AudioSource plyrsrc;
    private Image healthBar;

    [SerializeField]
    private TextAsset[] patterns;

    [SerializeField]
    private AudioClip hit;

    [SerializeField]
    private int attack = 0;
    public float health = 0;
    private float laserDamageCool;
    private bool activated = false;
    private bool positioned = false;
    private string direction = "right";

    private bool flashing = false;
    private bool walkFinished = true;
    private bool switching = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        bml = gameObject.GetComponent<BulletSourceScript>();
        healthBar = gameObject.transform.Find("Canvas").Find("HealthBar").GetComponent<Image>();
        bmll = gameObject.transform.Find("left").GetComponent<BulletSourceScript>();
        bmlr = gameObject.transform.Find("right").GetComponent<BulletSourceScript>();
        plyrsrc = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
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

        healthBar.fillAmount = health / 600;

        if(!activated && !positioned && gameObject.transform.position.y > 37)
        {
            rb.velocity = -(transform.up * 10);
            health = 0;
        }

        if(!activated && !positioned && gameObject.transform.position.y <= 37)
        {
            anim.SetBool("Stopped", true);
            rb.velocity = Vector2.zero;
            positioned = true;
        }

        if(activated && !walkFinished)
        {
            if(direction == "right" && transform.position.x > 12)
            {
                direction = "left";
                rb.velocity = -(transform.right * 10);
            }

            else if (direction == "left" && transform.position.x < -56)
            {
                direction = "right";
                rb.velocity = transform.right * 10;
            }
        }

        if(bmll.IsEnded && bmlr.IsEnded && bml.IsEnded)
        {
            anim.SetBool("Crying", false);
        }

        if (activated && (bmll.IsEnded && bmlr.IsEnded && bml.IsEnded) && walkFinished && !switching && GameObject.FindGameObjectsWithTag("Bullet").Length == 0)
        {
            StartCoroutine("SwitchAttacks");
        }
    }

    private void FixedUpdate()
    {
        if (!activated && positioned && health < 600)
        {
            health += 5;
        }

        if(!activated && positioned && health >= 600)
        {
            health = 600;
            activated = true;
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

        if(health <= 0)
        {
            bml.xmlFile = null;
            
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject b in bullets)
            {
                Destroy(b);
            }

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            bml.enabled = false;
            StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        GameController.Level = 2;
        LevelTracker.LevelToLoad = 2;
        SceneManager.LoadScene(4);
    }

    private IEnumerator SwitchAttacks()
    {
        switching = true;
        yield return new WaitForSeconds(1);
        if(attack == 0)
        {
            attack = Mathf.FloorToInt(Random.Range(1, 3));
        }
        else
        {
            attack = Mathf.FloorToInt(Random.Range(0, 3));
        }

        switch (attack)
        {
            case 0:
                StartCoroutine("Walk");
                break;

            case 1:
                anim.SetBool("Crying", true);
                if (Mathf.FloorToInt(Random.Range(0, 2)) == 0)
                {
                    bmll.xmlFile = patterns[1];
                    bmll.Initialize();
                }
                else
                {
                    bmlr.xmlFile = patterns[1];
                    bmlr.Initialize();
                }
                break;

            case 2:
                anim.SetBool("Crying", true);
                bml.xmlFile = patterns[0];
                bml.Initialize();
                break;
        }
        
        switching = false;
    }

    private IEnumerator Walk()
    {
        walkFinished = false;
        anim.SetBool("Stopped", false);
        
        if(Mathf.FloorToInt(Random.Range(0, 2)) == 0)
        {
            direction = "right";
            rb.velocity = transform.right * 10;
        }
        else
        {
            direction = "left";
            rb.velocity = -(transform.right * 10);
        }

        yield return new WaitForSeconds(Random.Range(1, 6));

        walkFinished = true;
        anim.SetBool("Stopped", true);
        rb.velocity = Vector2.zero;
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
