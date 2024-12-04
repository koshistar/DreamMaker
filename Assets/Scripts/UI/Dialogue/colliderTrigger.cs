using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
using UnityEngine.UI;

[RequireComponent(typeof(DialogueController))]
public class colliderTrigger : MonoBehaviour
{
    DialogueController dialogueController;
    public GameObject tipsPanel;
    public string tips = null;
    public Text texts;
    public int triggerNum = 2;
    public GameObject nextObject;
    public GameObject previousObject;
    
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
        if (Input.GetMouseButtonDown(0)&&DialogueUI.textFinished&&isEntered)
        {
            if (cnt!=tn)
            {
                cnt++;
                dialogueController.ShowDialogue();
            }
            else
            {
                if(texts!=null)
                    texts.gameObject.SetActive(false);
                if (tipsPanel != null)
                {
                    tipsPanel.gameObject.SetActive(false);
                }

                if (nextObject != null)
                {
                    nextObject.SetActive(true);
                    if (previousObject != null)
                    {
                        previousObject.gameObject.SetActive(false);
                    }           
                    gameObject.SetActive(false);
                }
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
        if (other.CompareTag("Player"))
        {
            isEntered = true;
            if (tipsPanel != null)
            {
                tipsPanel.SetActive(true);
            }
            if (tips != null)
            {
                AudioManaager.Instance.Play("tip");
                texts.gameObject.SetActive(true);
                texts.text = tips;
            }
        }
    }
}
