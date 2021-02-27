using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelnest.BulletML;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource sfx;
    [SerializeField]
    private AudioClip death;

    [SerializeField]
    private TextAsset[] bulletmlScripts;

    [SerializeField]
    private GameObject slowCollectible;

    [SerializeField]
    private GameObject keycapCollectible;

    public float health = 3;

    public int shooting = 0;
    public int pattern = 0;
    public float lifetime = 0;
    public float timeToShoot = 0;
    public int type = 0;
    public bool initialized = false;
    public float moveSpeed = 10;

    private float distanceTraveled = 0f;
    private string direction = "down";
    [SerializeField]
    private Vector2 initialPosition = Vector2.zero;
    void Start()
    {
        shooting = Mathf.FloorToInt(Random.Range(0, 2));
        initialPosition = transform.position;

        timeToShoot = Random.Range(2, 4);
        lifetime = 0;
        sfx = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(gameObject.transform.position.y > 56 || gameObject.transform.position.y < -56 || gameObject.transform.position.x > 26 || gameObject.transform.position.x < -66)
        {
            Destroy(gameObject);
        }

        lifetime += Time.deltaTime;

        if(type == 2 && lifetime >= timeToShoot && !initialized && shooting == 1)
        {
            initialized = true;
            gameObject.GetComponent<BulletSourceScript>().xmlFile = bulletmlScripts[1];
            gameObject.GetComponent<BulletSourceScript>().Initialize();
        }

        if (type == 3 && lifetime >= 2 && !initialized)
        {
            initialized = true;
            gameObject.GetComponent<BulletSourceScript>().xmlFile = bulletmlScripts[2];
            gameObject.GetComponent<BulletSourceScript>().Initialize();
        }

        if (type == 4 && gameObject.GetComponent<BulletSourceScript>().IsEnded)
        {
            gameObject.GetComponent<BulletSourceScript>().xmlFile = bulletmlScripts[0];
        }
    }

    void FixedUpdate()
    {
        switch (pattern)
        {
            case 1:
                rb.velocity = Vector2.down * moveSpeed;
                break;

            case 2:
                if (transform.position.x > 0)
                {
                    rb.velocity = -transform.right * moveSpeed;
                }
                else
                {
                    rb.velocity = -transform.right * moveSpeed;

                    if (transform.rotation.eulerAngles.z > -70)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 1));
                    }
                }
                break;

            case 3:
                if (transform.position.x < -40)
                {
                    rb.velocity = transform.right * moveSpeed;
                }
                else
                {
                    rb.velocity = transform.right * moveSpeed;

                    if (transform.rotation.eulerAngles.z > -70)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 1));
                    }
                }
                break;

            case 4:
                if(transform.position.x > -15)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 45));
                    rb.velocity = -(transform.right * moveSpeed);
                }
                else
                {
                    transform.rotation = Quaternion.identity;

                    rb.velocity = transform.up * moveSpeed;
                }

                break;

            case 5:
                if (transform.position.x < -25)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -45));
                    rb.velocity = (transform.right * moveSpeed);
                }
                else
                {
                    transform.rotation = Quaternion.identity;

                    rb.velocity = transform.up * moveSpeed;
                }

                break;

            case 6:
                rb.velocity = transform.right * moveSpeed;
                break;

            case 7:
                rb.velocity = -(transform.right * moveSpeed);
                break;

            case 8:
                if (lifetime < 2)
                {
                    rb.velocity = -(transform.up * moveSpeed);
                }

                if(lifetime >= 2 && lifetime <= 10)
                {
                    rb.velocity = Vector2.zero;
                }

                if(lifetime > 10)
                {
                    rb.velocity = transform.up * moveSpeed;
                }

                break;

            case 9:
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 45));
                rb.velocity = -(transform.right * moveSpeed);
                break;

            case 10:
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -45));
                rb.velocity = (transform.right * moveSpeed);
                break;

            case 11:
                rb.velocity = -(transform.up * moveSpeed * 2);
                break;

            case 12:
                if(direction == "down")
                {
                    if(distanceTraveled <= 10f)
                    {
                        rb.velocity = -(transform.up * moveSpeed);
                        distanceTraveled = Vector2.Distance(initialPosition, transform.position);
                    }
                    else
                    {
                        direction = "shooting";
                        distanceTraveled = 0;
                        StartCoroutine("Vomit");
                    }
                }

                if(direction == "right")
                {
                    if (distanceTraveled <= 10f)
                    {
                        rb.velocity = transform.right * moveSpeed;
                        distanceTraveled += Vector2.Distance(initialPosition, transform.position);
                        initialPosition = transform.position;
                    }
                    else
                    {
                        direction = "shooting";
                        distanceTraveled = 0;
                        StartCoroutine("VomitDown");
                    }
                }
                break;

            case 13:
                if (direction == "down")
                {
                    if (distanceTraveled <= 10f)
                    {
                        rb.velocity = -(transform.up * moveSpeed);
                        distanceTraveled = Vector2.Distance(initialPosition, transform.position);
                    }
                    else
                    {
                        direction = "shooting";
                        distanceTraveled = 0;
                        StartCoroutine("Vomit");
                    }
                }

                if (direction == "left")
                {
                    if (distanceTraveled <= 10f)
                    {
                        rb.velocity = -(transform.right * moveSpeed);
                        distanceTraveled += Vector2.Distance(initialPosition, transform.position);
                        initialPosition = transform.position;
                    }
                    else
                    {
                        direction = "shooting";
                        distanceTraveled = 0;
                        StartCoroutine("VomitDown");
                    }
                }
                break;
        }
    }

    private IEnumerator Vomit()
    {
        rb.velocity = Vector2.zero;
        gameObject.GetComponent<BulletSourceScript>().xmlFile = bulletmlScripts[3];
        gameObject.GetComponent<BulletSourceScript>().Initialize();
        yield return new WaitForSeconds(0.5f);
        initialPosition = transform.position;
        if(pattern == 12)
        {
            direction = "right";
        }

        if (pattern == 13)
        {
            direction = "left";
        }
    }

    private IEnumerator VomitDown()
    {
        rb.velocity = Vector2.zero;
        gameObject.GetComponent<BulletSourceScript>().xmlFile = bulletmlScripts[3];
        gameObject.GetComponent<BulletSourceScript>().Initialize();
        yield return new WaitForSeconds(0.5f);
        initialPosition = transform.position;
        direction = "down";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerShot"))
        {
            TakeDamage(collision.gameObject.GetComponent<ShotController>().damage);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PlayerShotBig"))
        {
            float damageToDeal = collision.gameObject.GetComponent<BigShotController>().damage;
            collision.gameObject.GetComponent<BigShotController>().damage -= health;
            TakeDamage(damageToDeal);
        }

        if (collision.gameObject.CompareTag("Letter"))
        {
            TakeDamage(collision.gameObject.GetComponent<LetterController>().damage);
            Destroy(collision.gameObject);
        }
    }

    //private IEnumerator TurnUp()
    //{

    //}

    public void TakeDamage(float damage)
    {
        GameObject col;

        health -= damage;

        if(health <= 0)
        {

            if(type == 1 || type == 2 || type == 4)
            {
                int pickup = Mathf.FloorToInt(Random.Range(0, 20));

                switch (pickup)
                {
                    case 0:
                        break;

                    case 1:
                        break;

                    case 2:
                        Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                        break;

                    case 3:
                        Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                        break;

                    case 4:
                        Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                        break;

                    case 5:
                        Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                        break;

                    case 6:
                        Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                        Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                        break;

                    case 7:
                        Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                        break;

                    case 8:
                        Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                        break;

                    case 9:
                        Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                        break;

                    case 10:
                        Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                        break;

                    case 11:
                        break;

                    case 12:
                        break;

                    case 13:
                        break;

                    case 14:
                        break;

                    case 15:
                        break;

                    case 16:
                        break;

                    case 17:
                        break;

                    case 18:
                        break;

                    case 19:
                        Instantiate(slowCollectible, transform.position, Quaternion.identity);
                        break;
                }
            }

            if (type == 3)
            {
                Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                Instantiate(slowCollectible, transform.position, Quaternion.identity);
            }
            sfx.PlayOneShot(death, 0.5f);
            Destroy(gameObject);
        }
    }
}
