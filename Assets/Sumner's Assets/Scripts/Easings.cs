using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easings : MonoBehaviour
{
    public static float EaseInSine(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }

    public static float EaseOutSine(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }

    public static float EaseInOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }

    public static float EaseInQuad(float x)
    {
        return x * x;
    }

    public static float EaseOutQuad(float x)
    {
        return 1 - (1 - x) * (1 - x);
    }

    public static float EaseInOutQuad(float x)
    {
        if (x < 0.5f)
        {
            return 2 * x * x;
        }
        else
        {
            return 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
        }
    }

    public static float EaseInOutBack(float x)
    {
        const float c1 = 1;
        const float c2 = c1;

        if (x < 0.5f)
        {
            return (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2;
        }
        else
        {
            return (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }
    }
}
