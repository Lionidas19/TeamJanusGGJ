using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCmovement : MonoBehaviour
{
    [Header("List of NPC checkpoints")]
    [Tooltip("The NPC will move from the first checkpoint to the next. If one checkpoint with the tag Patrol is inside the list, then the NPC will initiate a patrol animation. If no checkpoints are added the player will remain static.")]
    public List<GameObject> checkpoints;
    [Header("Patrol FOV child of NPC")]
    public GameObject child;
    [Header("Animator prefab")]
    public Animator patrol;
    [Header("Animation clip prefab")]
    public AnimationClip patrolClip;

    private Rigidbody2D rigidbody;

    [SerializeField] private FOV FoV;

    [Header("Looking direction for when the NPC should remain in place.")]
    [Tooltip("E.g. a vector of (0, 1) will point upwards, a (1, 0) will point right, a (1, 1) will point right and up, etc.")]
    public Vector2 StaticLookingDirection; 

    [Header("NPC movement speed")]
    public float MovementSpeed;

    [Header("Patrol animation management")]
    public float timeToStayBeforeNextCheckpoint;
    public float timeToPatrol;

    private float Timer;
    private float patrolTimer;
    private bool patrolCheckpoint;
    private float patrollingTimer;

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
        if(checkpoints.Count > 1)
        {
            lastCheckpoint = checkpoints[0].transform.position;
            nextCheckpoint = checkpoints[1].transform.position;
            transform.position = checkpoints[0].transform.position;
        }
        Timer = Time.time;
        patrollingTimer = 0;
    }

    void FixedUpdate()
    {
        if(LightOrDark.stop == false)
        {
            if(checkpoints.Count > 1)
            {
                if (Vector2.Distance(checkpoints[checkpointIndex].transform.position, transform.position) > 0.1f && Time.time - Timer < timeToStayBeforeNextCheckpoint)
                {
                    Vector2 movementInput = (nextCheckpoint - (Vector2)transform.position).normalized;
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
                else if(patrolCheckpoint == false && patrolCheckpoint == false)
                {
                    if(checkpoints[checkpointIndex].tag == "Patrol")
                    {
                        patrolCheckpoint = true;
                        patrolTimer = Time.time;
                        Timer = Time.time + timeToPatrol;
                        rigidbody.velocity = Vector2.zero;
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
                    if(Time.time - patrolTimer < timeToPatrol)
                    {
                        if(Time.time - patrollingTimer < patrolClip.length)
                        {
                            Vector3 angleAnim = AngleFromZ(child.transform.localRotation.eulerAngles + checkpoints[checkpointIndex].transform.rotation.eulerAngles);
                            FoV.SetAimDirection(angleAnim);
                            FoV.SetOrigin(transform.position);
                        }
                        else
                        {
                            patrol.SetTrigger("Start");
                            patrollingTimer = Time.time;
                        }
                    }
                    else
                    {
                        patrolCheckpoint = false;
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
            }
            else
            {
                if (checkpoints.Count == 1)
                {
                    if(checkpoints[0].tag == "Patrol")
                    {
                        if (Time.time - patrollingTimer < patrolClip.length)
                        {
                            Vector2 angleAnim = AngleFromZ(child.transform.localRotation.eulerAngles + checkpoints[0].transform.rotation.eulerAngles);
                            FoV.SetAimDirection(angleAnim);
                            FoV.SetOrigin(transform.position);
                        }
                        else
                        {
                            patrollingTimer = Time.time;
                            patrol.SetTrigger("Start");
                        }
                    }
                    else
                    {
                        FoV.SetAimDirection(StaticLookingDirection);
                        FoV.SetOrigin(transform.position);
                    }
                }
                else
                {
                    FoV.SetAimDirection(StaticLookingDirection);
                    FoV.SetOrigin(transform.position);
                }
            }
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    Vector2 AngleFromZ(Vector3 angle)
    {
        float x;
        float y;

        y = Mathf.Cos(angle.z * Mathf.Deg2Rad);
        x = -Mathf.Sin(angle.z * Mathf.Deg2Rad);

        return new Vector2(x, y);
    }
}
