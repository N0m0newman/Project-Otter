using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected string Name;
    protected bool isDead;
    [SerializeField] protected int Health;
    [SerializeField] protected int MaxHealth;
    protected new Rigidbody2D rigidbody;
    [SerializeField] protected float Oxygen;
    protected float MaxOxygen;
    // Start is called before the first frame update
    void Start()
    {
        MaxOxygen = 1f;
        MaxHealth = 2;
        Oxygen = 1f;
        Health = 2;
        isDead = false;
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
            Die();
            return false;
        }
        return true;
    }

    public virtual void Die()
    {
        Debug.Log("Dead0");
        rigidbody.gravityScale = .8f; 
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
