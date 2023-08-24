using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected string Name;
    protected bool isDead;
    [SerializeField] protected int Health;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected bool canTakeDamage;

    [SerializeField]
    protected int attackDamage;
    protected bool canAttack = false;
    [SerializeField]
    protected Transform attackPosition;
    protected float timeBetweenAttacks = 0f;
    protected float attackCooldown = 2f;
    protected float attackRange = 5f;
    [SerializeField]
    protected LayerMask damageableMask;

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

    public virtual void Attack() { }

    public virtual void Die()
    {
        rigidbody.gravityScale = .8f; 
    }

    public virtual bool ApplyDamage(int damage)
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
