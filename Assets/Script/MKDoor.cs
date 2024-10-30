using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKDoor : MonoBehaviour
{
    public Animator MKDAnimation;
    public AudioSource audioSource;
    public AudioClip triggerSound;
    // Start is called before the first frame update

    public void TriggerOpen()
    {
        MKDAnimation.SetBool("open", true);
    }
    public void TriggerClose() {
        MKDAnimation.SetBool("open", false);
    }
    public void TriggerLock() {
        MKDAnimation.SetBool("lock", true);
        MKDAnimation.SetTrigger("DoorLock");
        audioSource.clip = triggerSound;
        audioSource.Play();
    }
    public void TriggerUnLock()
    {
        MKDAnimation.SetBool("lock", false);
    }
}
