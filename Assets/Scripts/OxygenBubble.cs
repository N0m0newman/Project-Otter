using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBubble : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Oslo")
        {
            Oslo.instance.om.StartOxygenRegen();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Oslo")
        {
            Oslo.instance.om.EndOxygenRegen();
        }
    }
}
