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
    private BulletSourceScript bml;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        sfx = gameObject.GetComponent<AudioSource>();
        bml = gameObject.GetComponent<BulletSourceScript>();
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UI>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet")) && !shield)
        {
            Die();
        }
        else if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet")) && shield)
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "SlowBarUpgrade" && gc.keycaps >= 20)
        {
            collision.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Canvas").transform.Find("Prices").Find("SlowBarPrice").gameObject.SetActive(false);
            gc.keycaps -= 20;
            Typing.maxSlowDown += 2;
        }

        if (collision.gameObject.name == "Heal" && gc.keycaps >= 10 && GameController.lives < 6)
        {
            collision.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Canvas").transform.Find("Prices").Find("HealPrice").gameObject.SetActive(false);
            gc.keycaps -= 10;
            GameController.lives++;
        }

        if (collision.gameObject.name == "NewWord" && gc.keycaps >= 30)
        {
            collision.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Canvas").transform.Find("Prices").Find("WordPrice").gameObject.SetActive(false);
            gc.keycaps -= 30;

            string[] words = new string[GameController.words.Count];
            GameController.words.Keys.CopyTo(words, 0);

            string wordToUnlock = "AUTO";

            while (GameController.words[wordToUnlock])
            {
                wordToUnlock = words[Mathf.FloorToInt(Random.Range(0, words.Length))];
            }

            if (!GameController.words[wordToUnlock])
            {
                GameController.words[wordToUnlock] = true;
                ui.AddWord(wordToUnlock);
            }
        }
    }

    public void Die()
    {
        GameController.lives--;

        sfx.PlayOneShot(death);

        bml.xmlFile = gameObject.GetComponent<Typing>().patterns[0];

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

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
