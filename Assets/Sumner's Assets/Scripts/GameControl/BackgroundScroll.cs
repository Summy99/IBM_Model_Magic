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
        
    }

    private void Update()
    {
        if(background1.transform.position.y <= -140)
        {
            background1.transform.position = new Vector2(background1.transform.position.x, 224f);
        }

        if (background2.transform.position.y <= -140)
        {
            background2.transform.position = new Vector2(background2.transform.position.x, 224f);
        }

        background1.transform.position = new Vector2(background1.transform.position.x, background1.transform.position.y - scrollSpeed * Time.timeScale);
        background2.transform.position = new Vector2(background2.transform.position.x, background2.transform.position.y - scrollSpeed * Time.timeScale);
    }

    void FixedUpdate()
    {

    }
}
