using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelnest.BulletML;

public class FirstBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BulletSourceScript bmll;
    private BulletSourceScript bmlr;
    private AudioSource plyrsrc;

    [SerializeField]
    private TextAsset[] patterns;

    [SerializeField]
    private AudioClip hit;

    [SerializeField]
    private int attack = 0;
    public float health = 600;
    private bool activated = false;
    private string direction = "right";

    private bool walkFinished = true;
    private bool switching = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        bmll = gameObject.transform.Find("left").GetComponent<BulletSourceScript>();
        bmlr = gameObject.transform.Find("right").GetComponent<BulletSourceScript>();
        plyrsrc = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!activated && gameObject.transform.position.y > 37)
        {
            rb.velocity = -(transform.up * 10);
        }

        if(!activated && gameObject.transform.position.y <= 37)
        {
            anim.SetBool("Stopped", true);
            rb.velocity = Vector2.zero;
            activated = true;
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

        if(bmll.IsEnded && bmlr.IsEnded)
        {
            anim.SetBool("Crying", false);
        }

        if (activated && (bmll.IsEnded && bmlr.IsEnded) && walkFinished && !switching)
        {
            StartCoroutine("SwitchAttacks");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Letter"))
        {
            TakeDamage(collision.gameObject.GetComponent<ShotController>().damage);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PlayerShot"))
        {
            TakeDamage(collision.gameObject.GetComponent<ShotController>().damage);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        plyrsrc.PlayOneShot(hit);

        if(health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator SwitchAttacks()
    {
        switching = true;
        yield return new WaitForSeconds(1);
        attack = Mathf.FloorToInt(Random.Range(0, 5));

        switch (attack)
        {
            case 0:
                StartCoroutine("Walk");
                break;

            case 1:
                break;

            case 2:
                anim.SetBool("Crying", true);
                if (Mathf.FloorToInt(Random.Range(0, 2)) == 0)
                {
                    bmll.xmlFile = patterns[0];
                    bmll.Initialize();
                }
                else
                {
                    bmlr.xmlFile = patterns[0];
                    bmlr.Initialize();
                }
                break;

            case 3:
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
}
