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
    private GameObject enemy3Prefab;

    [SerializeField]
    private GameObject bossPrefab;

    [SerializeField]
    private GameObject spawnerHolder;

    private GameObject[] topSpawns, bottomSpawns, rightSpawns, leftSpawns;

    private GameController gc;
    private GameObject player;

    public int wave = 0;
    public bool shop = false;
    private bool waveRunning = false;
    private bool wave7PartTwoStarted = false;
    private bool waveIncrementing = false;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");

        topSpawns = GetAllChildren(spawnerHolder.transform.Find("Top"));
        rightSpawns = GetAllChildren(spawnerHolder.transform.Find("Right"));
        bottomSpawns = GetAllChildren(spawnerHolder.transform.Find("Bottom"));
        leftSpawns = GetAllChildren(spawnerHolder.transform.Find("Left"));
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && !waveIncrementing && !waveRunning && !gc.tutorial && !shop)
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
                    StartCoroutine("Wave4");
                    break;

                case 5:
                    StartCoroutine("Wave5");
                    break;

                case 6:
                    if (!shop)
                    {
                        player.transform.position = new Vector3(-20, -43, 0);
                        shop = true;
                        gameObject.GetComponent<Shop>().OpenShop();
                    }
                    break;

                case 7:
                    StartCoroutine("Wave7");
                    break;

                case 8:
                    StartCoroutine("Wave8");
                    break;

                case 9:
                    StartCoroutine("Wave9");
                    break;

                case 10:
                    Instantiate(bossPrefab, topSpawns[10].transform.position, Quaternion.identity);
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
            GameObject e3 = Instantiate(enemy2Prefab, topSpawns[10].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 4;
            e2.GetComponent<EnemyController>().pattern = 5;
            e3.GetComponent<EnemyController>().pattern = 1;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    private IEnumerator Wave4()
    {
        waveRunning = true;

        for (int i = 0; i < 11; i++)
        {
            GameObject e1 = Instantiate(enemy1Prefab, rightSpawns[1].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy1Prefab, leftSpawns[2].transform.position, Quaternion.identity);
            GameObject e3 = Instantiate(enemy1Prefab, rightSpawns[3].transform.position, Quaternion.identity);
            GameObject e4 = Instantiate(enemy1Prefab, leftSpawns[4].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 7;
            e2.GetComponent<EnemyController>().pattern = 6;
            e3.GetComponent<EnemyController>().pattern = 7;
            e4.GetComponent<EnemyController>().pattern = 6;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    private IEnumerator Wave5()
    {
        waveRunning = true;

        for (int i = 0; i < 36; i++)
        {
            GameObject e1 = Instantiate(enemy1Prefab, topSpawns[Mathf.FloorToInt(Random.Range(1, 9))].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 1;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    // Wave6 is shop

    private IEnumerator Wave7()
    {
        waveRunning = true;

        if (!wave7PartTwoStarted)
        {
            StartCoroutine("Wave7PartTwo");
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject e1 = Instantiate(enemy3Prefab, topSpawns[10].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 8;

            yield return new WaitForSeconds(12f);
        }
        waveRunning = false;
    }

    private IEnumerator Wave7PartTwo()
    {
        wave7PartTwoStarted = true;

        for (int i = 0; i < 36; i++)
        {
            GameObject e1 = enemy1Prefab;
            GameObject e2 = enemy2Prefab;

            if (i % 2 == 0)
            {
                if (Mathf.FloorToInt(Random.Range(0, 2)) == 0)
                {
                    e1 = Instantiate(enemy1Prefab, rightSpawns[Mathf.FloorToInt(Random.Range(0, 5))].transform.position, Quaternion.identity);
                }
                else
                {
                    e2 = Instantiate(enemy1Prefab, leftSpawns[Mathf.FloorToInt(Random.Range(0, 5))].transform.position, Quaternion.identity);
                }
            }
            else
            {
                if (Mathf.FloorToInt(Random.Range(0, 2)) == 0)
                {
                    e1 = Instantiate(enemy2Prefab, rightSpawns[Mathf.FloorToInt(Random.Range(0, 5))].transform.position, Quaternion.identity);
                }
                else
                {
                    e2 = Instantiate(enemy2Prefab, leftSpawns[Mathf.FloorToInt(Random.Range(0, 5))].transform.position, Quaternion.identity);
                }
            }

            e1.GetComponent<EnemyController>().pattern = 9;
            e2.GetComponent<EnemyController>().pattern = 10;

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1);
        wave7PartTwoStarted = false;
    }

    private IEnumerator Wave8()
    {
        waveRunning = true;

        List<GameObject> enemies = new List<GameObject>();

        for(int i = 1; i < 9; i++)
        {
            if(i != 3)
            enemies.Add(Instantiate(enemy1Prefab, topSpawns[i].transform.position, Quaternion.identity));
        }

        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<EnemyController>().pattern = 11;
        }

        enemies.Clear();
        yield return new WaitForSeconds(1);

        for (int i = 1; i < 9; i++)
        {
            if (i != 7)
                enemies.Add(Instantiate(enemy1Prefab, topSpawns[i].transform.position, Quaternion.identity));
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<EnemyController>().pattern = 11;
        }

        enemies.Clear();
        yield return new WaitForSeconds(1);

        for (int i = 1; i < 9; i++)
        {
            if (i != 5)
                enemies.Add(Instantiate(enemy1Prefab, topSpawns[i].transform.position, Quaternion.identity));
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<EnemyController>().pattern = 11;
        }

        enemies.Clear();
        yield return new WaitForSeconds(1);

        waveRunning = false;
    }

    private IEnumerator Wave9()
    {
        waveRunning = true;

        for (int i = 0; i < 2; i++)
        {
            GameObject e1 = enemy1Prefab;
            GameObject e2 = enemy1Prefab;

            if(i % 2 == 0)
            {
                e1 = Instantiate(enemy3Prefab, leftSpawns[1].transform.position, Quaternion.identity);
            }
            else
            {
                e2 = Instantiate(enemy3Prefab, rightSpawns[2].transform.position, Quaternion.identity);
            }

            e1.GetComponent<EnemyController>().pattern = 6;
            e2.GetComponent<EnemyController>().pattern = 7;

            yield return new WaitForSeconds(2);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    // wave 10 is boss

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
