using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation
{
    List<DialogueLine> dialogueComponenets;
    public Conversation()
    {
        dialogueComponenets = new List<DialogueLine>();
    }

    public void AddComponent(DialogueLine line)
    {
        dialogueComponenets.Add(line);
    }

    public List<DialogueLine> GetFullConversation() 
    {
        return dialogueComponenets;
    }

    public DialogueLine GetDialogueLine(int index)
    {
        return dialogueComponenets[index];
    }
}
