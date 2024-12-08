using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public static event Action OnDialogueComplete = delegate { };
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    List<string> dialogue = new List<string>();
    bool isDialogueActive = false;
    int dialogueIndex = 0;

    void Start()
    {
        dialoguePanel.SetActive(false);
        NPCController.OnNPCSelected += NPCController_OnNPCSelected;
    }

    private void OnDestroy()
    {
        NPCController.OnNPCSelected -= NPCController_OnNPCSelected;
    }

    private void NPCController_OnNPCSelected(string[] newDialogue)
    {
        dialogue.Clear();
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        dialogue.AddRange(newDialogue);
        isDialogueActive = true;    
        dialogueText.text = dialogue[dialogueIndex];
    }

    void Update()
    {
        if (isDialogueActive &&
            (Input.GetMouseButtonDown(0) ||
            Input.GetKeyDown(KeyCode.E)))
        {        
            if(dialogueIndex < dialogue.Count)
            {
                dialogueText.text = dialogue[dialogueIndex];
            }
            else
            {
                isDialogueActive = false;
                dialoguePanel.SetActive(false);
                OnDialogueComplete?.Invoke();
            }
            dialogueIndex++;
        }
    }
}