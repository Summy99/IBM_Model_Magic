using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptFlash : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("Flash");
    }

    private IEnumerator Flash()
    {
        while (true)
        {
            gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
