using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private RectTransform rect;

    private bool shaking = false;

    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        
    }

    private IEnumerator Shake(float duration)
    {
        float initDur = duration;

        while (duration > 0)
        {
            duration -= Time.deltaTime;
            rect.anchoredPosition = new Vector2(Random.Range(-25, 25), Random.Range(-25, 25)) * (duration/initDur);
            yield return new WaitForSeconds(0.01f);
        }

        rect.anchoredPosition = new Vector2(0, 0);
    }

    private IEnumerator ShakeContinuous(float duration)
    {
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            rect.anchoredPosition = new Vector2(Random.Range(-25, 25), Random.Range(-25, 25));
            yield return new WaitForSeconds(0.01f);
        }

        rect.anchoredPosition = new Vector2(0, 0);
    }
}
