using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;

    public float moveSpeed = 10;
    private float curSpeed = 10;

    private float maxY = 46;
    private float minY = -44;
    private float maxX = 17.5f;
    private float minX = -59.5f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float InputX = Input.GetAxis("Horizontal");
        float InputY = Input.GetAxis("Vertical");

        movement = new Vector2
            (
            InputX * curSpeed,
            InputY * curSpeed
            );

        if (Input.GetKey(KeyCode.LeftShift))
        {
            curSpeed = moveSpeed / 2;
        }
        else
        {
            curSpeed = moveSpeed;
        }

        gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, minX, maxX), Mathf.Clamp(gameObject.transform.position.y, minY, maxY), 0);
    }

    private void FixedUpdate()
    {
        rb.velocity = movement;
    }
}
