using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Meme : MonoBehaviour
{
    private string direction = "left";

    void Start()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
            transform.parent.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (direction == "left")
        {
            if(gameObject.GetComponent<RectTransform>().rotation.eulerAngles.z < 345 && gameObject.GetComponent<RectTransform>().rotation.eulerAngles.z > 340)
            {
                direction = "right";
            }

            gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, gameObject.GetComponent<RectTransform>().rotation.eulerAngles.z - 1);
        }

        if(direction == "right")
        {
            if (gameObject.GetComponent<RectTransform>().rotation.eulerAngles.z > 25 && gameObject.GetComponent<RectTransform>().rotation.eulerAngles.z < 30)
            {
                direction = "left";
            }

            gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, gameObject.GetComponent<RectTransform>().rotation.eulerAngles.z + 1);
        }
    }
}
