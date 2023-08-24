using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreddieFrog : NPC
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        DialogueFileStarter = "FrogDialogue";
    }

    public override void InteractObject()
    {
        base.InteractObject();
        dm.StartDialogue(DialogueFileStarter, conversationState, this);
    }
}
