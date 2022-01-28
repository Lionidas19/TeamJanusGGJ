using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
    public class SpeechBubbleController : MonoBehaviour
    {
        [System.NonSerialized] public bool is_displaying = false;
        [System.NonSerialized] public int current_dialogue_response_index = -1;
        [System.NonSerialized] public DialogueResponse[] temporary_current_dialogue_responses;

        public TextMeshProUGUI text_container;
        public GameObject speech_bubble_container;

        void Update()
        {
            /*if(current_dialogue_response_index != -1)
            {
                if(Input.GetKeyDown(KeyCode.A))
                {
                    SwitchToDialogueResponseWithIndex(current_dialogue_response_index - 1);
                }
                else if(Input.GetKeyDown(KeyCode.D))
                {
                    SwitchToDialogueResponseWithIndex(current_dialogue_response_index + 1);
                }
            }*/
        }

        public void DisplayDialogueLine(DialogueLine line)
        {
            if(!is_displaying)
            {
                is_displaying = true;
                speech_bubble_container.gameObject.SetActive(true);
            }

            text_container.text = "";

            if(line.contains_a_lie)
            {
                text_container.text = "<color=#DC143C>" + line.line_text + "</color>";
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(TypeLine(line.line_text));
            }
        }

        IEnumerator TypeLine(string dialogue_line)
        {
            foreach (char letter in dialogue_line.ToCharArray())
            {
                text_container.text += letter;
                yield return null;
            }
        }

        public void DisplayDialogueResponses(DialogueResponse[] responses)
        {
            // TODO: Display selection arrows

            if(!is_displaying)
            {
                is_displaying = true;
                speech_bubble_container.gameObject.SetActive(true);
            }

            temporary_current_dialogue_responses = responses;
            SwitchToDialogueResponseWithIndex(0);
        }

        public void SwitchToDialogueResponseWithIndex(int index)
        {
            // TODO: Update selection arrows to show in what direction stuff can be selected from.

            if((index < temporary_current_dialogue_responses.Length) && (index >= 0))
            {
                current_dialogue_response_index = index;
                text_container.text = temporary_current_dialogue_responses[current_dialogue_response_index].response_text;
            }
        }

        public void ClearSpeechBubble()
        {
            is_displaying = false;
            if(current_dialogue_response_index != -1)
            {
                // TODO: Disable displaying selection arrows.
                current_dialogue_response_index = -1;
            }

            speech_bubble_container.gameObject.SetActive(false);
        }
    }
}
