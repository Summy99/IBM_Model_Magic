using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : MonoBehaviour
{
    private GameObject player;

    public float damage = 3;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        gameObject.GetComponent<SpriteRenderer>().sprite = player.GetComponent<Typing>().letterSprites[Mathf.FloorToInt(Random.Range(0, player.GetComponent<Typing>().letterSprites.Length))];
    }

    void Update()
    {

    }
}
