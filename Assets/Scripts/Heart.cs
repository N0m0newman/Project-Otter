using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField]
    Sprite empty, half, full;
    public enum HeartStates{
        EMPTY,
        HALF,
        FULL,
    }
    public HeartStates state;
    [SerializeField]
    Image image;

    public void Awake()
    {
        state = HeartStates.FULL;
        image.sprite = full;
    }

    public void UpdateHealth(HeartStates states)
    {
        state = states;
        switch (state)
        {
            case HeartStates.EMPTY:
                image.sprite = empty;
                break;
            case HeartStates.HALF:
                image.sprite = half;
                break;
            case HeartStates.FULL:
                image.sprite = full;
                break;
        }
    }

    public HeartStates GetHealth()
    {
        return state;
    }

}
