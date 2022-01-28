using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float distanceToTarget;
    float closestDistance;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.transform.position, transform.position) <= distanceToTarget)
        {
            if(Vector2.Distance(player.transform.position, transform.position) <= closestDistance)
            {
                closestDistance = Vector2.Distance(player.transform.position, transform.position);

            }
        }
    }
}
