using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShotController : MonoBehaviour
{
    private Rigidbody2D rb;

    private GameObject player;

    public float damage = 20;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();

        gameObject.GetComponent<SpriteRenderer>().sprite = player.GetComponent<Typing>().letterSprites[Mathf.FloorToInt(Random.Range(0, player.GetComponent<Typing>().letterSprites.Length))];
        rb.AddTorque(-35);
    }

    void Update()
    {
        if(damage <= 0)
        {
            Destroy(gameObject);
        }

        transform.localScale = new Vector3((damage / 10) * 5, (damage / 10) * 5, (damage / 10) * 5);
    }
}
