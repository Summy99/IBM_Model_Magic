using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private GameObject gc;

    [SerializeField]
    private AudioClip healthDrop;
    [SerializeField]
    private AudioClip[] types;
    [SerializeField]
    private Sprite[] shards;

    private PolygonCollider2D[] cols;

    public string type = "slow";

    private float speed = 1f;
    private int shard = 0;
    private bool moving = false;
    public bool activated = false;

    void Start()
    {
        activated = false;

        if(type == "heal")
        {
            cols = gameObject.GetComponents<PolygonCollider2D>();

            shard = Mathf.FloorToInt(Random.Range(0, shards.Length));
            gameObject.GetComponent<SpriteRenderer>().sprite = shards[shard];

            for(int i = 0; i < cols.Length; i++)
            {
                if(i == shard)
                {
                    cols[i].enabled = true;
                }
                else
                {
                    cols[i].enabled = false;
                }
            }
        }

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

            if (!moving)
            {
                speed = rb.velocity.magnitude * 3;
                StartCoroutine("Realign");
            }

            speed *= 1 + (Time.deltaTime * 2);
        }

        if(Vector2.Distance(transform.position, player.transform.position) <= 10)
        {
            activated = true;
        }

        if(transform.position.y < -50 && !activated)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Realign()
    {
        moving = true;
        while (true)
        {
            rb.velocity = Vector2.zero;
            rb.velocity = (player.transform.position - transform.position).normalized * speed;
            yield return new WaitForSeconds(0.01f);
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
                collision.gameObject.GetComponent<AudioSource>().PlayOneShot(healthDrop);
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
