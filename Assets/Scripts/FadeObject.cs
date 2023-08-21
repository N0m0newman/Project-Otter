using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    enum states
    {
        FADEOUT,
        FADEIN,
        IDLE
    }
    [SerializeField] states fadeState = states.IDLE;
    [SerializeField] private float fadeOutTime;

    private void Update()
    {
        switch (fadeState)
        {
            case states.FADEOUT:
                Color objectcolor = GetComponent<TextMeshPro>().color;
                float fade = objectcolor.a - (fadeOutTime * Time.deltaTime);
                objectcolor = new Color(objectcolor.r, objectcolor.g, objectcolor.b, fade);
                GetComponent<TextMeshPro>().color = objectcolor;
                if (objectcolor.a <= 0)
                {
                    objectcolor.a = 0;
                    fadeState = states.IDLE;
                }
                break;
            case states.FADEIN:
                Color oc = GetComponent<TextMeshPro>().color;
                float f = oc.a + (fadeOutTime * Time.deltaTime);
                objectcolor = new Color(oc.r, oc.g, oc.b, f);
                GetComponent<TextMeshPro>().color = objectcolor;
                if (objectcolor.a >= 1)
                {
                    objectcolor.a = 1;
                    fadeState = states.IDLE;
                }
                break;
            case states.IDLE:
                break;
        }
    }

    public void FadeOutObject()
    {
        fadeState = states.FADEOUT;
    }

    public void FadeInObject()
    {
        fadeState = states.FADEIN;
    }
}
