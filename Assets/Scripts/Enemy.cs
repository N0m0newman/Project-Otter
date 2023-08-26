using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : Entity
{
    public Transform target;
    public float nextWaypointDistance = 3f;

    public Transform graphics;

    public Path path;
    int currentWaypoint = 0;
    bool reachedPoint = false;
    bool canMove = true;

    Seeker seeker;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        Debug.Log(distance);
        if (distance < attackRange && attackReady && canAttack) {
            attackReady = false;
            StartCoroutine(Attack());           
        }
        if (seeker.IsDone() && canMove)
        {
            seeker.StartPath(rigidbody.position, target.position, OnPathComplete);
        }
    }

    public override IEnumerator Attack()
    {
        Debug.Log("Starting attack");
        rigidbody.velocity = Vector3.zero;
        canMove = false;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange, damageableMask);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Entity>().ApplyDamage(attackDamage);
        }
        yield return new WaitForSeconds(attackCooldown);
        attackReady = true;
        canMove = true;
        Debug.Log("Finished attack");
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        if (path == null) return;
        if (!canMove) return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedPoint = true;
            return;
        }
        else
        {
            reachedPoint = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody.position).normalized;
        Vector2 force = direction * movementSpeed * Time.deltaTime;

        rigidbody.AddForce(force);

        float distance = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        //work out sprite flip rate
        if(force.x >= 0.01f)
        {
            graphics.localScale = new Vector3(-1f,1f, 1f);
        } else if(force.x <= -0.01f)
        {
            graphics.localScale = new Vector3(1f,1f, 1f);
        }

    }

}
