using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip clickSound;
    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        clickSound = Resources.Load<AudioClip>("breaker_switch");
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound()
    {
        audioSource.PlayOneShot(clickSound);
    } 
}
