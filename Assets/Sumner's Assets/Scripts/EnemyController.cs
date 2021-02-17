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

    void Start()
    {
        shooting = Mathf.FloorToInt(Random.Range(0, 2));

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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerShot"))
        {
            TakeDamage(collision.gameObject.GetComponent<ShotController>().damage);
            Destroy(collision.gameObject);
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
            int pickup = Mathf.FloorToInt(Random.Range(0, 10));

            switch (pickup)
            {
                case 0:
                    break;

                case 1:
                    break;

                case 2:
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
                    break;

                case 7:
                    break;

                case 8:
                    break;

                case 9:
                    Instantiate(slowCollectible, transform.position, Quaternion.identity);
                    break;
            }
            sfx.PlayOneShot(death, 0.5f);
            Destroy(gameObject);
        }
    }
}
