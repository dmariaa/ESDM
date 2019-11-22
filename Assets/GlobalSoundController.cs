using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlobalSoundController : MonoBehaviour
{
    public void PlayMusic()
    {
        AudioSource audioData = GetComponent<AudioSource>();
        audioData.loop = true;
        audioData.Play(0);
    }

    public void StopMusic()
    {
        AudioSource audioData = GetComponent<AudioSource>();
        audioData.Stop();
    }
}
