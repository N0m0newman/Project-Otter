using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public override void InteractObject()
    {
        base.InteractObject();
        Debug.Log("Interacting with chest!");
    }
}
