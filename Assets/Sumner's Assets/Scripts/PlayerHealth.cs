using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameController gc;
    private AudioSource sfx;
    [SerializeField]
    private AudioClip death;

    public bool shield = false;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        sfx = gameObject.GetComponent<AudioSource>();
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
    }

    public void Die()
    {
        gc.lives--;

        sfx.PlayOneShot(death);

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
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.5f);
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Typing>().enabled = true;
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
