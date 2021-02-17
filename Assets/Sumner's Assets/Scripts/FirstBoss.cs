using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelnest.BulletML;

public class FirstBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BulletSourceScript bml;

    [SerializeField]
    private TextAsset[] patterns;

    private int attack = 0;
    public int health = 300;
    private bool activated = false;

    private bool walkFinished = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(!activated && gameObject.transform.position.y > 37)
        {
            rb.velocity = -(transform.up * 10);
        }

        if(gameObject.transform.position.y <= 37)
        {
            anim.SetBool("Stopped", true);
            rb.velocity = Vector2.zero;
            activated = true;
        }

        if (activated && (bml.IsEnded || walkFinished))
        {

        }
    }
}
