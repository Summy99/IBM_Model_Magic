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
    private GameObject unlockWord;
    private GameObject[] uLetters;

    [SerializeField]
    private GameObject wordBankL;

    [SerializeField]
    private GameObject wordBankC;

    [SerializeField]
    private GameObject wordBankR;

    [SerializeField]
    private GameObject lifeBar;

    [SerializeField]
    private GameObject slowBar;

    [SerializeField]
    private GameObject type;

    private int column = 1;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
        uLetters = GetAllChildren(unlockWord.transform);
    }

    void Update()
    {

        slowBar.GetComponent<Image>().fillAmount = player.GetComponent<Typing>().slowDown / Typing.maxSlowDown;

        type.SetActive(player.GetComponent<Typing>().mode == "typing");

        type.transform.Find("PendingWord").GetComponent<TextMeshProUGUI>().text = player.GetComponent<Typing>().word;

        keycapCounter.text = " x" + GameController.keycaps;

        lifeBar.GetComponent<Image>().sprite = lifeBarStates[GameController.lives];
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

    public void UpdateWordUnlock()
    {
        for(int i = 0; i < uLetters.Length; i++)
        {
            if(i < player.GetComponent<Typing>().wordToBeUnlocked.Length)
            {
                uLetters[i].GetComponent<TextMeshProUGUI>().text = "_";
            }
            else
            {
                uLetters[i].GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void AddLetter()
    {
        int iLetter = Mathf.FloorToInt(Random.Range(0, player.GetComponent<Typing>().wordToBeUnlocked.Length));

        while(uLetters[iLetter].GetComponent<TextMeshProUGUI>().text != "_")
        {
            iLetter = Mathf.FloorToInt(Random.Range(0, player.GetComponent<Typing>().wordToBeUnlocked.Length));
        }

        print(iLetter);

        uLetters[iLetter].GetComponent<TextMeshProUGUI>().text = player.GetComponent<Typing>().wordToBeUnlocked.ToCharArray()[iLetter].ToString();
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
