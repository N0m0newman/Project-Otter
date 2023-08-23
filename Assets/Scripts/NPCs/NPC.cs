using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public enum ConversationState 
{
    FIRSTGREETING,
    RETURNGREETING1,
    RETURNGREETING2,
    COMPLETEDPUZZLE,
    COMPLETEDCOLLECTIBLE
}

public class NPC : Interactable
{
    protected ConversationState conversationState;
    [SerializeField]
    protected BoxCollider2D boxCollider;
    protected string DialogueFileStarter;
    public DialogueManager dm;
    public virtual void Start()
    {
        conversationState = ConversationState.FIRSTGREETING;
        dm = DialogueManager.instance;
    }

    public void UpgradeConversation()
    {
        switch (conversationState)
        {
            case ConversationState.FIRSTGREETING:
                conversationState = ConversationState.RETURNGREETING1;
                break;
            case ConversationState.RETURNGREETING1:
                conversationState = ConversationState.COMPLETEDPUZZLE;
                break;
            case ConversationState.RETURNGREETING2:
                //last conversation piece this doesnt lead anywhere, deadend looped conversation.
                break;
            case ConversationState.COMPLETEDPUZZLE:
                conversationState = ConversationState.RETURNGREETING2;
                break;
                //only triggered when they have found the collectable
            case ConversationState.COMPLETEDCOLLECTIBLE:
                conversationState = ConversationState.RETURNGREETING2;
                break;
        }
    }

}
