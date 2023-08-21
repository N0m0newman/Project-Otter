using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private BoxCollider2D collider;
    [SerializeField] private GameObject interactableTextObject;
    private FadeObject interactableFader;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        interactableFader = interactableTextObject.GetComponent<FadeObject>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collider != null)
        {
            if (collision.tag != "Oslo") return;
            if(interactableTextObject != null)
            {
                interactableFader.FadeInObject();
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
            }
        }
    }
}
