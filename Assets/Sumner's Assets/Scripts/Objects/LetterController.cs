using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : MonoBehaviour
{
    private Rigidbody2D rb;

    public int damage = 1;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.up * 75;
    }

    
    void Update()
    {
        
    }
}