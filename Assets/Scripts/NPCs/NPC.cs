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
    protected string DialogueFileStarter;
    public DialogueManager dm;

    public bool hasCompletedPuzzle = false;
    public bool hasCompletedCollectible = false;

    public virtual void Start()
    {
        conversationState = ConversationState.FIRSTGREETING;       
    }

    public void UpgradeConversation()
    {
        switch (conversationState)
        {
            case ConversationState.FIRSTGREETING:
                conversationState = ConversationState.RETURNGREETING1;
                break;
            case ConversationState.RETURNGREETING1:
                if(hasCompletedPuzzle) conversationState = ConversationState.COMPLETEDPUZZLE;
                break;
            case ConversationState.RETURNGREETING2:
                //last conversation piece this doesnt lead anywhere, deadend looped conversation.
                break;
                //trigered when they have completed the puzzle
            case ConversationState.COMPLETEDPUZZLE:
                if(hasCompletedPuzzle) conversationState = ConversationState.RETURNGREETING2;
                break;
                //only triggered when they have found the collectable
            case ConversationState.COMPLETEDCOLLECTIBLE:
                conversationState = ConversationState.RETURNGREETING2;
                break;
        }
    }
    public void UpgradeConversation(ConversationState conversationState)
    {
        this.conversationState = conversationState;
    }

    public ConversationState GetConversationState()
    {
        return conversationState;
    }

    public virtual void FinishedConversation()
    {
        UpgradeConversation();
    }

}
