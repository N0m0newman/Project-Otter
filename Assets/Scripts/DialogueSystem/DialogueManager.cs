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
    GameObject CharacterName;
    [SerializeField]
    GameObject DialogueBody;
    TextMeshProUGUI DBody, CharName;

    private void Start()
    {
        if(Directory.Exists(pathtofiles))
        {
            DBody = DialogueBody.GetComponent<TextMeshProUGUI>();
            CharName = CharacterName.GetComponent<TextMeshProUGUI>();
            conversations = new List<Conversation>();
            
            Conversation convo = conversations[0];
            DialogueLine dialogue = convo.GetDialogueLine(0);
            CharName.text = dialogue.name;
            DBody.text = dialogue.line;
        }
    }

    public void generateConversationsFromFile()
    {
        files = Directory.GetFiles(pathtofiles, @"*.txt", SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            StreamReader reader1 = new StreamReader(file);
            string[] lines = reader1.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Conversation conversation = new Conversation();
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
                DialogueLine dline = new DialogueLine(name, body, null, 2f);
                conversation.AddComponent(dline);
            }
            conversations.Add(conversation);
        }
    }



}
