using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushInteractable : Interactable
{
    bool is_being_used = false;
    private void Start()
    {
        Debug.Log("Bleh");
    }

    public override void Interact()
    {
        // IS THIS THE BEST WAY??????????????
        var playerGameObject = GameObject.FindWithTag("Player");
        if(!is_being_used)
        {
            is_being_used = true;
            playerGameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, playerGameObject.transform.position.z);
        }
        else
        {
            is_being_used = false;
        }


    }
}
