using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelnest.BulletML;

public class ShotController : MonoBehaviour
{
    private GameObject player;

    public float damage = 3;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<BulletSourceScript>().xmlFile.name == "AutoFire")
        {
            damage = 1.5f;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = player.GetComponent<Typing>().letterSprites[Mathf.FloorToInt(Random.Range(0, player.GetComponent<Typing>().letterSprites.Length))];
    }

    void Update()
    {

    }
}
