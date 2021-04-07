using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite[] sprites;

    void Start()
    {
        StartCoroutine("Flap");
    }

    private IEnumerator Flap()
    {
        while (true)
        {
            sprite.sprite = sprites[0];
            yield return new WaitForSeconds(0.1f);
            sprite.sprite = sprites[1];
            yield return new WaitForSeconds(0.1f);
        }
    }
}
