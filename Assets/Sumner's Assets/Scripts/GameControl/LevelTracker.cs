using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{
    public static int LevelToLoad = 0;
    void Start()
    {
        StartCoroutine("LoadLevel", LevelToLoad);
    }
    private IEnumerator LoadLevel(int level)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(level);
    }
}
