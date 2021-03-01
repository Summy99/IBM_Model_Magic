using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameController gc;
    [SerializeField] private GameObject keycap;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if(transform.position.y > 15)
        {
            rb.velocity = -(transform.up * 10);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerShot") || collision.gameObject.CompareTag("Letter"))
        {
            Instantiate(keycap, transform.position, Quaternion.identity);
            gc.tutorialStage = 6;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
