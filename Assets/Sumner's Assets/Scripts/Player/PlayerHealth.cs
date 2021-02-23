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

        if (collision.gameObject.name == "SlowBarUpgrade" && gc.keycaps >= 25)
        {
            collision.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Canvas").transform.Find("Prices").Find("SlowBarPrice").gameObject.SetActive(false);
            gc.keycaps -= 25;
            gameObject.GetComponent<Typing>().maxSlowDown += 2;
        }

        if (collision.gameObject.name == "Heal" && gc.keycaps >= 15 && gc.lives < 6)
        {
            collision.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Canvas").transform.Find("Prices").Find("HealPrice").gameObject.SetActive(false);
            gc.keycaps -= 15;
            gc.lives++;
        }

        if (collision.gameObject.name == "NewWord" && gc.keycaps >= 40)
        {
            collision.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Canvas").transform.Find("Prices").Find("WordPrice").gameObject.SetActive(false);
            gc.keycaps -= 40;

            string[] words = new string[gc.words.Count];
            gc.words.Keys.CopyTo(words, 0);

            string wordToUnlock = "AUTO";

            while (gc.words[wordToUnlock])
            {
                wordToUnlock = words[Mathf.FloorToInt(Random.Range(0, words.Length))];
            }

            if (!gc.words[wordToUnlock])
            {
                gc.words[wordToUnlock] = true;
                ui.AddWord(wordToUnlock);
            }
        }
    }

    public void Die()
    {
        gc.lives--;

        sfx.PlayOneShot(death);

        bml.xmlFile = gameObject.GetComponent<Typing>().patterns[0];

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if(gc.lives <= 0)
        {
            gc.GameOver();
        }

        gameObject.transform.position = new Vector3(-20, -43, 0);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Typing>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

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
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
