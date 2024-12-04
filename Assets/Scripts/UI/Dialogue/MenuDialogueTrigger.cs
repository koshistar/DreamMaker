using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
// using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueController))]
//该对话结束近行切换
public class MenuDialogueTrigger : MonoBehaviour
{
    private DialogueController dialogueController;

    public int triggerNum = 1;
    private int cnt = 0;
    public List<GameObject> nextDialogue;
    public List<GameObject> previousDialogue;
    public string voice = null;
    
    private int tn;
    // Start is called before the first frame update
    void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }

    private void Start()
    {
        tn=triggerNum;
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))&&DialogueUI.textFinished)
        {
            if (cnt!=tn)
            {
                cnt++;
                dialogueController.ShowDialogue();
                if (voice != null)
                {
                    AudioManaager.Instance.Play(voice);
                }
            }
            else
            {
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                foreach (var nd in nextDialogue)
                {
                    nd.SetActive(true);
                }

                foreach (var pd in previousDialogue)
                {
                    pd.SetActive(false);
                }
            }
        }
    }
}
