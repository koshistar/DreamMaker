using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;

namespace DIALOGUE
{

    //对话运行脚本
    public class DialogueController : MonoBehaviour
    {
        public DialogueData_SO dialogueEmpty;
        private bool isTalking;
        private ConcurrentStack<string> dialogueEmptyStack;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void Awake()
        {
            FillDialogueStack();
        }
        private void FillDialogueStack()
        {
            dialogueEmptyStack = new ConcurrentStack<string>();
            for (int i = dialogueEmpty.dialogueList.Count - 1; i > -1; i--)
            {
                dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
            }
        }
        // public void ClearDialogueStack()
        // {
        //     dialogueEmptyStack.Clear();
        //     dialogueEmpty = null;
        // }
        
        public void ShowDialogue()
        {
            if (!isTalking)
            {
                AudioManaager.Instance.Play("dialogue");
                StartCoroutine(DialogueRoutine(dialogueEmptyStack));
            }
        }            

        private IEnumerator DialogueRandomRoutine(ConcurrentStack<string> data)
        {
            isTalking = true;
            //Time.timeScale = 0;
            if (data.TryPop(out string result))
            {
                EventHandler.CallShowDialogueEvent(result);
                yield return null;
                isTalking = false;
                //Time.timeScale = 1f;
                EventHandler.CallGameStateChangeEvent(GameState.Pause);
            }
            else
            {
                EventHandler.CallShowDialogueEvent(string.Empty);
                //ClearDialogueStack();
                FillDialogueStack();
                isTalking = false;
                //Time.timeScale = 1f;
                EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
            }
        }
        private IEnumerator DialogueRoutine(ConcurrentStack<string> data)
        {
            isTalking = true;
            //Time.timeScale = 0;
            if (data.TryPop(out string result))
            {
                EventHandler.CallShowDialogueEvent(result);
                yield return null;
                isTalking = false;
                //Time.timeScale = 1f;
                EventHandler.CallGameStateChangeEvent(GameState.Pause);
            }
            else
            {
                EventHandler.CallShowDialogueEvent(string.Empty);
                //ClearDialogueStack();
                FillDialogueStack();
                isTalking = false;
                //Time.timeScale = 1f;
                EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
            }
        }
    }
}