using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public DialogueSystem.DialogueSceneManager manager;

    public Text text;

    public LayerMask layerMask;

    [HideInInspector]
    public bool nearNPCs;
    [HideInInspector]
    public string name;

    // Start is called before the first frame update
    void Awake()
    {
        nearNPCs = false;
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 10.0f, layerMask);
        int closestNPC = 0;
        if (colliders.Length == 0)
        {
            nearNPCs = false;
            text.enabled = false;
        }
        else if (colliders.Length >= 1)
        {
            Debug.Log(colliders.Length);
            nearNPCs = true;
            for (int i = 1; i < colliders.Length; i++)
            {
                if (Vector2.Distance(transform.position, colliders[i].transform.position) < Vector2.Distance(transform.position, colliders[closestNPC].transform.position))
                {
                    closestNPC = i;
                }
            }
        }
        if (nearNPCs == true)
        {
            name = colliders[closestNPC].gameObject.name;
            manager.name = name;
            text.enabled = true;
            text.text = "Press E to talk to " + name;
        }
        else
        {
            name = "";
            manager.name = name;
            text.enabled = false;
        }
    }
}
