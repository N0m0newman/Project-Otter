using System.Collections;
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
    [SerializeField]
    private AudioClip[] reactionSounds;
    private bool reacting = false;

    private GameObject backpack;

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
        if (Input.GetButtonDown("Interact") && CouldInteract && interactable != null)
        {
            OsloStartInteraction();
        }

        //Check if he should sprint
        isFast = Input.GetButton("Sprint");
        
        if(timeBetweenAttacks <= 0)
        {
            if (Input.GetButtonDown("Fire1") && canAttack)
            {   
                timeBetweenAttacks = attackCooldown;
                Attack();
            }else
            {
                timeBetweenAttacks -= Time.deltaTime;
            }
        }
    }

    public void ToggleBackpack()
    {
        
    }

    public override void Attack()
    {
        base.Attack();
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, damageableMask);
        for(int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Entity>().ApplyDamage(attackDamage);
        }
    }

    private void FixedUpdate()
    {
        if (movementDirection != null && rigidbody != null && canMove)
        {
            rigidbody.velocity = (isFast) ? movementDirection * (movementSpeed * 2) * Time.deltaTime : movementDirection * movementSpeed * Time.deltaTime; 
        } 
    }

    public void React(int index)
    {
        StartCoroutine(ReactionStart(index));
    }

    private IEnumerator ReactionStart(int index)
    {
        if (reacting) yield return new WaitForSeconds(.1f);
        reacting = true;
        reactionSprite.sprite = reactionSprites[index];
        new WaitForSeconds(1);
        reactionSprite.sprite = null;
        reacting = false;
        yield return new WaitForSeconds(.1f);
    }

    public void ForceClearReaction()
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
        canAttack = false;
        if (interactable != null) interactable.InteractObject(); 
    }

    public void FinishInteraction(bool reinteractable)
    {
        om.TakeOxygen = true;
        canTakeDamage = true;
        canMove = true;
        interactable = null;
        interacting = false;
        canAttack = true;
        CouldInteract = reinteractable;
    }
     
}
