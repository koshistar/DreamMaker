using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
//using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueController))]
//场景的最后一段对话，结束后进入下一场景
public class LastTrigger : MonoBehaviour
{
    private DialogueController dialogueController;
    public GameObject director;
    public int triggerNumber = 1;
    public string voice = null; 
    private int cnt = 0;

    private int tn;
    // Start is called before the first frame update
    void Start()
    {
        dialogueController = GetComponent<DialogueController>();
        cnt = 0;
        tn = triggerNumber;
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
                director.SetActive(true);
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                // foreach (var nd in nextDialogue)
                // {
                //     nd.SetActive(true);
                // }
                //
                // foreach (var pd in previousDialogue)
                // {
                //     pd.SetActive(false);
                // }
            }
        }
    }
}
