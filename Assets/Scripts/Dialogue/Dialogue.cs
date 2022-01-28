using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem 
{
    [System.Serializable]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] public string dialogue_instance_title;
        [SerializeField] public bool is_speaking_with_character = true;
        [SerializeField] public CharacterID character_speaking_with;
        [SerializeField] public DialogueLine[] dialogue_lines;
    }
}
