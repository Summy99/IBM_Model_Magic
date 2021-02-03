using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;

    public float damage = 1;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        rb.AddTorque(-720);
        rb.velocity = Vector2.up * 75;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
