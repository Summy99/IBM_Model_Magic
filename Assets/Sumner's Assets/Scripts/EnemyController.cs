using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource sfx;
    [SerializeField]
    private AudioClip death;
    public float health = 3;

    public int wave = 0;
    public float moveSpeed = 10;

    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(gameObject.transform.position.y > 56 || gameObject.transform.position.y < -56 || gameObject.transform.position.x > 26 || gameObject.transform.position.x < -66)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        switch (wave)
        {
            case 1:
                rb.velocity = Vector2.down * moveSpeed;
                break;

            case 2:
                rb.velocity = Vector2.down * moveSpeed;
                break;

            case 3:
                rb.velocity = Vector2.down * moveSpeed;
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            sfx.PlayOneShot(death, 0.5f);
            Destroy(gameObject);
        }
    }
}
