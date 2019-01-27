using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    static MusicManager instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void PlayClip(AudioClip clip)
    {
        AudioSource source = instance.GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
    }
}
