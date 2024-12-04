using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

[RequireComponent(typeof(DialogueController))]
public class map2endtrigger : MonoBehaviour
{
    DialogueController dialogueController;
    public int triggerNumber = 1;
    private int cnt = 0;
    private int tn;

    public GameObject director;
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
        if (Input.anyKeyDown&&DialogueUI.textFinished)
        {
            if (cnt!=tn)
            {
                cnt++;
                dialogueController.ShowDialogue();
            }
            else
            {
                gameObject.SetActive(false);
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
