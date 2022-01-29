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
    public List<GameObject> target;

    bool playWind, playedWind;

    [Header("Children")]
    public GameObject Black;
    public GameObject ScrollingTexture;

    void Awake()
    {
        DarkMusic1.volume = 0.5f;
        DarkMusic2.volume = 0.3f;
        LightMusic.volume = 0.7f;
        Howling.volume = 0.2f;
        HeartBeat.volume = 0.5f;
        HeavyBreathing.volume = 0.4f;
        Wind.volume = 1;
        Birds.volume = 0.05f;
        ManyBirds.volume = 0.7f;
        CountrySide.volume = 0.6f;
        Footsteps.volume = 1;

        LightOrDark.stop = false;
    }

    void Start()
    {
        
        if(LightOrDark.light == true)
        {
            Black.gameObject.SetActive(false);
            ScrollingTexture.gameObject.SetActive(false);
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
            Black.gameObject.SetActive(true);
            ScrollingTexture.gameObject.SetActive(true);
            DarkMusic1.Play();
            DarkMusic2.Play();
            Howling.Play();
            HeartBeat.Play();
            HowlingTimer = Time.time + Random.Range(1.3f * Howling.clip.length, 2 * Howling.clip.length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LightOrDark.stop == false)
        {
            if (LightOrDark.light == true)
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
                if (Time.time > HowlingTimer)
                {
                    Howling.Play();
                    HowlingTimer = Time.time + Random.Range(1.3f * Howling.clip.length, 2 * Howling.clip.length);
                }
            }
        }
        else
        {
            DarkMusic1.volume = 0;
            DarkMusic2.volume = 0;
            LightMusic.volume = 0;
            Howling.volume = 0;
            HeartBeat.volume = 0;
            HeavyBreathing.volume = 0;
            Wind.volume = 0;
            Birds.volume = 0;
            ManyBirds.volume = 0;
            CountrySide.volume = 0;
            Footsteps.volume = 0;
        }
    }
}
