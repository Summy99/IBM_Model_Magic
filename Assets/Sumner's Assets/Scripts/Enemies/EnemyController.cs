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
    private GameObject slowCollectible, keycapCollectible, healthCollectible;

    [SerializeField]
    private GameObject yParticles;

    private CircleCollider2D playerCol;

    public float health = 3;

    public int shooting = 0;
    public int pattern = 0;
    private float laserDamageCool = 0;
    public float lifetime = 0;
    public float timeToShoot = 0;
    public int type = 0;
    public bool type2Shoot = false;
    public bool initialized = false;
    public float moveSpeed = 10;
    private bool boom = false;
    private bool turnt = false;
    private bool ringed = false;
    private bool rung = false;

    private bool flashing = false;
    private float distanceTraveled = 0f;
    private string direction = "down";
    [SerializeField]
    private Vector3 initialPosition = Vector3.zero;

    private Vector3 playerPos;

    void Start()
    {
        playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();

        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().dead)
        {
            gameObject.SetActive(false);
        }

        shooting = Mathf.FloorToInt(Random.Range(0, 2));
        initialPosition = transform.position;

        timeToShoot = Random.Range(2, 4);
        lifetime = 0;
        sfx = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        if(type == 6 || type == 7)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Typing>().touhou)
        {
            if (gameObject.GetComponent<Animator>())
            {
                gameObject.GetComponent<Animator>().enabled = false;
            }
            else if (gameObject.transform.Find("Sprite").GetComponent<Animator>())
            {
                gameObject.transform.Find("Sprite").GetComponent<Animator>().enabled = false;
            }

            if (gameObject.GetComponent<SpriteRenderer>())
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<Typing>().yukkuriFlan;
            }
            else
            {
                gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<Typing>().yukkuriFlan;
            }
        }
    }

    private void Update()
    {
        if(laserDamageCool > 0)
        {
            laserDamageCool -= Time.deltaTime;
        }

        if(type != 7 && type != 8)
        {
            if (gameObject.GetComponent<SpriteRenderer>())
            {
                transform.Find("Flash").GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                transform.Find("Flash").GetComponent<SpriteRenderer>().sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;
            }
        }

        if (gameObject.transform.position.y > 56 || gameObject.transform.position.y < -56 || gameObject.transform.position.x > 26 || gameObject.transform.position.x < -66)
        {
            Destroy(gameObject);
        }

        lifetime += Time.deltaTime;

        if(type == 2 && lifetime >= timeToShoot && !initialized && shooting == 1 && pattern != 22)
        {
            initialized = true;
            gameObject.GetComponent<BulletSourceScript>().xmlFile = bulletmlScripts[1];
            gameObject.GetComponent<BulletSourceScript>().Initialize();
        }

        if (type == 2 && !initialized && (pattern == 22 || type2Shoot))
        {
            initialized = true;
            gameObject.GetComponent<BulletSourceScript>().xmlFile = bulletmlScripts[6];
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

        if (type == 5 && !initialized)
        {
            initialized = true;
            gameObject.GetComponent<BulletSourceScript>().xmlFile = bulletmlScripts[4];
            gameObject.GetComponent<BulletSourceScript>().Initialize();
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

            case 14:
                if(transform.position.y < 45)
                {
                    rb.velocity = transform.up * moveSpeed;
                }
                else
                {
                    rb.velocity = transform.right * moveSpeed;
                }
                break;

            case 15:
                if (transform.position.y < 45)
                {
                    rb.velocity = transform.up * moveSpeed;
                }
                else
                {
                    rb.velocity = -(transform.right * moveSpeed);
                }
                break;

            case 16:
                rb.velocity = -(transform.right * moveSpeed);

                if (transform.rotation.eulerAngles.z < 180)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 0.5f));
                }
                break;

            case 17:
                rb.velocity = transform.right * moveSpeed;

                if (transform.rotation.eulerAngles.z > -180)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 0.5f));
                }
                break;

            case 18:
                moveSpeed = 20;
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -15));
                rb.velocity = -(transform.up * moveSpeed);
                break;

            case 19:
                moveSpeed = 20;
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 15));
                rb.velocity = -(transform.up * moveSpeed);
                break;

            case 20:
                rb.velocity = -((initialPosition - playerPos).normalized * moveSpeed);
                break;

            case 21:
                if(transform.position.y > 9)
                {
                    rb.velocity = -((initialPosition - playerPos).normalized * moveSpeed);
                }
                else if(!boom)
                {
                    boom = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine("Skull");
                }
                break;

            case 22:
                rb.velocity = -(transform.right * moveSpeed);
                break;

            case 23:
                if(transform.position.y > -15 && !turnt)
                {
                    rb.velocity = -(transform.up * moveSpeed);
                }
                else if(!turnt)
                {
                    if (transform.position.x < -35)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -30));
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
                    }

                    turnt = true;
                }

                if (turnt)
                {
                    rb.velocity = transform.up * moveSpeed;
                }
                break;

            case 24:
                if(transform.position.y > 12 && !rung)
                {
                    rb.velocity = -(transform.up * moveSpeed);
                }
                else if (!ringed)
                {
                    rb.velocity = Vector2.zero;
                    ringed = true;
                    StartCoroutine("RingFire");
                }
                if (rung)
                {
                    rb.velocity = transform.up * moveSpeed;
                }
                break;

            case 25:
                moveSpeed = 25;
                rb.velocity = transform.up * moveSpeed;
                break;

            case 26:
                rb.velocity = transform.up * moveSpeed;
                break;

            case 27:
                if (transform.position.x > 5)
                {
                    rb.velocity = -((initialPosition - playerPos).normalized * moveSpeed);
                }
                else if (!boom)
                {
                    boom = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine("Skull");
                }
                break;

            case 28:
                if (transform.position.y < -25)
                {
                    rb.velocity = -((initialPosition - playerPos).normalized * moveSpeed);
                }
                else if (!boom)
                {
                    boom = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine("Skull");
                }
                break;

            case 29:
                if (transform.position.x < -45)
                {
                    rb.velocity = -((initialPosition - playerPos).normalized * moveSpeed);
                }
                else if (!boom)
                {
                    boom = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine("Skull");
                }
                break;

            case 30:
                if (transform.position.y > 25)
                {
                    rb.velocity = -((initialPosition - playerPos).normalized * moveSpeed);
                }
                else if (!boom)
                {
                    boom = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine("Skull");
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

    private IEnumerator Skull()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<BulletSourceScript>().xmlFile = bulletmlScripts[5];
        gameObject.GetComponent<BulletSourceScript>().Initialize();
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    private IEnumerator RingFire()
    {
        gameObject.GetComponent<BulletSourceScript>().xmlFile = bulletmlScripts[6];
        gameObject.GetComponent<BulletSourceScript>().Initialize();
        yield return new WaitForSeconds(1);
        rung = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerShot"))
        {
            TakeDamage(collision.gameObject.GetComponent<ShotController>().damage);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("HomingShot"))
        {
            TakeDamage(collision.gameObject.GetComponent<HomingShotController>().damage);
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

        if (collision.gameObject.CompareTag("Player") && !collision.GetComponent<PlayerHealth>().shield && collision == playerCol)
        {
            collision.GetComponent<CircleCollider2D>().enabled = false;
            collision.GetComponent<PlayerHealth>().Die();
        }
        else if (collision.gameObject.CompareTag("Player") && collision.GetComponent<PlayerHealth>().shield && collision == playerCol)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("laser"))
        {
            if(laserDamageCool <= 0)
            {
                TakeDamage(0.25f);
                laserDamageCool = 0.01f;
            }
        }
    }

    //private IEnumerator TurnUp()
    //{

    //}

    public void TakeDamage(float damage)
    {
        if (!flashing && type != 7 && type != 8)
        {
            StartCoroutine("Flash");
        }

        health -= damage;

        if(health <= 0)
        {
            GameObject p = Instantiate(yParticles, transform.position, Quaternion.identity);

            Destroy(p, 1f);

            if(GameController.Level != 3)
            {
                if (type == 1 || type == 2 || type == 4 || type == 6 || type == 7 || type == 8)
                {
                    int pickup = Mathf.FloorToInt(Random.Range(0, 20));

                    switch (pickup)
                    {
                        case 0:
                            Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                            break;

                        case 1:
                            Instantiate(healthCollectible, transform.position, Quaternion.identity);
                            break;

                        case 2:
                            Instantiate(healthCollectible, transform.position, Quaternion.identity);
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
                            break;
                    }
                }

                if (type == 3 || type == 5 || type == 9)
                {
                    Instantiate(healthCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                }
            }
            else
            {
                if (type == 1 || type == 2 || type == 4 || type == 6 || type == 7 || type == 8)
                {
                    int pickup = Mathf.FloorToInt(Random.Range(0, 20));

                    switch (pickup)
                    {
                        case 0:
                            Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                            break;

                        case 1:
                            Instantiate(healthCollectible, transform.position, Quaternion.identity);
                            break;

                        case 2:
                            Instantiate(healthCollectible, transform.position, Quaternion.identity);
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
                            Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                            break;

                        case 6:
                            Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                            Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                            Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                            break;

                        case 7:
                            Instantiate(keycapCollectible, transform.position, Quaternion.identity);
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
                            Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                            break;

                        case 12:
                            Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                            break;

                        case 13:
                            Instantiate(keycapCollectible, transform.position, Quaternion.identity);
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
                            break;
                    }
                }

                if (type == 3 || type == 5 || type == 9)
                {
                    Instantiate(healthCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                    Instantiate(keycapCollectible, transform.position, Quaternion.identity);
                }
            }

            sfx.PlayOneShot(death, 0.5f);
            Destroy(gameObject);
        }
    }

    public IEnumerator Flash()
    {
        flashing = true;
        transform.Find("Flash").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.Find("Flash").gameObject.SetActive(false);
        flashing = false;
    }
}