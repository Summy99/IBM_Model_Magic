using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShotController : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;

    public GameObject target;

    public bool moving = false;
    public bool targeted = false;
    public float speed = 50f;
    public float damage = 0.5f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        rb.AddTorque(-100);

        gameObject.GetComponent<SpriteRenderer>().sprite = player.GetComponent<Typing>().letterSprites[Mathf.FloorToInt(Random.Range(0, player.GetComponent<Typing>().letterSprites.Length))];
    }

    void Update()
    {
        if(target == null)
        {
            GetTarget();
        }
    }

    private void FixedUpdate()
    {
        if (targeted)
        {
            if (!moving)
            {
                StartCoroutine("SetVelocity");
            }
        }
        else
        {
            rb.velocity = Vector2.up * speed;
        }
    }

    private void GetTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length > 0)
        {
            targeted = true;
            GameObject newTarget = enemies[0];
            float minDistance = 9999;

            foreach (GameObject e in enemies)
            {
                if (Vector2.Distance(player.transform.position, gameObject.transform.position) < minDistance)
                {
                    minDistance = Vector2.Distance(player.transform.position, gameObject.transform.position);
                    newTarget = e;
                }
            }

            target = newTarget;
        }
        else
        {
            targeted = false;
        }
    }

    private IEnumerator SetVelocity()
    {
        moving = true;
        while(target != null)
        {
            rb.velocity = -(transform.position - target.transform.position).normalized * speed;
            yield return new WaitForSeconds(0.1f);
        }
        moving = false;
    }
}
