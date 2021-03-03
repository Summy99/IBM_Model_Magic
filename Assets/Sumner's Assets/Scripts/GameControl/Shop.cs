using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public float time = 30f;

    [SerializeField]
    private GameObject shopOptions;

    [SerializeField]
    private GameObject prices;

    [SerializeField]
    private AudioClip theme;

    private GameObject gc;

    [SerializeField]
    private TextMeshProUGUI message;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
        if(time > 0 && gc.GetComponent<Waves>().shop)
        {
            time -= Time.deltaTime;
            message.text = Mathf.FloorToInt(time).ToString();
        }

        if(time <= 0 && gc.GetComponent<Waves>().shop)
        {
            CloseShop();
        }
    }

    public void OpenShop()
    {
        shopOptions.SetActive(true);
        prices.SetActive(true);

        time = 30f;

        for(int i = 0; i < shopOptions.transform.childCount; i++)
        {
            shopOptions.transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < prices.transform.childCount; i++)
        {
            prices.transform.GetChild(i).gameObject.SetActive(true);
        }

        time = 30f;
    }

    public void CloseShop()
    {
        message.text = "";
        shopOptions.SetActive(false);
        prices.SetActive(false);
        gc.GetComponent<Waves>().shop = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = theme;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
    }
}
