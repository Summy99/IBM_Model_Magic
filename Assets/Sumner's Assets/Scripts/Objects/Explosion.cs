using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.localScale = new Vector3(transform.localScale.x + 1f * (Time.deltaTime * 50), transform.localScale.y + 1f * (Time.deltaTime * 50), transform.localScale.z);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, gameObject.GetComponent<SpriteRenderer>().color.a - 0.007f);

        if(gameObject.GetComponent<SpriteRenderer>().color.a <= Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(5);
        }
    }
}
