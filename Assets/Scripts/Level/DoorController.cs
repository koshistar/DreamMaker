using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public string openAnimationTrigger = "OpenDoor";
    public string idleAnimationState = "Idle";

    private bool isDoorOpening = false;  // 标记门是否在打开

    private void Update()
    {
        if (isDoorOpening)
        {
            AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(openAnimationTrigger) && stateInfo.normalizedTime >= 1.0f)
            {
                isDoorOpening = false;
            }
        }
    }

    public void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger(openAnimationTrigger);
            isDoorOpening = true;

            StartCoroutine(ResetDoorAfterDelay(6f));
        }

    }

    private IEnumerator ResetDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        ResetDoorToIdle();
    }

    private void ResetDoorToIdle()
    {
        if (doorAnimator != null)
        {
            doorAnimator.ResetTrigger(openAnimationTrigger);
        }
    }
}
