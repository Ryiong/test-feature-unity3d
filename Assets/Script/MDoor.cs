using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDoor : MonoBehaviour
{
    public Animator MAnimation;
    public AudioSource audioSource; 
    public AudioClip triggerSound;
    public void TriggerLock()
    {
        MAnimation.SetBool("lock", true);
        MAnimation.SetTrigger("DoorLock");
        audioSource.clip = triggerSound;
        audioSource.Play();
    }
}
