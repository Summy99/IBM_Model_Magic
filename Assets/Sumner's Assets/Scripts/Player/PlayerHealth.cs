using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelnest.BulletML;

public class PlayerHealth : MonoBehaviour
{
    private GameController gc;
    private AudioSource sfx;
    private UI ui;
    [SerializeField]
    private AudioClip death;

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
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UI>();
    }

    private void Update()
    {
        if(heal >= 8)
        {
            heal = 0;
            GameController.lives++;
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

        sfx.PlayOneShot(death);

        bml.xmlFile = gameObject.GetComponent<Typing>().patterns[0];
        gameObject.GetComponent<Typing>().StopCoroutine("HomingShot");
        gameObject.GetComponent<Typing>().StopCoroutine("Laser");

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if(gameObject.GetComponent<Typing>().mode == "typing")
        {
            gameObject.GetComponent<Typing>().mode = "shooting";
        }

        if(GameController.lives <= 0)
        {
            gc.GameOver();
        }

        gameObject.transform.position = new Vector3(-20, -43, 0);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Typing>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

        StartCoroutine("Respawn");
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        for (int i = 0; i < 5; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Typing>().enabled = true;
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
}
