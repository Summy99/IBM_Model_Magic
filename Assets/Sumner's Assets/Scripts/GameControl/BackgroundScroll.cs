using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField]
    private GameObject background1, background2;

    public float scrollSpeed = 10f;

    void Start()
    {
        StartCoroutine(Scroll());
    }

    private void Update()
    {
        if (gameObject.GetComponent<GameController>().dead)
        {
            this.enabled = false;
        }

        if (background1.transform.position.y <= -140)
        {
            background1.transform.position = new Vector2(background1.transform.position.x, background2.transform.position.y + 182.5908f);
        }

        if (background2.transform.position.y <= -140)
        {
            background2.transform.position = new Vector2(background2.transform.position.x, background1.transform.position.y + 182.5908f);
        }
    }

    private IEnumerator Scroll()
    {
        while (true)
        {
            background1.transform.position = new Vector2(background1.transform.position.x, background1.transform.position.y - scrollSpeed * Time.timeScale);
            background2.transform.position = new Vector2(background2.transform.position.x, background2.transform.position.y - scrollSpeed * Time.timeScale);

            yield return new WaitForSeconds(0.01f);
        }
    }
}
