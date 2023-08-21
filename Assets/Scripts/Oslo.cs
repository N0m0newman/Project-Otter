using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Oslo : Entity
{
    [SerializeField] float movementSpeed = 2f;
    private Rigidbody2D rigidbody;
    private Vector2 movementDirection;
    void Start()
    {
        name = "Oslo";
        Health = 2;
        MaxHealth = 2;
        Oxygen = 1f;
        MaxOxygen = 1f;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        if (movementDirection != null)
        {
            rigidbody.velocity = movementDirection * movementSpeed *Time.deltaTime;
        }
    }
}
