using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : Entity
{
    [SerializeField]
    private Collider2D interactionZone;
    [SerializeField]
    private GameObject interactableTextObject;
    [SerializeField]
    private FadeObject interactableFader;
    [SerializeField]
    private bool interactableObject = false;
    protected Oslo oslo;

    private void Start()
    {
        if (interactableObject && interactionZone != null)
        {
            Debug.Log("InteractableGenerationCreated");
            oslo = Oslo.instance;
        }
    }

    public virtual void InteractObject() { }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (interactableObject && interactionZone != null)
        {
            if (collision.tag != "Oslo") return;
            if (interactableTextObject != null && interactableFader != null)
            {
                interactableFader.FadeInObject();
                Oslo.instance.CouldInteract = true;
                Oslo.instance.interactable = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactableObject && interactionZone != null)
        {
            if (collision.tag != "Oslo") return;
            if (interactableTextObject != null && interactableFader != null)
            {
                interactableFader.FadeOutObject();
                Oslo.instance.CouldInteract = false;
                Oslo.instance.interactable = null;
            }
        }
    }
}
