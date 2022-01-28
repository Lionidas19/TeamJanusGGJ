using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [System.Serializable]
    public class DialogueResponse 
    {
        [SerializeField] public string response_text;
        [SerializeField] public int next_dialogue_line_index;
    }
}
