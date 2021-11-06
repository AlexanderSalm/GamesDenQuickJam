using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongInfo : MonoBehaviour
{
    [HideInInspector]
    public AudioSource intro;
    [HideInInspector]
    public AudioSource loop;

    [Range(0.0f, 1.0f)]
    public float volume;

    public AudioSource[] GetSources() {
        AudioSource[] sources = GetComponents<AudioSource>();
        return sources;
    }
}
