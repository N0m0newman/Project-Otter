using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Conversation
{
    public string ConversationName;
    List<DialogueLine> dialogueComponenets;
    public Conversation(string name)
    {
        dialogueComponenets = new List<DialogueLine>();
        ConversationName = name; 
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
