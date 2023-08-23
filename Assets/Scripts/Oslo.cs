using UnityEngine;

public class Oslo : Entity
{
    [SerializeField] float movementSpeed = 2f;
    private Vector2 movementDirection;
    private bool isUnderwater = true;
    public bool isFast = false;
    public bool canMove = true;

    public OxygenManager om;

    public static Oslo instance;
    public Interactable interactable;
    public bool CouldInteract = false;
    public bool interacting = false;
    public SpriteRenderer reactionSprite;
    [SerializeField]
    public Sprite[] reactionSprites;
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
        if(Input.GetButtonDown("Interact") && CouldInteract && interactable != null)
        {
            OsloStartInteraction();
        }
    }

    private void FixedUpdate()
    {
        if (movementDirection != null && rigidbody != null && canMove)
        {
            rigidbody.velocity = movementDirection * movementSpeed * Time.deltaTime;
        } 
    }

    public void React(int index)
    {
        
    }

    public void ClearReaction()
    {
        reactionSprite.sprite = null;
    }

    public void OsloStartInteraction()
    {
        om.TakeOxygen = false;
        canTakeDamage = false;
        canMove = false;
        interacting = true;
        CouldInteract = false;
        if (interactable != null) interactable.InteractObject(); 
    }

    public void FinishInteraction(bool reinteractable)
    {
        om.TakeOxygen = true;
        canTakeDamage = true;
        canMove = true;
        interactable = null;
        interacting = false;
        CouldInteract = reinteractable;
    }
     
}
