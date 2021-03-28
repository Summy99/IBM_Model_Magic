using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private GameObject gc;
   
    [SerializeField]
    private AudioClip[] types;

    public string type = "slow";

    public bool activated = false;

    void Start()
    {
        activated = false;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));

        float x = 0;

        rb = gameObject.GetComponent<Rigidbody2D>();
        gc = GameObject.FindGameObjectWithTag("GameController");
        player = GameObject.FindGameObjectWithTag("Player");

        if(Mathf.FloorToInt(Random.Range(0, 2)) == 0)
        {
            x = Random.Range(-20, -10);
        }
        else if (Mathf.FloorToInt(Random.Range(0, 2)) == 1)
        {
            x = Random.Range(10, 20);
        }

        rb.AddForce(new Vector2(x, Random.Range(20, 40)), ForceMode2D.Impulse);
    }

    void Update()
    {
        if (!activated)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -20f, 999));
        }

        if (activated)
        {
            rb.gravityScale = 0;
            rb.AddForce((player.transform.position - transform.position).normalized * 500);
        }

        if(Vector2.Distance(transform.position, player.transform.position) <= 10)
        {
            activated = true;
        }

        if(transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && type == "slow")
        {
            collision.gameObject.GetComponent<Typing>().slowDown += 0.2f * Typing.maxSlowDown;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && type == "heal")
        {
            if(GameController.lives < 6)
            {
                collision.gameObject.GetComponent<PlayerHealth>().heal++;
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && type == "keycap")
        {
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(types[Mathf.FloorToInt(Random.Range(0, types.Length))]);
            collision.gameObject.GetComponent<Typing>().slowDown += 0.01f * Typing.maxSlowDown;
            GameController.keycaps++;

            collision.gameObject.GetComponent<Typing>().letterProgress++;

            if(collision.gameObject.GetComponent<Typing>().letterProgress == 6)
            {
                collision.gameObject.GetComponent<Typing>().letterProgress = 0;
                gc.GetComponent<UI>().AddLetter();
            }

            Destroy(gameObject);
        }
    }
}
