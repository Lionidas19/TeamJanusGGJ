using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicLight : MonoBehaviour
{
    #region Sound Clips
    [Header("Music Tracks")]
    public AudioSource LightMusic;
    [Header("Bad Sound Effects")]
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
    public List<GameObject> target;

    bool playWind, playedWind;

    void Awake()
    {
        LightMusic.volume = 0.7f;
        Wind.volume = 1;
        Birds.volume = 0.05f;
        ManyBirds.volume = 0.7f;
        CountrySide.volume = 0.6f;
        Footsteps.volume = 1;

        LightOrDark.stop = false;
        LightOrDark.light = true;
    }

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
        if (LightOrDark.stop == false)
        {
            if (Time.time > BirdsTimer && playWind == false)
            {
                Birds.Play();
                BirdsTimer = Time.time + Random.Range(1.3f * Birds.clip.length, 2 * Birds.clip.length);
            }

            int closestTarget = 0;

            for (int i = 1; i < target.Count; i++)
            {
                if (Vector2.Distance(target[i].transform.position, transform.position) < Vector2.Distance(target[closestTarget].transform.position, transform.position))
                {
                    closestTarget = i;
                }
            }

            float playerToTarget = Vector2.Distance(target[closestTarget].transform.position, transform.position);
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
            LightMusic.volume = 0;
            Wind.volume = 0;
            Birds.volume = 0;
            ManyBirds.volume = 0;
            CountrySide.volume = 0;
            Footsteps.volume = 0;
        }
    }
}
