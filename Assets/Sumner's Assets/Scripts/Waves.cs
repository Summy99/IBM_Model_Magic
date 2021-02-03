using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy1Prefab;

    [SerializeField]
    private GameObject spawnerHolder;

    private GameObject[] topSpawns, bottomSpawns, rightSpawns, leftSpawns;

    public int wave = 1;
    private bool waveRunning = false;
    private bool waveIncrementing = false;

    void Start()
    {
        topSpawns = GetAllChildren(spawnerHolder.transform.Find("Top"));
        rightSpawns = GetAllChildren(spawnerHolder.transform.Find("Right"));
        bottomSpawns = GetAllChildren(spawnerHolder.transform.Find("Bottom"));
        leftSpawns = GetAllChildren(spawnerHolder.transform.Find("Left"));
    }

    void Update()
    {
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
            }
        }

        if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && !waveIncrementing && !waveRunning)
        {
            StartCoroutine("IncrementWave");
        }
    }

    private void IncrementWave()
    {
        waveIncrementing = true;
        wave++;
        waveIncrementing = false;
    }

    private IEnumerator Wave1()
    {
        waveRunning = true;

        for(int i = 0; i < 11; i++)
        {
            GameObject e1 = Instantiate(enemy1Prefab, topSpawns[2].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy1Prefab, topSpawns[7].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().wave = wave;
            e2.GetComponent<EnemyController>().wave = wave;

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
            GameObject e1 = Instantiate(enemy1Prefab, topSpawns[1].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy1Prefab, topSpawns[8].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().wave = wave;
            e2.GetComponent<EnemyController>().wave = wave;

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
            GameObject e1 = Instantiate(enemy1Prefab, topSpawns[4].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy1Prefab, topSpawns[5].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().wave = wave;
            e2.GetComponent<EnemyController>().wave = wave;

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
