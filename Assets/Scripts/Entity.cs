using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected string Name;
    protected bool isDead;
    protected int Health;
    protected int MaxHealth;
    protected float Oxygen;
    protected float MaxOxygen;
    // Start is called before the first frame update
    void Start()
    {
        MaxOxygen = 1f;
        MaxHealth = 2;
        Oxygen = 1f;
        Health = 2;
        isDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ReduceOxygen()
    {
        return false;
    }
    public bool ReduceOxygen(float oxygen)
    {
        return false;
    }
    public bool Damage()
    {
        Health -= 1;
        if(Health < 0)
        {
            isDead = true;
            return false;
        }
        return true;
    }

    public bool Damage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            isDead = true;
            return false;
        }
        return true;
    }

    public bool IsDead() 
    { 
        return isDead; 
    }
}
