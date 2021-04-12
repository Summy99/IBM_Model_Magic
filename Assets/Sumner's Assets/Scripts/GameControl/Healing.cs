using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healing : MonoBehaviour
{
    [SerializeField]
    private Sprite[] divisions;

    private Image sprite;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        sprite.sprite = divisions[player.GetComponent<PlayerHealth>().heal];

        switch (GameController.lives)
        {
            case 6:
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(430, 260);
                break;

            case 5:
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(374.6f, 259.5f);
                break;

            case 4:
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(332.1f, 259.5f);
                break;

            case 3:
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(289.6f, 259.5f);
                break;

            case 2:
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(247.1f, 259.5f);
                break;

            case 1:
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(204.6f, 259.5f);
                break;
        }
    }
}
