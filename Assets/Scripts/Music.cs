using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    #region Sound Clips
    [Header("Music Tracks")]
    public AudioSource DarkMusic1;
    public AudioSource DarkMusic2;
    public AudioSource LightMusic;
    [Header("Bad Sound Effects")]
    public AudioSource Howling;
    public AudioSource HeartBeat;
    public AudioSource HeavyBreathing;
    public AudioSource Wind;
    [Header("Good Sound Effects")]
    public AudioSource Birds;
    public AudioSource ManyBirds;
    public AudioSource CountrySide;
    public AudioSource Footsteps;

    float BirdsTimer;
    float HowlingTimer;
    #endregion 

    [Header("Target")]
    public float DistanceFromTarget;
    float closestDistanceFromTarget;
    public float minDistanceFromTarget;
    public GameObject target;

    bool playWind, playedWind;

    void Start()
    {
        if(LightOrDark.light == true)
        {
            LightMusic.Play();
            Birds.Play();
            ManyBirds.Play();
            CountrySide.Play();
            closestDistanceFromTarget = DistanceFromTarget;
            playWind = false;
            playedWind = false;
            BirdsTimer = Time.time + Random.Range(1.3f * Birds.clip.length, 2 * Birds.clip.length);
        }
        else
        {
            DarkMusic1.Play();
            //DarkMusic2.Play();
            Howling.Play();
            HeartBeat.Play();
            HowlingTimer = Time.time + Random.Range(1.3f * Howling.clip.length, 2 * Howling.clip.length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(LightOrDark.light == true)
        {
            if (Time.time > BirdsTimer && playWind == false)
            {
                Birds.Play();
                BirdsTimer = Time.time + Random.Range(1.3f * Birds.clip.length, 2 * Birds.clip.length);
            }

            float playerToTarget = Vector2.Distance(target.transform.position, transform.position);            
            if (playerToTarget <= closestDistanceFromTarget)
            {
                playWind = true;
                closestDistanceFromTarget = playerToTarget;
                float ratio = (closestDistanceFromTarget - minDistanceFromTarget) / (DistanceFromTarget - minDistanceFromTarget);
                if (ratio > 0)
                {
                    LightMusic.volume = ratio;
                    Birds.volume = ratio;
                    ManyBirds.volume = ratio;
                    CountrySide.volume = ratio;
                }
                else
                {
                    LightMusic.volume = 0;
                    Birds.volume = 0;
                    ManyBirds.volume = 0;
                    CountrySide.volume = 0;
                }
            }
            if (playWind == true && playedWind == false)
            {
                playedWind = true;
                Wind.Play();
            }
        }
        else
        {
            if (Time.time > HowlingTimer)
            {
                Howling.Play();
                HowlingTimer = Time.time + Random.Range(1.3f * Howling.clip.length, 2 * Howling.clip.length);
            }
        }
    }
}
