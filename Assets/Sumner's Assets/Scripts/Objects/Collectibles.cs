﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;

    public string type = "slow";

    private bool activated = false;

    void Start()
    {
        activated = false;

        float x = 0;

        rb = gameObject.GetComponent<Rigidbody2D>();
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
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -10f, 999));
        }

        if (activated)
        {
            rb.gravityScale = 0;
            rb.AddForce((player.transform.position - transform.position).normalized * 500);
        }

        if(Vector2.Distance(transform.position, player.transform.position) <= 10)
        {
            activated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && type == "slow")
        {
            collision.gameObject.GetComponent<Typing>().slowDown += 0.1f * collision.gameObject.GetComponent<Typing>().maxSlowDown;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && type == "keycap")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().keycaps++;
            Destroy(gameObject);
        }
    }
}