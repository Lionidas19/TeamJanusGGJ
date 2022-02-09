using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace DialogueSystem 
{
    public class DialogueSceneManager : MonoBehaviour
    {
        public ControlScheme controls;
        public static DialogueSceneManager instance;

        public Animator transition;

        public string name;

        [SerializeField] private Dialogue[] scene_dialogues;
        [SerializeField] public CharacterIDToSpeechBubbleMappingStruct[] set_character_to_id_mapping_struct_values;

        private Dictionary<CharacterID, SpeechBubbleController> scene_character_to_speech_bubble_controller_mapping;

        private Dialogue currently_running_dialogue = null;
        private int current_running_dialogue_current_line_index = -1;
        private int current_running_dialogue_next_line_index = -1;
        private bool currently_has_response_choice_open = false;


        private bool to_load_a_level_after_handling_dialogue = false;
        private string level_to_load_after_dialogue;


        private bool temp_has_started_dialogue_reading = false;

        private bool should_block_first_interact_press = false;
        private bool has_blocked_first_interact_press = false;

        void Awake()
        {
            if(instance != null) 
            {
                Destroy(this);
            }
            else 
            {
                instance = this;
            }

            scene_character_to_speech_bubble_controller_mapping = new Dictionary<CharacterID, SpeechBubbleController>();

            foreach(CharacterIDToSpeechBubbleMappingStruct map_entry in set_character_to_id_mapping_struct_values)
            {
                scene_character_to_speech_bubble_controller_mapping.Add(map_entry.character_id, map_entry.speech_bubble_controller);
            }

            controls = new ControlScheme();
            controls.Player.Interact.performed += ctx => ProgressDialogueWithButton();
        }

        public void Start()
        {
            //ProgressDialogueWithButton();
        }

        public void ProgressDialogueWithButton()
        {
            // TEMP
            if(!temp_has_started_dialogue_reading)
            {
                if(name == "")
                {
                    return;
                }
                else
                {
                    /*TriggerDialogueByDialogueInstanceName("Test_Dialogue", true);*/
                    TriggerDialogueByDialogueInstanceName(name, true);
                    temp_has_started_dialogue_reading = true;
                }
            }
            else
            {

                HandleDialogueProgression();
            }
        }
        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        public Dialogue GetDialogueByDialogueInstanceName(string instance_name)
        {
            Dialogue result = null;

            foreach(Dialogue dialogue in scene_dialogues)
            {
                if(dialogue.dialogue_instance_title == instance_name)
                {
                    result = dialogue;
                    break;
                }
            }

            return result;
        }

        public bool TriggerDialogueByDialogueInstanceName(string instance_name, bool should_block_immediate_interact_for_first_dialogue_line)
        {
            bool success_result = false;

            if(should_block_immediate_interact_for_first_dialogue_line)
            {
                should_block_first_interact_press = true;
            }

            if(currently_running_dialogue == null)
            {
                Dialogue dialogue_to_trigger = GetDialogueByDialogueInstanceName(instance_name);

                if(dialogue_to_trigger != null)
                {
                    SpeechBubbleController raz_speech_bubble_controller = scene_character_to_speech_bubble_controller_mapping[CharacterID.Raz];
                    if(raz_speech_bubble_controller != null)
                    {
                        if(dialogue_to_trigger.is_speaking_with_character)
                        {
                            SpeechBubbleController other_character_speech_bubble_controller = scene_character_to_speech_bubble_controller_mapping[dialogue_to_trigger.character_speaking_with];
                            if(other_character_speech_bubble_controller != null)
                            {
                                StartDialogue(dialogue_to_trigger);
                                success_result = true;
                            }
                        }
                        else
                        {
                            StartDialogue(dialogue_to_trigger);
                            success_result = true;
                        }
                    }
                }
            }

            return success_result;
        }

        public void StartDialogue(Dialogue dialogue_to_start)
        {
            currently_running_dialogue = dialogue_to_start;
            HandleDialogueLineByIndex(0);
        }

        public void ClearDialogue()
        {
            currently_running_dialogue = null;
            current_running_dialogue_current_line_index = -1;
            ClearAllSpeechBubbles();
            should_block_first_interact_press = false;
            has_blocked_first_interact_press = false;
            temp_has_started_dialogue_reading = false;
        }

        public void HandleDialogueLineByIndex(int index)
        {
            current_running_dialogue_current_line_index = index;

            DialogueLine current_dialogue_line = currently_running_dialogue.dialogue_lines[current_running_dialogue_current_line_index];
            CharacterID line_speaker = current_dialogue_line.character_speaking_line;

            current_running_dialogue_next_line_index = current_dialogue_line.next_dialogue_line_index;

            ClearAllSpeechBubblesExceptFromSpecificCharacter(line_speaker);
            scene_character_to_speech_bubble_controller_mapping[line_speaker].DisplayDialogueLine(current_dialogue_line);
        }

        public void HandleDialogueProgression()
        {
            DialogueLine current_dialogue_line = currently_running_dialogue.dialogue_lines[current_running_dialogue_current_line_index];
            
            if(current_running_dialogue_next_line_index >= currently_running_dialogue.dialogue_lines.Length || current_running_dialogue_next_line_index < 0)
            {
                if(current_dialogue_line.load_level_after_line)
                {
                    to_load_a_level_after_handling_dialogue = true;
                    level_to_load_after_dialogue = current_dialogue_line.level_to_load;
                }
                ClearDialogue();
                if(!to_load_a_level_after_handling_dialogue) return;
                StartCoroutine("MoveOn");
                to_load_a_level_after_handling_dialogue = false;
                //level_to_load_after_dialogue = "";
            }
            else
            {
                // Check if we need to handle response stuff first before going straight to next dialogue line
                if(current_dialogue_line.available_responses.Length >= 1 && !currently_has_response_choice_open)
                {
                    HandleOpenDialogueResponses();
                }
                else if(current_dialogue_line.available_responses.Length >= 1 && currently_has_response_choice_open)
                {
                    HandleSelectingDialogueResponse();
                }
                else
                {
                    HandleDialogueLineByIndex(current_running_dialogue_next_line_index);
                }
            }
        }

        public void HandleOpenDialogueResponses()
        {
            currently_has_response_choice_open = true;

            DialogueLine current_dialogue_line = currently_running_dialogue.dialogue_lines[current_running_dialogue_current_line_index];
            CharacterID line_speaker = current_dialogue_line.character_speaking_line;

            ClearAllSpeechBubblesExceptFromSpecificCharacter(line_speaker);
            scene_character_to_speech_bubble_controller_mapping[line_speaker].DisplayDialogueResponses(current_dialogue_line.available_responses);
        }

        public void HandleSelectingDialogueResponse()
        {
            currently_has_response_choice_open = false;
            
            DialogueLine current_dialogue_line = currently_running_dialogue.dialogue_lines[current_running_dialogue_current_line_index];
            CharacterID line_speaker = current_dialogue_line.character_speaking_line;

            int selected_response_index = scene_character_to_speech_bubble_controller_mapping[line_speaker].current_dialogue_response_index;
            DialogueResponse selected_response = current_dialogue_line.available_responses[selected_response_index];

            HandleDialogueLineByIndex(selected_response.next_dialogue_line_index);
        }

        public void ClearAllSpeechBubbles()
        {
            foreach(CharacterID id in System.Enum.GetValues(typeof(CharacterID)))
            {
                if(scene_character_to_speech_bubble_controller_mapping[id].is_displaying)
                {
                    scene_character_to_speech_bubble_controller_mapping[id].ClearSpeechBubble();
                }
            }
        }

        public void ClearAllSpeechBubblesExceptFromSpecificCharacter(CharacterID character_to_exclude_from_clearing)
        {
            foreach(CharacterID id in System.Enum.GetValues(typeof(CharacterID)))
            {
                if(id != character_to_exclude_from_clearing)
                {
                    if(scene_character_to_speech_bubble_controller_mapping[id] == null) continue;
                    if(scene_character_to_speech_bubble_controller_mapping[id].is_displaying)
                    {
                        scene_character_to_speech_bubble_controller_mapping[id].ClearSpeechBubble();
                    }
                }
            }
        }
        IEnumerator MoveOn()
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1);

            SceneManager.LoadScene(level_to_load_after_dialogue);
        }
    }

    

    [System.Serializable]
    public struct CharacterIDToSpeechBubbleMappingStruct
    {
        [SerializeField] public CharacterID character_id;
        [SerializeField] public SpeechBubbleController speech_bubble_controller;
    }
}
