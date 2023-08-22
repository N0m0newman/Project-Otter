using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Oslo : Entity
{
    [SerializeField] float movementSpeed = 2f;
    private Vector2 movementDirection;
    private bool isUnderwater = true;
    public bool isFast = false;

    OxygenManager om;

    public static Oslo instance;
    public bool interacting = false;
    void Start()
    {
        name = "Oslo";
        Health = 2;
        MaxHealth = 2;
        Oxygen = 1f;
        MaxOxygen = 1f;
        rigidbody = GetComponent<Rigidbody2D>();
        om = GetComponent<OxygenManager>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isUnderwater)
        {
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        } else
        {
            if(Input.GetAxis("Vertical") > 0)
            {
                movementDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
            } else
            {
                movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
        }
    }

    private void FixedUpdate()
    {
        if (movementDirection != null && rigidbody != null && !interacting)
        {
            rigidbody.velocity = movementDirection * movementSpeed * Time.deltaTime;
        } 
    }

     
}
