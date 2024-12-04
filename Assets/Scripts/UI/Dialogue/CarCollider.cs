using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

[RequireComponent(typeof(DialogueController))]
public class CarCollider : MonoBehaviour
{
    DialogueController dialogueController;
    public int triggerNum = 2;
    public string voice = null;
    
    private int cnt;
    private int tn;

    private bool isEntered = false;
    // Start is called before the first frame update
    void Start()
    {
        cnt = 0;
        tn = triggerNum;
        dialogueController = GetComponent<DialogueController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown&&DialogueUI.textFinished&&isEntered)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            AudioManaager.Instance.Play("car");
            isEntered = true;
        }
    }
}
