using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtext : MonoBehaviour
{
    public GameObject Image;
    public GameObject Location;
    public float timeToEnable;
    public float timeToDisable;
    float enableTimer;
    float disableTimer;

    private void Start()
    {
        enableTimer = Time.time;
        disableTimer = Time.time;
        Image.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        Image.transform.position = Camera.main.WorldToScreenPoint((Vector2)Location.transform.position);
        if(Time.time - enableTimer > timeToEnable && Image.active == false)
        {
            Image.active = true;
            disableTimer = Time.time;
        }
        if(Time.time - disableTimer > timeToDisable && Image.active == true)
        {
            Image.active = false;
            enableTimer = Time.time;
        }
    }
}
