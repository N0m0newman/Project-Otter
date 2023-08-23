using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private BoxCollider2D collider;
    [SerializeField] private GameObject interactableTextObject;
    private FadeObject interactableFader;
    [SerializeField] private bool canInteract = false;
    private Oslo oslo;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        interactableFader = interactableTextObject.GetComponent<FadeObject>();
        oslo = Oslo.instance;
    }

    private void Update()
    {
        if(canInteract && !oslo.interacting)
        {
            InteractObject();
        }
    }

    public virtual void InteractObject() { }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collider != null)
        {
            if (collision.tag != "Oslo") return;
            if(interactableTextObject != null)
            {
                interactableFader.FadeInObject();
                canInteract = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collider != null)
        {
            if (collision.tag != "Oslo") return;
            if(interactableTextObject != null)
            {
                interactableFader.FadeOutObject();
                canInteract = false;
            }
        }
    }
}
