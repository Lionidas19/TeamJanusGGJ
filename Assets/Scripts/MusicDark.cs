using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDark : MonoBehaviour
{
    #region Sound Clips
    [Header("Music Tracks")]
    public AudioSource DarkMusic1;
    public AudioSource DarkMusic2;
    [Header("Bad Sound Effects")]
    public AudioSource Howling;
    public AudioSource HeartBeat;
    public AudioSource HeavyBreathing;

    float HowlingTimer;
    #endregion 

    void Awake()
    {
        DarkMusic1.volume = 0.5f;
        DarkMusic2.volume = 0.3f;
        Howling.volume = 0.2f;
        HeartBeat.volume = 0.5f;
        HeavyBreathing.volume = 0.4f;

        LightOrDark.stop = false;
        LightOrDark.light = false;
        LightOrDark.numberOfDark++;
    }

    void Start()
    {
        DarkMusic1.Play();
        DarkMusic2.Play();
        Howling.Play();
        HeartBeat.Play();
        HowlingTimer = Time.time + Random.Range(1.3f * Howling.clip.length, 2 * Howling.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        if (LightOrDark.stop == false)
        {
            if (Time.time > HowlingTimer)
            {
                Howling.Play();
                HowlingTimer = Time.time + Random.Range(1.3f * Howling.clip.length, 2 * Howling.clip.length);
            }
        }
        else
        {
            DarkMusic1.volume = 0;
            DarkMusic2.volume = 0;
            Howling.volume = 0;
            HeartBeat.volume = 0;
            HeavyBreathing.volume = 0;
        }
    }
}
