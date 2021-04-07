using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFlash : MonoBehaviour
{
    private TextMeshProUGUI text;

    private bool flashing = false;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!flashing)
        {
            StartCoroutine("Flash");
        }
    }

    private IEnumerator Flash()
    {
        flashing = true;
        while (true)
        {
            text.enabled = false;
            yield return new WaitForSeconds(0.2f);
            text.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
