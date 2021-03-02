using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy1Prefab, enemy2Prefab, enemy3Prefab, enemy4Prefab, enemy5Prefab, enemy6Prefab;

    [SerializeField]
    private GameObject bossPrefab, boss2Prefab;

    [SerializeField]
    private GameObject spawnerHolder;

    private GameObject[] topSpawns, bottomSpawns, rightSpawns, leftSpawns;

    private GameController gc;
    private GameObject player;

    [SerializeField]
    private AudioClip bosstheme, finalBossTheme;

    [SerializeField]
    private AudioClip shoptheme;

    public int wave = 0;
    public bool shop = false;
    private bool waveRunning = false;
    private bool wave7PartTwoStarted = false;
    private bool waveIncrementing = false;
    private bool bossSpawned = false;

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
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && !waveIncrementing && !waveRunning && !gc.tutorial && !shop && !bossSpawned)
        {
            wave++;
        }
        // level 1
        if (!waveRunning && GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && GameController.level == 1)
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
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = shoptheme;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
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
                    if (!bossSpawned)
                    {
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = bosstheme;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
                        Instantiate(bossPrefab, topSpawns[10].transform.position, Quaternion.identity);
                        bossSpawned = true;
                    }
                    break;
            }
        }
        // level 2
        if (!waveRunning && GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && GameController.level == 2)
        {
            switch (wave)
            {
                case 1:
                    StartCoroutine("Wave21");
                    break;

                case 2:
                    StartCoroutine("Wave22");
                    break;

                case 3:
                    StartCoroutine("Wave23");
                    break;

                case 4:
                    StartCoroutine("Wave24");
                    break;

                case 5:
                    StartCoroutine("Wave25");
                    break;

                case 6:
                    if (!shop)
                    {
                        player.transform.position = new Vector3(-20, -43, 0);
                        shop = true;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = shoptheme;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
                        gameObject.GetComponent<Shop>().OpenShop();
                    }
                    break;

                case 7:
                    StartCoroutine("Wave27");
                    break;

                case 8:
                    StartCoroutine("Wave28");
                    break;

                case 9:
                    StartCoroutine("Wave29");
                    break;

                case 10:
                    if (!bossSpawned)
                    {
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = bosstheme;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
                        Instantiate(boss2Prefab, topSpawns[10].transform.position, Quaternion.identity);
                        bossSpawned = true;
                    }
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

    // level 2 start
    private IEnumerator Wave21()
    {
        waveRunning = true;

        GameObject e1 = Instantiate(enemy4Prefab, topSpawns[1].transform.position, Quaternion.identity);
        GameObject e2 = Instantiate(enemy4Prefab, topSpawns[3].transform.position, Quaternion.identity);
        GameObject e3 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);
        GameObject e4 = Instantiate(enemy4Prefab, topSpawns[7].transform.position, Quaternion.identity);

        e1.GetComponent<EnemyController>().pattern = 12;
        e2.GetComponent<EnemyController>().pattern = 12;
        e3.GetComponent<EnemyController>().pattern = 13;
        e4.GetComponent<EnemyController>().pattern = 13;

        yield return new WaitForSeconds(4);

        GameObject e5 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);
        GameObject e6 = Instantiate(enemy4Prefab, topSpawns[5].transform.position, Quaternion.identity);
        GameObject e7 = Instantiate(enemy4Prefab, topSpawns[7].transform.position, Quaternion.identity);
        GameObject e8 = Instantiate(enemy4Prefab, topSpawns[8].transform.position, Quaternion.identity);
        GameObject e9 = Instantiate(enemy4Prefab, topSpawns[3].transform.position, Quaternion.identity);

        e5.GetComponent<EnemyController>().pattern = 13;
        e6.GetComponent<EnemyController>().pattern = 13;
        e7.GetComponent<EnemyController>().pattern = 13;
        e8.GetComponent<EnemyController>().pattern = 13;
        e9.GetComponent<EnemyController>().pattern = 12;

        yield return new WaitForSeconds(4);

        GameObject e10 = Instantiate(enemy4Prefab, topSpawns[3].transform.position, Quaternion.identity);
        GameObject e11 = Instantiate(enemy4Prefab, topSpawns[8].transform.position, Quaternion.identity);
        GameObject e12 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);

        e10.GetComponent<EnemyController>().pattern = 12;
        e11.GetComponent<EnemyController>().pattern = 13;
        e12.GetComponent<EnemyController>().pattern = 13;

        yield return new WaitForSeconds(1);

        waveRunning = false;
    }

    private IEnumerator Wave22()
    {
        waveRunning = true;

        GameObject e1 = Instantiate(enemy4Prefab, topSpawns[5].transform.position, Quaternion.identity);
        GameObject e2 = Instantiate(enemy4Prefab, topSpawns[4].transform.position, Quaternion.identity);
        GameObject e3 = Instantiate(enemy4Prefab, topSpawns[2].transform.position, Quaternion.identity);

        e1.GetComponent<EnemyController>().pattern = 13;
        e2.GetComponent<EnemyController>().pattern = 12;
        e3.GetComponent<EnemyController>().pattern = 12;

        yield return new WaitForSeconds(4);

        GameObject e4 = Instantiate(enemy4Prefab, topSpawns[7].transform.position, Quaternion.identity);
        GameObject e5 = Instantiate(enemy4Prefab, topSpawns[3].transform.position, Quaternion.identity);
        GameObject e6 = Instantiate(enemy4Prefab, topSpawns[5].transform.position, Quaternion.identity);
        GameObject e7 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);

        e4.GetComponent<EnemyController>().pattern = 13;
        e5.GetComponent<EnemyController>().pattern = 12;
        e6.GetComponent<EnemyController>().pattern = 13;
        e7.GetComponent<EnemyController>().pattern = 13;

        yield return new WaitForSeconds(4);

        GameObject e8 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);
        GameObject e9 = Instantiate(enemy4Prefab, topSpawns[2].transform.position, Quaternion.identity);
        GameObject e10 = Instantiate(enemy4Prefab, topSpawns[5].transform.position, Quaternion.identity);
        GameObject e11 = Instantiate(enemy4Prefab, topSpawns[8].transform.position, Quaternion.identity);
        GameObject e12 = Instantiate(enemy4Prefab, topSpawns[7].transform.position, Quaternion.identity);

        e8.GetComponent<EnemyController>().pattern = 13;
        e9.GetComponent<EnemyController>().pattern = 12;
        e10.GetComponent<EnemyController>().pattern = 13;
        e11.GetComponent<EnemyController>().pattern = 13;
        e12.GetComponent<EnemyController>().pattern = 13;

        yield return new WaitForSeconds(1);

        waveRunning = false;
    }

    private IEnumerator Wave23()
    {
        waveRunning = true;

        for(int i = 0; i < 20; i++)
        {
            GameObject e1 = Instantiate(enemy2Prefab, bottomSpawns[1].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy2Prefab, bottomSpawns[8].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 14;
            e2.GetComponent<EnemyController>().pattern = 15;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    private IEnumerator Wave24()
    {
        waveRunning = true;
        StartCoroutine("Wave24Part2");

        for(int i = 0; i < 5; i++)
        {
            GameObject e1 = Instantiate(enemy5Prefab, rightSpawns[0].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy5Prefab, leftSpawns[0].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 16;
            e2.GetComponent<EnemyController>().pattern = 17;

            yield return new WaitForSeconds(4);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    private IEnumerator Wave24Part2()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 30; i++)
        {
            GameObject e1 = Instantiate(enemy1Prefab, rightSpawns[0].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 7;

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator Wave25()
    {
        waveRunning = true;

        for(int i = 0; i < 5; i++)
        {
            GameObject e1 = Instantiate(enemy4Prefab, topSpawns[1].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy4Prefab, topSpawns[2].transform.position, Quaternion.identity);
            GameObject e3 = Instantiate(enemy4Prefab, topSpawns[3].transform.position, Quaternion.identity);
            GameObject e4 = Instantiate(enemy4Prefab, topSpawns[4].transform.position, Quaternion.identity);
            GameObject e5 = Instantiate(enemy4Prefab, topSpawns[5].transform.position, Quaternion.identity);
            GameObject e6 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);
            GameObject e7 = Instantiate(enemy4Prefab, topSpawns[7].transform.position, Quaternion.identity);
            GameObject e8 = Instantiate(enemy4Prefab, topSpawns[8].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 12;
            e2.GetComponent<EnemyController>().pattern = 12;
            e3.GetComponent<EnemyController>().pattern = 12;
            e4.GetComponent<EnemyController>().pattern = 12;
            e5.GetComponent<EnemyController>().pattern = 13;
            e6.GetComponent<EnemyController>().pattern = 13;
            e7.GetComponent<EnemyController>().pattern = 13;
            e8.GetComponent<EnemyController>().pattern = 13;

            yield return new WaitForSeconds(4);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    private IEnumerator Wave27()
    {
        waveRunning = true;

        for(int i = 0; i < 15; i++)
        {
            GameObject e1 = Instantiate(enemy1Prefab, topSpawns[1].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy1Prefab, topSpawns[2].transform.position, Quaternion.identity);
            GameObject e3 = Instantiate(enemy1Prefab, topSpawns[10].transform.position, Quaternion.identity);
            GameObject e4 = Instantiate(enemy1Prefab, topSpawns[4].transform.position, Quaternion.identity);
            GameObject e5 = Instantiate(enemy1Prefab, topSpawns[5].transform.position, Quaternion.identity);
            GameObject e6 = Instantiate(enemy1Prefab, topSpawns[6].transform.position, Quaternion.identity);
            GameObject e7 = Instantiate(enemy1Prefab, topSpawns[7].transform.position, Quaternion.identity);
            GameObject e8 = Instantiate(enemy1Prefab, topSpawns[8].transform.position, Quaternion.identity);
            GameObject e9 = Instantiate(enemy1Prefab, topSpawns[9].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 18;
            e2.GetComponent<EnemyController>().pattern = 18;
            e3.GetComponent<EnemyController>().pattern = 18;
            e4.GetComponent<EnemyController>().pattern = 18;
            e5.GetComponent<EnemyController>().pattern = 18;
            e6.GetComponent<EnemyController>().pattern = 18;
            e7.GetComponent<EnemyController>().pattern = 18;
            e8.GetComponent<EnemyController>().pattern = 18;
            e9.GetComponent<EnemyController>().pattern = 18;

            yield return new WaitForSeconds(1);
        }

        for (int i = 0; i < 15; i++)
        {
            GameObject e1 = Instantiate(enemy1Prefab, topSpawns[0].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy1Prefab, topSpawns[1].transform.position, Quaternion.identity);
            GameObject e3 = Instantiate(enemy1Prefab, topSpawns[2].transform.position, Quaternion.identity);
            GameObject e4 = Instantiate(enemy1Prefab, topSpawns[3].transform.position, Quaternion.identity);
            GameObject e5 = Instantiate(enemy1Prefab, topSpawns[10].transform.position, Quaternion.identity);
            GameObject e6 = Instantiate(enemy1Prefab, topSpawns[5].transform.position, Quaternion.identity);
            GameObject e7 = Instantiate(enemy1Prefab, topSpawns[6].transform.position, Quaternion.identity);
            GameObject e8 = Instantiate(enemy1Prefab, topSpawns[7].transform.position, Quaternion.identity);
            GameObject e9 = Instantiate(enemy1Prefab, topSpawns[8].transform.position, Quaternion.identity);
            GameObject e10 = Instantiate(enemy1Prefab, topSpawns[9].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 19;
            e2.GetComponent<EnemyController>().pattern = 19;
            e3.GetComponent<EnemyController>().pattern = 19;
            e4.GetComponent<EnemyController>().pattern = 19;
            e5.GetComponent<EnemyController>().pattern = 19;
            e6.GetComponent<EnemyController>().pattern = 19;
            e7.GetComponent<EnemyController>().pattern = 19;
            e8.GetComponent<EnemyController>().pattern = 19;
            e9.GetComponent<EnemyController>().pattern = 19;
            e10.GetComponent<EnemyController>().pattern = 19;

            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    private IEnumerator Wave28()
    {
        waveRunning = true;
        for(int i = 0; i < 3; i++)
        {
            GameObject e1 = Instantiate(enemy6Prefab, topSpawns[2].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy6Prefab, topSpawns[3].transform.position, Quaternion.identity);
            GameObject e3 = Instantiate(enemy6Prefab, topSpawns[7].transform.position, Quaternion.identity);
            GameObject e4 = Instantiate(enemy6Prefab, topSpawns[8].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 20;
            e2.GetComponent<EnemyController>().pattern = 20;
            e3.GetComponent<EnemyController>().pattern = 20;
            e4.GetComponent<EnemyController>().pattern = 20;

            yield return new WaitForSeconds(1);
        }

        for (int i = 0; i < 3; i++)
        {
            GameObject e1 = Instantiate(enemy6Prefab, topSpawns[1].transform.position, Quaternion.identity);
            GameObject e2 = Instantiate(enemy6Prefab, topSpawns[10].transform.position, Quaternion.identity);
            GameObject e3 = Instantiate(enemy6Prefab, topSpawns[8].transform.position, Quaternion.identity);
            GameObject e4 = Instantiate(enemy6Prefab, topSpawns[9].transform.position, Quaternion.identity);

            e1.GetComponent<EnemyController>().pattern = 20;
            e2.GetComponent<EnemyController>().pattern = 20;
            e3.GetComponent<EnemyController>().pattern = 20;
            e4.GetComponent<EnemyController>().pattern = 20;

            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    private IEnumerator Wave29()
    {
        waveRunning = true;

        GameObject e1 = Instantiate(enemy4Prefab, topSpawns[5].transform.position, Quaternion.identity);
        GameObject e2 = Instantiate(enemy4Prefab, topSpawns[4].transform.position, Quaternion.identity);
        GameObject e3 = Instantiate(enemy4Prefab, topSpawns[2].transform.position, Quaternion.identity);

        e1.GetComponent<EnemyController>().pattern = 13;
        e2.GetComponent<EnemyController>().pattern = 12;
        e3.GetComponent<EnemyController>().pattern = 12;

        yield return new WaitForSeconds(4);

        GameObject e4 = Instantiate(enemy4Prefab, topSpawns[7].transform.position, Quaternion.identity);
        GameObject e5 = Instantiate(enemy4Prefab, topSpawns[3].transform.position, Quaternion.identity);
        GameObject e6 = Instantiate(enemy4Prefab, topSpawns[5].transform.position, Quaternion.identity);
        GameObject e7 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);

        e4.GetComponent<EnemyController>().pattern = 13;
        e5.GetComponent<EnemyController>().pattern = 12;
        e6.GetComponent<EnemyController>().pattern = 13;
        e7.GetComponent<EnemyController>().pattern = 13;

        yield return new WaitForSeconds(4);

        GameObject e8 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);
        GameObject e9 = Instantiate(enemy4Prefab, topSpawns[2].transform.position, Quaternion.identity);
        GameObject e10 = Instantiate(enemy4Prefab, topSpawns[5].transform.position, Quaternion.identity);
        GameObject e11 = Instantiate(enemy4Prefab, topSpawns[8].transform.position, Quaternion.identity);
        GameObject e12 = Instantiate(enemy4Prefab, topSpawns[7].transform.position, Quaternion.identity);

        e8.GetComponent<EnemyController>().pattern = 13;
        e9.GetComponent<EnemyController>().pattern = 12;
        e10.GetComponent<EnemyController>().pattern = 13;
        e11.GetComponent<EnemyController>().pattern = 13;
        e12.GetComponent<EnemyController>().pattern = 13;

        yield return new WaitForSeconds(4);

        GameObject e13 = Instantiate(enemy4Prefab, topSpawns[1].transform.position, Quaternion.identity);
        GameObject e14 = Instantiate(enemy4Prefab, topSpawns[3].transform.position, Quaternion.identity);
        GameObject e15 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);
        GameObject e16 = Instantiate(enemy4Prefab, topSpawns[7].transform.position, Quaternion.identity);

        e13.GetComponent<EnemyController>().pattern = 12;
        e14.GetComponent<EnemyController>().pattern = 12;
        e15.GetComponent<EnemyController>().pattern = 13;
        e16.GetComponent<EnemyController>().pattern = 13;

        yield return new WaitForSeconds(4);

        GameObject e17 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);
        GameObject e18 = Instantiate(enemy4Prefab, topSpawns[5].transform.position, Quaternion.identity);
        GameObject e19 = Instantiate(enemy4Prefab, topSpawns[7].transform.position, Quaternion.identity);
        GameObject e20 = Instantiate(enemy4Prefab, topSpawns[8].transform.position, Quaternion.identity);
        GameObject e21 = Instantiate(enemy4Prefab, topSpawns[3].transform.position, Quaternion.identity);

        e17.GetComponent<EnemyController>().pattern = 13;
        e18.GetComponent<EnemyController>().pattern = 13;
        e19.GetComponent<EnemyController>().pattern = 13;
        e20.GetComponent<EnemyController>().pattern = 13;
        e21.GetComponent<EnemyController>().pattern = 12;

        yield return new WaitForSeconds(4);

        GameObject e22 = Instantiate(enemy4Prefab, topSpawns[3].transform.position, Quaternion.identity);
        GameObject e23 = Instantiate(enemy4Prefab, topSpawns[8].transform.position, Quaternion.identity);
        GameObject e24 = Instantiate(enemy4Prefab, topSpawns[6].transform.position, Quaternion.identity);

        e22.GetComponent<EnemyController>().pattern = 12;
        e23.GetComponent<EnemyController>().pattern = 13;
        e24.GetComponent<EnemyController>().pattern = 13;

        yield return new WaitForSeconds(1);
        waveRunning = false;
    }

    // wave 30 is boss

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