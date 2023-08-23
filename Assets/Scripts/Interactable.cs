using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : Entity
{
    [SerializeField]
    private new BoxCollider2D collider;
    [SerializeField] 
    private GameObject interactableTextObject;
    private FadeObject interactableFader;
    [SerializeField]
    private bool interactableObject = false;
    private Oslo oslo;

    private void Start()
    {
        if(interactableObject)
        {
            Debug.Log("InteractableGenerationCreated");
            collider = GetComponent<BoxCollider2D>();
            interactableFader = interactableTextObject.GetComponent<FadeObject>();
            oslo = Oslo.instance;
        }
    }

    public virtual void InteractObject() {}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (interactableObject)
        {
            if (collider != null)
            {
                if (collision.tag != "Oslo") return;
                if (interactableTextObject != null)
                {
                    interactableFader.FadeInObject();
                    Oslo.instance.CouldInteract = true;
                    Oslo.instance.interactable = this;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactableObject)
        {
            if (collider != null)
            {
                if (collision.tag != "Oslo") return;
                if (interactableTextObject != null)
                {
                    interactableFader.FadeOutObject();
                    Oslo.instance.CouldInteract = false;
                    Oslo.instance.interactable = null;
                }
            }
        }
    }
}
