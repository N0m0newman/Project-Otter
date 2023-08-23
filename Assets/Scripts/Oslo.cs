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
    public Interactable interactable;
    public bool CouldInteract = false;
    public bool interacting = false;
    void Start()
    {
        name = "Oslo";
        Health = 2;
        MaxHealth = 2;
        rigidbody = GetComponent<Rigidbody2D>();
        om = GetComponent<OxygenManager>();
        character = Characters.OSLO;
        instance = this;
    }

    void Update()
    {
        if(isUnderwater)
        {
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        } 
        else
        {
            if(Input.GetAxis("Vertical") > 0)
            {
                movementDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
            } else
            {
                movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
        }
        if(Input.GetButtonDown("Interact") && CouldInteract)
        {
            interacting = true;
            CouldInteract = false;
            interactable.InteractObject();
        }
    }

    private void FixedUpdate()
    {
        if (movementDirection != null && rigidbody != null)
        {
            rigidbody.velocity = movementDirection * movementSpeed * Time.deltaTime;
        } 
    }

    public void FinishInteraction(bool reinteractable)
    {
        interactable = null;
        interacting = false;
        CouldInteract = reinteractable;
    }
     
}
