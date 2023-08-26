using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (TakeOxygen)
        {
            if (timeRemaing > 0)
            {
                timeRemaing -= Time.deltaTime;
            } else
            {
                if(RegenerateOxygen)
                {
                    IncreaseOxygen(3);
                }
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

    public void StartOxygenRegen()
    {
        RegenerateOxygen = true;
        Oslo.instance.isUnderwater = false;
        Oslo.instance.animator.SetBool("isRegeneratingOxygen", true);
    }

    public void EndOxygenRegen()
    {
        RegenerateOxygen = false;
        Oslo.instance.isUnderwater = true;
        Oslo.instance.animator.SetBool("isRegeneratingOxygen", false);
    }

    public float IncreaseOxygen(float oxygen)
    {
        Oxygen += oxygen;
        if(Oxygen > MaxOxygen) Oxygen = MaxOxygen;
        slider.value = Oxygen;
        return Oxygen;
    }

    public float ReduceOxygen(float oxygen)
    {
        Oxygen -= oxygen;
        slider.value = Oxygen;
        return Oxygen;
    }
}
