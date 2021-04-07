using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private TextMeshProUGUI text;

    public string curColorChange = "greenUp";

    public float colorRed = 1f;
    public float colorGreen = 0f;
    public float colorBlue = 0f;

    public float roc = 0.1f;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        text.color = new Color(colorRed, colorGreen, colorBlue);


        if(colorRed >= 1f && colorGreen <= 0f && colorBlue <= 0f)
        {
            curColorChange = "greenUp";
        }

        if (colorRed >= 1f && colorGreen >= 1f && colorBlue <= 0f)
        {
            curColorChange = "redDown";
        }

        if (colorRed <= 0f && colorGreen >= 1f && colorBlue <= 0f)
        {
            curColorChange = "blueUp";
        }

        if (colorRed <= 0f && colorGreen >= 1f && colorBlue >= 1f)
        {
            curColorChange = "greenDown";
        }

        if (colorRed <= 0f && colorGreen <= 0f && colorBlue >= 1f)
        {
            curColorChange = "redUp";
        }

        if (colorRed >= 1f && colorGreen <= 0f && colorBlue >= 1f)
        {
            curColorChange = "blueDown";
        }



        if (curColorChange == "greenUp" && colorGreen < 1)
        {
            colorGreen += roc;
        }

        if (curColorChange == "redDown" && colorRed > 0)
        {
            colorRed -= roc;
        }

        if (curColorChange == "blueUp" && colorBlue < 1)
        {
            colorBlue += roc;
        }

        if (curColorChange == "greenDown" && colorGreen > 0)
        {
            colorGreen -= roc;
        }

        if (curColorChange == "redUp" && colorRed < 1)
        {
            colorRed += roc;
        }

        if (curColorChange == "blueDown" && colorBlue > 0)
        {
            colorBlue -= roc;
        }
    }
}
