using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCmovement : MonoBehaviour
{
    public List<GameObject> checkpoints;

    private Rigidbody2D rigidbody;

    [SerializeField] private FOV FoV;

    public float MovementSpeed;

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
        transform.position = checkpoints[0].transform.position;
    }

    void FixedUpdate()
    {
        if(LightOrDark.stop == false)
        {
            if (Vector2.Distance(checkpoints[checkpointIndex].transform.position, transform.position) > 1)
            {
                Vector2 movementInput = (nextCheckpoint - lastCheckpoint).normalized;
                rigidbody.velocity = movementInput * MovementSpeed;
                if (movementInput.x >= 0)
                {
                    rigidbody.MoveRotation(0);
                }
                else if (movementInput.x < 0)
                {
                    rigidbody.MoveRotation(-180);
                }
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
