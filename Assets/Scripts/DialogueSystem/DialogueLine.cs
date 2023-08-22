using UnityEngine;

public class DialogueLine
{
    public string name;
    public string line;
    public Sprite picture;
    public float revealTime;
    public AudioSource audioSource;

    public DialogueLine(string name, string line, Sprite picture, float revealTime)
    {
        this.name = name;
        this.line = line;
        this.picture = picture;
        this.revealTime = revealTime;
        if(name.ToLower() == "oslo") ConfigureAudioSource();
    }

    void ConfigureAudioSource()
    {
        switch(line.ToLower())
        {
            case "(happy squeak)":
                break;
            case "(curious squeak)":
                break;
            case "(injured squeak)":
                break;
            case "(confused squeak)":
                break;
            case "(excited squeak)":
                break;
            case "(alarmed squeak)":
                break;
            case "(sad squeak)":
                break;
            case "(angry squeak)":
                break;
            case "(determined squeak)":
                break;
        }
    }

}
