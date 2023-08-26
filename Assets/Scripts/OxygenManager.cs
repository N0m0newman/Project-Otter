using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private float SlowTake = .1f;
    [SerializeField]
    private float FastTake = .2f;

    public bool TakeOxygen = false;
    public bool RegenerateOxygen = false;

    public GameObject oxygenBar;
    private Slider slider;
    private void Start()
    {
        slider = oxygenBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TakeOxygen && !RegenerateOxygen)
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
        if(RegenerateOxygen)
        {

        }
    }

    void StartRegenOxygen()
    {

    }

    public float ReduceOxygen(float oxygen)
    {
        Oxygen -= oxygen;
        slider.value = Oxygen;
        return Oxygen;
    }
}
