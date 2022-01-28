using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [System.Serializable]
    public class DialogueLine
    {
        [SerializeField] public CharacterID character_speaking_line;
        [SerializeField] public string line_text;
        [SerializeField] public bool contains_a_lie;
        [SerializeField] public int next_dialogue_line_index;
        [SerializeField] public DialogueResponse[] available_responses;
    }
}
