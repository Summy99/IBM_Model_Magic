using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelnest.BulletML;

public class PoopBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject[] topSpawns;
    [SerializeField] private GameObject spawnerHolder;
    [SerializeField] private GameObject poopWallPrefab;
    private BulletSourceScript bml;
    private AudioSource plyrsrc;

    [SerializeField]
    private AudioClip hit;

    private bool activated = false;
    private bool switching = false;
    private string direction = "right";
    public float moveSpeed = 10f;
    public float health = 500f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bml = gameObject.GetComponent<BulletSourceScript>();
        plyrsrc = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        spawnerHolder = GameObject.Find("SpawnPoints");

        topSpawns = GetAllChildren(spawnerHolder.transform.Find("Top"));
    }

    void Update()
    {
        if (!activated)
        {
            rb.velocity = -(transform.up * moveSpeed);

            if(transform.position.y <= 24)
            {
                activated = true;
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            switch (direction)
            {
                case "right":
                    rb.velocity = transform.right * moveSpeed;
                    break;

                case "left":
                    rb.velocity = -(transform.right * moveSpeed);
                    break;

                case "stop":
                    rb.velocity = Vector2.zero;
                    break;
            }

            if (transform.position.x >= 15 && direction == "right")
            {
                direction = "left";
            }

            if (transform.position.x <= -57.5f && direction == "left")
            {
                direction = "right";
            }

            if (bml.IsEnded && !switching && GameObject.FindGameObjectsWithTag("Bullet").Length == 0)
            {
                StartCoroutine("SwitchAttacks");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Letter"))
        {
            TakeDamage(collision.gameObject.GetComponent<ShotController>().damage);
            Destroy(collision.gameObject);
        }

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
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        plyrsrc.PlayOneShot(hit, 0.5f);

        if (health <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        GameController.level = 3;
        LevelTracker.LevelToLoad = 3;
        SceneManager.LoadScene(4);
    }

    private IEnumerator SwitchAttacks()
    {
        switching = true;

        direction = "stop";

        for (int i = 0; i < Mathf.FloorToInt(Random.Range(5, 13)); i++)
        {
            GameObject p = Instantiate(poopWallPrefab, topSpawns[Mathf.FloorToInt(Random.Range(0, topSpawns.Length))].transform.position, Quaternion.identity);
            Destroy(p, 5f);

            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        }

        bml.Initialize();
        direction = "right";
        switching = false;
    }

    private GameObject[] GetAllChildren(Transform parent)
    {
        GameObject[] children = new GameObject[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i).gameObject;
        }

        return children;
    }
}