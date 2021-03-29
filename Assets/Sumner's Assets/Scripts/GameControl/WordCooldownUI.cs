using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordCooldownUI : MonoBehaviour
{
    private Image i;
    private RectTransform r;

    public int wordIndex = 0;

    private int length = 5;
    private Word word;

    void Start()
    {
        i = gameObject.GetComponent<Image>();
        r = gameObject.GetComponent<RectTransform>();
        word = GameController.Words[wordIndex];

        length = word.Name.Length;
        r.sizeDelta = new Vector2(length * 10, r.sizeDelta.y);
    }

    void Update()
    {
        i.fillAmount = GameController.Words[wordIndex].CurCool / GameController.Words[wordIndex].MaxCool;
    }
}
