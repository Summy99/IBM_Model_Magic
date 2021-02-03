using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        rb.AddTorque(-720);
        rb.velocity = Vector2.up * 75;
    }

    void Update()
    {

    }
}
