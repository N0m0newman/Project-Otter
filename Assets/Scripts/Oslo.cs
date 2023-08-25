using System;
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
    [SerializeField]
    private GameObject HealthBar;
    [SerializeField]
    private GameObject HeartPrefab;

    void Awake()
    {
        name = "Oslo";
        Health = 4;
        MaxHealth = 4;
        rigidbody = GetComponent<Rigidbody2D>();
        om = GetComponent<OxygenManager>();
        character = Characters.OSLO;
        attackReady = true;
        canAttack = true;
    }

    private void Start()
    {
        StartCoroutine(SyncHealthAmount());
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
        if (attackReady && Input.GetButtonDown("Fire1") && canAttack)
        {
            attackReady = false;
            Debug.Log("Attack started!");
            StartCoroutine(Attack());
        }
    }

    public void ToggleBackpack()
    {
        
    }

    public override IEnumerator Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, damageableMask);
        for(int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Entity>().ApplyDamage(attackDamage);
        }
        Debug.Log("Started cooldown");
        yield return new WaitForSeconds(attackCooldown);
        Debug.Log("Attack finished");
        attackReady = true;
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
        rigidbody.velocity = Vector3.zero;
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

    IEnumerator SyncHealthAmount()
    {
        //work out how many hearts we will need
        int hearts = MaxHealth / 2;
        //destroy all hearts from parent
        foreach(Transform child in HealthBar.transform)
        {
            Destroy(child.gameObject);
        }
        //build new hearts, apply to master object
        for(int i = 0; i < hearts; i++)
        {
            GameObject newHeart = Instantiate(HeartPrefab, HealthBar.transform);
        }
        //DONT FUYCKING REMOVE THIS LINE OR I WILL CRY AND SHIT MYSELF.
        yield return new WaitForEndOfFrame();
        //figure out how many hearts to destroy
        int healthToRemove = MaxHealth - Health;
        //if full health dont, cancel out
        if (healthToRemove == 0) yield return null;
        int halfHearts;
        int FullRemoveHearts = Math.DivRem(healthToRemove, 2, out halfHearts);
        //get last heart out of all
        int lastheart = HealthBar.transform.childCount - 1;
        //remove full hearts
        for (int i = 0; i < FullRemoveHearts; i++)
        {
            Heart heart = (lastheart == 0) ? HealthBar.transform.GetChild(0).GetComponent<Heart>() : HealthBar.transform.GetChild(lastheart - i).GetComponent<Heart>();
            heart.UpdateHealth(Heart.HeartStates.EMPTY);
            yield return new WaitForEndOfFrame();
        }
        //calculate half hearts
        for (int i = 0; i < halfHearts; i++)
        {
            //get last full heart
            int b = lastheart - FullRemoveHearts;
            Heart heart = (b == 0) ? HealthBar.transform.GetChild(0).GetComponent<Heart>() : HealthBar.transform.GetChild(b - i).GetComponent<Heart>();
            heart.UpdateHealth(Heart.HeartStates.HALF);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public override bool ApplyDamage(int damage)
    {
        bool b = base.ApplyDamage(damage);
        StartCoroutine(SyncHealthAmount());
        return b;
    }

}
