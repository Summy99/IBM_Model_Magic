using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    private GameController gc;
    private GameObject player;
    public static bool started = false;

    [SerializeField]
    private Sprite[] lifeBarStates;

    [SerializeField]
    private GameObject letterHolder;

    [SerializeField]
    private TextMeshProUGUI keycapCounter;

    [SerializeField]
    private GameObject wordBankL;

    [SerializeField]
    private GameObject wordBankC;

    [SerializeField]
    private GameObject wordBankR;

    [SerializeField]
    private GameObject lifeBar;
    private GameObject[] letterUI;

    [SerializeField]
    private GameObject slowBar;

    [SerializeField]
    private GameObject type;

    private int column = 1;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
        letterUI = GetAllChildren(letterHolder.transform);
    }

    void Update()
    {
        lifeBar.GetComponent<Image>().sprite = lifeBarStates[GameController.lives];

        for(int i = 0; i < letterUI.Length; i++)
        {
            if (gc.letters[i])
            {
                letterUI[i].SetActive(true);
            }
            else
            {
                letterUI[i].SetActive(false);
            }
        }

        slowBar.GetComponent<Image>().fillAmount = player.GetComponent<Typing>().slowDown / Typing.maxSlowDown;

        type.SetActive(player.GetComponent<Typing>().mode == "typing");

        type.transform.Find("PendingWord").GetComponent<TextMeshProUGUI>().text = player.GetComponent<Typing>().word;

        keycapCounter.text = " x" + GameController.keycaps;
    }

    public void AddWord(string word)
    {
        switch (column)
        {
            case 1:
                wordBankL.GetComponent<TextMeshProUGUI>().text += "\n\n" + word;
                column++;
                break;

            case 2:
                wordBankC.GetComponent<TextMeshProUGUI>().text += "\n\n" + word;
                column++;
                break;

            case 3:
                wordBankR.GetComponent<TextMeshProUGUI>().text += "\n\n" + word;
                column = 1;
                break;
        }
    }

    private GameObject[] GetAllChildren(Transform parent)
    {
        GameObject[] children = new GameObject[parent.childCount];

        for(int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i).gameObject;
        }

        return children;
    }
}
