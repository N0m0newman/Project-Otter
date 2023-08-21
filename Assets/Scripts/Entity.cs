using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected string Name;
    protected bool isDead;
    [SerializeField] protected int Health;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected float Oxygen;
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

    public float ReduceOxygen(float oxygen)
    {
        Oxygen -= oxygen;
        return Oxygen;
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
