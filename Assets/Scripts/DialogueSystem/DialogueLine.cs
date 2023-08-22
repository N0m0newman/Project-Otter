using UnityEngine;

public class DialogueLine
{
    public string name;
    public string line;
    public Sprite picture;
    public float revealTime;

    public DialogueLine(string name, string line, Sprite picture, float revealTime)
    {
        this.name = name;
        this.line = line;
        this.picture = picture;
        this.revealTime = revealTime;
    }
}
