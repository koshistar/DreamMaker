using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("�ռ�������")]
    public GameObject collectedEffect;
    public Color normalColor = Color.green; // ������ɫ
    public Color highlightColor = Color.yellow; // ������ɫ
    public float breathSpeed = 2f; // �����ٶ�

    [Header("��С����")]
    public float shrinkFactor = 0.1f; // ��С�����ٱ�
    public float shrinkDuration = 0.5f; // ��С�ĳ���ʱ��

    private Material objectMaterial; // �����޸Ĳ���
    private bool isCollected = false; // �ж���Ʒ�Ƿ��ռ�
    private Renderer objectRenderer; // ��Ⱦ�����

    [Header("前后不同对话")]
    public GameObject mouse1;
    public GameObject mouse2;
    public static int collectNum = 0;
    private void Start()
    {
        // ��ȡ�����Renderer���
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            objectMaterial = objectRenderer.material; // ��ȡ����
        }
    }

    private void Update()
    {
        if (!isCollected)
        {
            // ʹ��PingPongʵ����ɫ����Ч��
            float lerpValue = Mathf.PingPong(Time.time * breathSpeed, 1f);
            Color currentColor = Color.Lerp(normalColor, highlightColor, lerpValue);

            if (objectMaterial != null)
            {
                objectMaterial.color = currentColor; // �޸Ĳ��ʵ���ɫ
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCollection playerCollection = other.GetComponentInChildren<PlayerCollection>();
            if (playerCollection != null)
            {
                playerCollection.CollectItem(); // �ռ���Ʒ��ͳ������
                ShowCollectedEffect(); // ��ʾ�ռ���Ч

                // ��ʼ��СЧ��
                if (!isCollected)
                {
                    isCollected = true; // ���Ϊ���ռ�
                    collectNum++;
                    if(collectNum<3&&collectNum>0)
                        mouse1.SetActive(true);
                    if (collectNum == 3)
                    {
                        mouse2.SetActive(true);
                        mouse1.SetActive(false);
                    }
                    StartCoroutine(ShrinkAndDestroy()); // ������С������Э��
                }
            }
        }
    }

    private void ShowCollectedEffect()
    {
        if (collectedEffect != null)
        {
            AudioManaager.Instance.Play("get1");
            Instantiate(collectedEffect, transform.position, Quaternion.identity); // ��ʾ�ռ���Ч
        }
    }

    private IEnumerator ShrinkAndDestroy()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * shrinkFactor;

        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            // ʹ��Lerpʵ��ƽ����С
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ȷ�����մ�С��Ŀ���С
        transform.localScale = targetScale;

        // ����С��ɺ���������
        Destroy(gameObject, 0.2f); // �ӳ�������Ʒ����ȷ����СЧ�����
    }
}
