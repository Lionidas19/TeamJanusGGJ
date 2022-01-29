using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushInteractable : MonoBehaviour
{
    bool is_being_used = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player") return;
        if (!other.gameObject.TryGetComponent<PlayerController>(out var playerController)) return;
        playerController.Hide();
        is_being_used = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag != "Player") return;
        if (!other.gameObject.TryGetComponent<PlayerController>(out var playerController)) return;
        playerController.Unhide();
        is_being_used = false;
    }
}
