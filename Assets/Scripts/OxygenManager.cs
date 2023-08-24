using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    [SerializeField]
    private float timeRemaing = .5f;
    [SerializeField]
    private float OxygenLength = .5f;

    [SerializeField] 
    private float Oxygen;
    [SerializeField]
    private float MaxOxygen;
    [SerializeField]
    private float SlowTake = .01f;
    [SerializeField]
    private float FastTake = .02f;

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
                    if(ReduceOxygen(FastTake) <= 0)
                    {
                        Oslo.instance.ApplyDamage(1);
                    } 
                    
                } else
                {
                    timeRemaing = OxygenLength;
                    if(ReduceOxygen(SlowTake) <= 0)
                    {
                        Oslo.instance.ApplyDamage(1);
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
