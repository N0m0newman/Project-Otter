using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    string pathtofiles = "Assets/DialogueFiles";
    [SerializeField]
    string characterPattern = @"\[.*?\]";
    [SerializeField]
    string[] files;
    [SerializeField]
    List<Conversation> conversations;
    [SerializeField]
    GameObject DialogueBox;
    [SerializeField]
    GameObject DialogueBody, CharacterName;
    TextMeshProUGUI DBody, CharName;
    public bool DialogueActive = false;
    Oslo oslo;
    private int index;
    Conversation activeConversation;
    public static DialogueManager instance;
    private NPC activeNPC;
    private void Start()
    {
        oslo = Oslo.instance;
        if(Directory.Exists(pathtofiles))
        {
            DBody = DialogueBody.GetComponent<TextMeshProUGUI>();
            CharName = CharacterName.GetComponent<TextMeshProUGUI>();
            conversations = new List<Conversation>();
            generateConversationsFromFile();
        }
        instance = this;
    }

    private void Update()
    {
        if(DialogueActive)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(DBody.text == activeConversation.GetDialogueLine(index).line)
                {
                    NextLine();
                } else
                {
                    StopAllCoroutines();
                    DBody.text = activeConversation.GetDialogueLine(index).line;
                }
            }
        }
    }

    public void StartDialogue(string name, ConversationState state, NPC npc)
    {
        DialogueBox.SetActive(true);
        DialogueActive = true;
        index = 0;
        activeConversation = GetConversationFromNameAndState(name, state);
        if(activeConversation == null)
        {
            Debug.LogError("Missing Conversation Data");
            return;
        }
        StartCoroutine(TypeLineSlow(activeConversation.GetDialogueLine(0)));
        CharName.text = activeConversation.GetDialogueLine(0).name;
        activeNPC = npc;
    }

    public void EndDialogue()
    {
        index = 0;
        DialogueBox.SetActive(false);
        DialogueActive = false;
        oslo.FinishInteraction(true);
        activeNPC.FinishedConversation();
        activeNPC = null;
    }

    public void ForceStopDialogue()
    {
        StopAllCoroutines();
        EndDialogue();
    }

    public void generateConversationsFromFile()
    {
        files = Directory.GetFiles(pathtofiles, @"*.txt", SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            StreamReader reader1 = new StreamReader(file);
            string[] lines = reader1.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string n = file.Replace(pathtofiles + "\\", "");
            Conversation conversation = new Conversation(n);
            foreach (string line in lines)
            {
                RegexOptions options = RegexOptions.Multiline;
                Match match = Regex.Match(line, characterPattern, options);
                string name, body;
                name = match.Value;
                name = name.Replace("[", string.Empty);
                name = name.Replace("]", string.Empty);
                body = line;
                body = body.Replace(match.Value + " ", string.Empty);
                DialogueLine dline = new DialogueLine(name, body, null, .06f);
                conversation.AddComponent(dline);
            }
            conversations.Add(conversation);
        }
    }

    IEnumerator TypeLineSlow(DialogueLine Dialogue)
    {
        //Type each character out 1 by 1 for dialogue effectness.........
        //if (Dialogue.name == "OSLO") oslo.React();
        foreach (char c in Dialogue.line.ToCharArray())
        {
            DBody.text += c;
            yield return new WaitForSeconds(Dialogue.revealTime);
        }
    }

    IEnumerator TypeLineSlow(string Dialogue)
    {
        //Type each character out 1 by 1 for dialogue effectness.........
        foreach (char c in Dialogue.ToCharArray())
        {
            DBody.text += c;
            yield return new WaitForSeconds(.1f);
        }
    }

    void NextLine()
    {
        if(index < activeConversation.GetFullConversation().Count -1)
        {
            index++;
            DBody.text = String.Empty;
            StartCoroutine(TypeLineSlow(activeConversation.GetDialogueLine(index)));
            CharName.text = activeConversation.GetDialogueLine(index).name;
        } else
        {
            EndDialogue();
        }
    }

    private Conversation GetConversationFromName(string name)
    {
        foreach(Conversation conversation in conversations) 
        {
            if (conversation.ConversationName == name) return conversation;
        }
        return null;
    }

    private Conversation GetConversationFromNameAndState(string starter, ConversationState state)
    {
        string n = null;
        switch (state)
        {
            case ConversationState.FIRSTGREETING:
                n = starter + "FirstGreeting.txt";
                break;
            case ConversationState.RETURNGREETING1:
                n = starter + "ReturnGreeting1.txt";
                break;
            case ConversationState.RETURNGREETING2:
                n = starter + "ReturnGreeting2.txt";
                break;
            case ConversationState.COMPLETEDPUZZLE:
                n = starter + "CompletedPuzzle.txt";
                break;
            case ConversationState.COMPLETEDCOLLECTIBLE:
                n = starter + "CompletedCollectible.txt";
                break;
        }
        return GetConversationFromName(n);
    }

}
