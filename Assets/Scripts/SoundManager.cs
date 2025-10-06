using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public AudioSource[] SoundList;

    void Start()
    {
        SoundList = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}
