using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] private FOV FoV;
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
    #endregion 

    [Header("Target")]
    public float DistanceFromTarget;
    float closestDistanceFromTarget;
    public float minDistanceFromTarget;
    public GameObject target;

    bool playWind, playedWind;

    void Start()
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

    // Update is called once per frame
    void Update()
    {

        if(Time.time > BirdsTimer)
        {
            print("Birds");
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
            }
            else
            {
                LightMusic.volume = 0;
            }
        }
        if(playWind == true && playedWind == false)
        {
            playedWind = true;
            Wind.Play();
        }
    }
}
