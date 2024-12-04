using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DIALOGUE
{

    //唤起对话UI
    public class DialogueUI : MonoBehaviour
    {
        public GameObject panel;
        public Text dialogueText;
        public float textSpeed = 0.1f;
        public static bool textFinished = true;

        private void OnEnable()
        {
            EventHandler.ShowDialogueEvent += ShowDialogue;
        }
        private void OnDisable()
        {
            EventHandler.ShowDialogueEvent -= ShowDialogue;
        }

        private void ShowDialogue(string dialogue)
        {
            if (dialogue != string.Empty)
            {
                panel.SetActive(true);
            }
            else
                panel.SetActive(false);
            if (textFinished)
                StartCoroutine(SetTextPush(dialogue));
        }
        IEnumerator SetTextPush(string dialogue)
        {
            textFinished = false;
            dialogueText.text = "";
            for(int i=0;i<dialogue.Length;i++)
            {
                dialogueText.text += dialogue[i];
                yield return new WaitForSeconds(textSpeed);
            }
            textFinished = true;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}