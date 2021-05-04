using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelnest.BulletML;

public class PlayerHealth : MonoBehaviour
{
    private GameController gc;
    private AudioSource sfx;
    private UI ui;
    private AnimationManager anim;
    private Typing typing;
    [SerializeField]
    private AudioClip death, healed;

    [SerializeField]
    private GameObject keycap;

    [SerializeField]
    private AudioClip[] types;

    public bool shield = false;
    public int heal = 0;
    public CircleCollider2D colliderPlayer;
    private BulletSourceScript bml;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        sfx = gameObject.GetComponent<AudioSource>();
        bml = gameObject.GetComponent<BulletSourceScript>();
        anim = gameObject.GetComponent<AnimationManager>();
        typing = gameObject.GetComponent<Typing>();
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UI>();
    }

    private void Update()
    {
        if(heal >= 8)
        {
            heal = 0;
            GameController.lives++;
            sfx.PlayOneShot(healed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == colliderPlayer)
        {
            if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet")) && !shield)
            {
                print(collision.gameObject.name);
                Die();
            }
            else if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet")) && shield)
            {
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.name == "SlowBarUpgrade" && GameController.keycaps >= 25)
            {
                collision.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("Canvas").transform.Find("Prices").Find("SlowBarPrice").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("Canvas").transform.Find("Prices").Find("SlowBarPriceIcon").gameObject.SetActive(false);
                GameController.keycaps -= 25;
                Typing.maxSlowDown *= 1.1f;
            }

            if (collision.gameObject.name == "Heal" && GameController.keycaps >= 15 && GameController.lives < 6)
            {
                GameController.keycaps -= 15;
                GameController.lives++;
            }
        }
    }

    public void Die()
    {
        GameController.lives--;
        anim.PlayAnimation("death");
        Time.timeScale = 1;

        int lp = gameObject.GetComponent<Typing>().letterProgress;

        for (int i = 0; i < lp; i++)
        {
            gameObject.GetComponent<Typing>().letterProgress--;
            Instantiate(keycap, transform.position, Quaternion.identity);
        }

        sfx.PlayOneShot(death);

        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Margins").GetComponent<ScreenShake>().StartCoroutine("Shake", 0.5);

        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (GameObject c in collectibles)
        {
            c.GetComponent<Collectibles>().activated = false;
        }

        bml.xmlFile = gameObject.GetComponent<Typing>().patterns[0];
        gameObject.GetComponent<Typing>().StopCoroutine("HomingShot");
        gameObject.GetComponent<Typing>().StopCoroutine("ShootLetters");
        gameObject.GetComponent<Typing>().StopCoroutine("Laser");
        gameObject.transform.Find("laser").gameObject.SetActive(false);

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-500, -300);

        if (gameObject.GetComponent<Typing>().mode == "typing")
        {
            gameObject.GetComponent<Typing>().mode = "shooting";
        }

        if(GameController.lives <= 0)
        {
            gc.GameOver();
        }

        gameObject.GetComponent<Typing>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

        StartCoroutine("Respawn");
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        gameObject.transform.position = new Vector3(-20, -43, 0);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        anim.PlayAnimation("idle");
        for (int i = 0; i < 5; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        gameObject.GetComponent<Typing>().enabled = true;
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
}
