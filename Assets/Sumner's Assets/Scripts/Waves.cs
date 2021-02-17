using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy1Prefab;

    [SerializeField]
    private GameObject enemy2Prefab;

    [SerializeField]
    private GameObject spawnerHolder;

    private GameObject[] topSpawns, bottomSpawns, rightSpawns, leftSpawns;

    private GameController gc;

    public int wave = 0;
    private bool waveRunning = false;
    private bool waveIncrementing = false;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        topSpawns = GetAllChildren(spawnerHolder.transform.Find("Top"));
        rightSpawns = GetAllChildren(spawnerHolder.transform.Find("Right"));
        bottomSpawns = GetAllChildren(spawnerHolder.transform.Find("Bottom"));
        leftSpawns = GetAllChildren(spawnerHolder.transform.Find("Left"));
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && !waveIncrementing && !waveRunning && !gc.tutorial)
        {
            wave++;
        }

        if (!waveRunning && GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            switch (wave)
            {
                case 1:
                    StartCoroutine("Wave1");
                    break;

                case 2:
                    StartCoroutine("Wave2");
                    break;

                case 3:
                    StartCoroutine("Wave3");
                    break;

                case 4:
                    wave = 0;
                    break;
            }
        }
    }

    private IEnumerator Wave1()
    {
        waveRunning = true;

        for(int i = 0; i < 11; i++)
        {
            GameObject e1 = Instantiate(enemy1Prefab, topSpawns[2].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy1Prefab, topSpawns[7].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 1;
            e2.GetComponent<EnemyController>().pattern = 1;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    private IEnumerator Wave2()
    {
        waveRunning = true;

        for (int i = 0; i < 11; i++)
        {
            GameObject e1 = Instantiate(enemy2Prefab, rightSpawns[5].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy2Prefab, leftSpawns[5].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 2;
            e2.GetComponent<EnemyController>().pattern = 3;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    private IEnumerator Wave3()
    {
        waveRunning = true;

        for (int i = 0; i < 11; i++)
        {
            GameObject e1 = Instantiate(enemy1Prefab, rightSpawns[3].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy1Prefab, leftSpawns[3].transform.position, Quaternion.identity);
            GameObject e3 = Instantiate(enemy2Prefab, topSpawns[6].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 4;
            e2.GetComponent<EnemyController>().pattern = 5;
            e3.GetComponent<EnemyController>().pattern = 1;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    private GameObject[] GetAllChildren(Transform parent)
    {
        GameObject[] children = new GameObject[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i).gameObject;
        }

        return children;
    }
}
