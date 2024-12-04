using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

[RequireComponent(typeof(DialogueController))]
public class TestForDialogue : MonoBehaviour
{
    private DialogueController dialogueController;
    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && DialogueUI.textFinished) 
        {
            dialogueController.ShowDialogue();
        }
    }
}
