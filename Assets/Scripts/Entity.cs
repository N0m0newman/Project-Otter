using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected string Name;
    protected bool isDead;
    [SerializeField] protected int Health;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected bool canTakeDamage;

    protected new Rigidbody2D rigidbody;

    [SerializeField]
    protected Characters character;

    void Start()
    {
        MaxHealth = 2;
        Health = 2;
        isDead = false;
        canTakeDamage = false;
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
        rigidbody.gravityScale = .8f; 
    }

    public bool Damage(int damage)
    {
        if (!canTakeDamage) return false;
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
