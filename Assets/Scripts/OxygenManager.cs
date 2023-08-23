using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    [SerializeField]
    private float timeRemaing = 2f;
    [SerializeField]
    private float OxygenLength = 2f;

    [SerializeField] 
    private float Oxygen;
    [SerializeField]
    private float MaxOxygen;

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
                    if(ReduceOxygen(0.02f) <= 0)
                    {
                        Oslo.instance.Damage();
                    } 
                    
                } else
                {
                    timeRemaing = OxygenLength;
                    if(ReduceOxygen(0.01f) <= 0)
                    {
                        Oslo.instance.Damage();
                    }
                }
                
            }
        }
    }

    public float ReduceOxygen(float oxygen)
    {
        Oxygen -= oxygen;
        return Oxygen;
    }
}
