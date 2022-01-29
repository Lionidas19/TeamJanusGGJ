using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCmovement : MonoBehaviour
{
    public List<GameObject> checkpoints;

    private Rigidbody2D rigidbody;

    [SerializeField] private FOV FoV;

    public float MovementSpeed;

    private float Timer;

    Vector2 lastCheckpoint;
    Vector2 nextCheckpoint;
    Vector2 lastDirection;

    int checkpointIndex;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        lastDirection = Vector2.zero;
        checkpointIndex = 0;
        lastCheckpoint = checkpoints[0].transform.position;
        nextCheckpoint = checkpoints[1].transform.position;
        transform.position = checkpoints[0].transform.position;
        Timer = Time.time;
    }

    void FixedUpdate()
    {
        if(LightOrDark.stop == false)
        {
            if (Vector2.Distance(checkpoints[checkpointIndex].transform.position, transform.position) > 0.1f && Time.time - Timer < 5)
            {
                Vector2 movementInput = (nextCheckpoint - /*lastCheckpoint*/(Vector2)transform.position).normalized;
                rigidbody.velocity = movementInput * MovementSpeed;
                if (movementInput == Vector2.zero)
                {
                    FoV.SetAimDirection(lastDirection);
                }
                else
                {
                    FoV.SetAimDirection(movementInput.normalized);
                    lastDirection = movementInput.normalized;
                }

                FoV.SetOrigin(transform.position);
            }
            else
            {
                Timer = Time.time;
                lastCheckpoint = checkpoints[checkpointIndex].transform.position;
                checkpointIndex++;
                if (checkpointIndex >= checkpoints.Count)
                {
                    checkpointIndex = 0;
                }
                nextCheckpoint = checkpoints[checkpointIndex].transform.position;
            }
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}
