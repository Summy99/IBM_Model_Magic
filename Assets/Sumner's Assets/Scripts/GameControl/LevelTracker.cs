using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI loadMessage;

    private string text = "LOADING...";
    private bool animRunning = false;
    public static int LevelToLoad = 0;
    void Start()
    {
        loadMessage.text = "";
        StartCoroutine("LoadAnim");
        StartCoroutine("LoadLevel", LevelToLoad);
    }

    private void Update()
    {
        if(!animRunning && loadMessage.text == text)
        {
            loadMessage.text = "";
            StartCoroutine("LoadAnim");
        }

        print(animRunning);
    }

    private IEnumerator LoadAnim()
    {
        animRunning = true;
        foreach(char c in text)
        {
            loadMessage.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        animRunning = false;
    }

    private IEnumerator LoadLevel(int level)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(level);
    }
}
