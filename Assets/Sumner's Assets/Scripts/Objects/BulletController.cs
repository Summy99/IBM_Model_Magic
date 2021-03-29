using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.GetComponent<PlayerHealth>().shield && collision == GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>())
        {
            print(collision.gameObject.name);
            collision.GetComponent<PlayerHealth>().Die();
        }
        else if (collision.gameObject.CompareTag("Player") && collision.GetComponent<PlayerHealth>().shield && collision == GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>())
        {
            Destroy(collision.gameObject);
        }
    }
}
