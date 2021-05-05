using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisions : MonoBehaviour
{
    public string side = "r";
    private float laserDamageCool = 0;
    private CircleCollider2D playerCol;

    private void Start()
    {
        playerCol = GameObject.FindWithTag("Player").GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(laserDamageCool > 0)
        {
            laserDamageCool -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(side == "r")
        {
            if(collision.gameObject.CompareTag("Letter") && transform.parent.parent.GetComponent<GlassesBoss>().activated && transform.parent.parent.GetComponent<GlassesBoss>().rHandHP > 0)
            {
                transform.parent.parent.GetComponent<GlassesBoss>().TakeDamage(side, collision.GetComponent<LetterController>().damage);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("PlayerShot") && transform.parent.parent.GetComponent<GlassesBoss>().activated && transform.parent.parent.GetComponent<GlassesBoss>().rHandHP > 0)
            {
                transform.parent.parent.GetComponent<GlassesBoss>().TakeDamage(side, collision.GetComponent<ShotController>().damage);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("HomingShot") && transform.parent.parent.GetComponent<GlassesBoss>().activated && transform.parent.parent.GetComponent<GlassesBoss>().rHandHP > 0)
            {
                transform.parent.parent.GetComponent<GlassesBoss>().TakeDamage(side, collision.GetComponent<HomingShotController>().damage + 0.25f);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("PlayerShotBig") && transform.parent.parent.GetComponent<GlassesBoss>().activated && transform.parent.parent.GetComponent<GlassesBoss>().rHandHP > 0)
            {
                transform.parent.parent.GetComponent<GlassesBoss>().TakeDamage(side, collision.GetComponent<BigShotController>().damage);
                Destroy(collision.gameObject);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Letter") && transform.parent.parent.GetComponent<GlassesBoss>().activated && transform.parent.parent.GetComponent<GlassesBoss>().lHandHP > 0)
            {
                transform.parent.parent.GetComponent<GlassesBoss>().TakeDamage(side, collision.GetComponent<LetterController>().damage);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("PlayerShot") && transform.parent.parent.GetComponent<GlassesBoss>().activated && transform.parent.parent.GetComponent<GlassesBoss>().lHandHP > 0)
            {
                transform.parent.parent.GetComponent<GlassesBoss>().TakeDamage(side, collision.GetComponent<ShotController>().damage);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("HomingShot") && transform.parent.parent.GetComponent<GlassesBoss>().activated && transform.parent.parent.GetComponent<GlassesBoss>().lHandHP > 0)
            {
                transform.parent.parent.GetComponent<GlassesBoss>().TakeDamage(side, collision.GetComponent<HomingShotController>().damage + 0.25f);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("PlayerShotBig") && transform.parent.parent.GetComponent<GlassesBoss>().activated && transform.parent.parent.GetComponent<GlassesBoss>().lHandHP > 0)
            {
                transform.parent.parent.GetComponent<GlassesBoss>().TakeDamage(side, collision.GetComponent<BigShotController>().damage);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Player") && !collision.GetComponent<PlayerHealth>().shield && collision == playerCol && collision.GetComponent<Typing>().modeSwitchCool <= 0)
        {
            collision.GetComponent<CircleCollider2D>().enabled = false;
            collision.GetComponent<PlayerHealth>().Die();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(side == "r")
        {
            if (collision.gameObject.CompareTag("laser") && transform.parent.parent.GetComponent<GlassesBoss>().activated && transform.parent.parent.GetComponent<GlassesBoss>().rHandHP > 0)
            {
                if (laserDamageCool <= 0)
                {
                    transform.parent.parent.GetComponent<GlassesBoss>().TakeDamage(side, 0.4f);
                    laserDamageCool = 0.001f;
                }
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("laser") && transform.parent.parent.GetComponent<GlassesBoss>().activated && transform.parent.parent.GetComponent<GlassesBoss>().lHandHP > 0)
            {
                if (laserDamageCool <= 0)
                {
                    transform.parent.parent.GetComponent<GlassesBoss>().TakeDamage(side, 0.4f);
                    laserDamageCool = 0.001f;
                }
            }
        }
    }
}
