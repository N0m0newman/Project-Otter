using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    [SerializeField]
    private float timeRemaing = 2f;
    [SerializeField]
    private float OxygenLength = 2f;


    public bool TakeOxygen = false;
    // Update is called once per frame
    void Update()
    {
        if (TakeOxygen)
        {
            if (timeRemaing > 0)
            {
                timeRemaing -= Time.deltaTime;
            } else
            {
                if(Oslo.instance.isFast)
                {
                    timeRemaing = OxygenLength;
                    Oslo.instance.ReduceOxygen(0.02f);
                } else
                {
                    timeRemaing = OxygenLength;
                    Debug.Log(Oslo.instance.ReduceOxygen(0.01f));
                }
                
            }
        }
    }
}
