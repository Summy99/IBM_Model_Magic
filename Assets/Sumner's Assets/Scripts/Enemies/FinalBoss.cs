using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelnest.BulletML;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    private BulletSourceScript bml;
    private Animator anim;
    private Image healthBar;

    [SerializeField] private TextAsset[] patterns;
    private AudioSource plyrsrc;

    [SerializeField] private AudioClip hit;

    public float health = 0f;
    public float moveSpeed = 10f;

    private int attack = 0;
    private bool switching = false;
    private bool activated = false;
    private bool positioned = false;
    private bool started = false;
    private string direction = "right";
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bml = gameObject.GetComponent<BulletSourceScript>();
        anim = gameObject.GetComponent<Animator>();
        plyrsrc = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        healthBar = gameObject.transform.Find("Canvas").Find("HealthBar").GetComponent<Image>();

        StartCoroutine("Enter");
    }

    void Update()
    {
        if (started)
        {
            healthBar.fillAmount = health / 1000;

            if (Input.GetKeyDown(KeyCode.Home))
            {
                health = 2;
            }

            if (activated && positioned)
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

                if (transform.position.x >= 13 && direction == "right")
                {
                    direction = "left";
                }

                if (transform.position.x <= -56 && direction == "left")
                {
                    direction = "right";
                }

                if (bml.IsEnded && !switching)
                {
                    StartCoroutine("SwitchAttacksPhase1");
                }
            }
            
            if (!activated && !positioned)
            {
                rb.velocity = -(transform.up * moveSpeed);
                if (transform.position.y <= 11)
                {
                    rb.velocity = Vector2.zero;
                    positioned = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!activated && positioned && health < 1000)
        {
            health += 5;
        }

        if (!activated && positioned && health >= 1000)
        {
            health = 1000;
            activated = true;
        }
    }

    private IEnumerator SwitchAttacksPhase1()
    {
        switching = true;
        yield return new WaitForSeconds(0.5f);
        attack = Mathf.FloorToInt(Random.Range(0, 3));

        switch (attack)
        {
            case 0:
                bml.xmlFile = patterns[1];
                bml.Initialize();
                direction = "stop";
                break;

            case 1:
                bml.xmlFile = patterns[2];
                bml.Initialize();
                direction = "right";
                break;

            case 2:
                anim.SetTrigger("devil");
                bml.xmlFile = patterns[3];
                bml.Initialize();
                direction = "stop";
                yield return new WaitForSeconds(12);
                anim.SetTrigger("undevil");
                StartCoroutine("SwitchAttacksPhase1");
                break;
        }

        switching = false;
    }

    private IEnumerator Enter()
    {
        yield return new WaitForSeconds(3);
        started = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Letter") && activated)
        {
            TakeDamage(collision.gameObject.GetComponent<LetterController>().damage);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PlayerShot") && activated)
        {
            TakeDamage(collision.gameObject.GetComponent<ShotController>().damage);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PlayerShotBig") && activated)
        {
            float damageToDeal = collision.gameObject.GetComponent<BigShotController>().damage;
            collision.gameObject.GetComponent<BigShotController>().damage -= health;
            TakeDamage(damageToDeal);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        plyrsrc.PlayOneShot(hit, 0.5f);

        if (health <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(5);
    }
}
