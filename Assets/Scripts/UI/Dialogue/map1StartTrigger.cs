using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
// using Input = UnityEngine.Windows.Input;

[RequireComponent(typeof(DialogueController))]
public class map1StartTrigger : MonoBehaviour
{
    DialogueController dialogueController;
    public int triggerNumber = 1;
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
