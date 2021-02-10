using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        float x = 0;

        rb = gameObject.GetComponent<Rigidbody2D>();

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
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -10f, 999));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Typing>().slowDown += 0.1f * collision.gameObject.GetComponent<Typing>().maxSlowDown;
            Destroy(gameObject);
        }
    }
}
