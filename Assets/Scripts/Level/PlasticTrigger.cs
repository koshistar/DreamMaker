using UnityEngine;

public class PlasticTrigger : MonoBehaviour
{
    public GameObject childObjectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            if (childObjectToActivate != null)
            {
                childObjectToActivate.SetActive(true);
            }
            else
            {
                Debug.LogWarning("������δָ����");
            }
        }
    }
}
