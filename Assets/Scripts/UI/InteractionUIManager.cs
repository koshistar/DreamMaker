﻿using UnityEngine;

public class InteractionUIManager : MonoBehaviour
{
    public GameObject interactionPanel1;  // ���1

    public InteractionEventManager eventManager;  // �¼�����������

    private void Start()
    {
        // ���������¼�
        eventManager.OnInteractWithObject.AddListener(OnInteractWithObject);
        Debug.Log("Interaction UI Manager initialized.");
    }

    // �����¼��������UI����
    private void OnInteractWithObject()
    {
        Debug.Log("Interaction event received, showing the appropriate UI panel.");
        ShowPanel(1);  // �򿪵�һ�����
    }

    // ��ʾ��ͬ��UI���
    private void ShowPanel(int panelIndex)
    {
        Debug.Log("Showing UI panel " + panelIndex);

        // �����������
        interactionPanel1.SetActive(false);


        // ��ʾָ�������
        switch (panelIndex)
        {
            case 1:
                interactionPanel1.SetActive(true);
                Debug.Log("Panel 1 is now active.");
                break;
                Debug.LogWarning("Invalid panel index.");
                break;
        }
    }
}
