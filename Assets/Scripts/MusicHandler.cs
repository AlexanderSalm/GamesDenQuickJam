using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public GameObject song;

    private AudioSource intro;
    private AudioSource loop;

    public double timeInIntro;
    public double timeInLoop;

    void Start()
    {
        GameObject songObject = Instantiate(song, Vector3.zero, Quaternion.identity, GetComponent<Transform>());
        songObject.name = "Current Song: " + song.name;

        SongInfo songScript = songObject.GetComponent<SongInfo>();
        AudioSource[] sources = songScript.GetSources();
        intro = sources[0];
        loop = sources[1];

        intro.volume = songScript.volume;
        loop.volume = songScript.volume;

        double introDuration = (double)intro.clip.samples / intro.clip.frequency;
        double startTime = AudioSettings.dspTime + 0.1;
        intro.PlayScheduled(startTime);
        loop.PlayScheduled(startTime + introDuration);

    }

    void Update()
    {
        timeInIntro = intro.time;
        timeInLoop = loop.time;
    }
}
