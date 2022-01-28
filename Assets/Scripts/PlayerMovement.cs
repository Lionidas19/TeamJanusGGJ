using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ControlScheme controls;
    
    private Rigidbody2D rigidbody;

    [SerializeField] private FOV FoV;

    public float MovementSpeed;
    Vector2 lastDirection;

    void Awake()
    {
        controls = new ControlScheme();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        lastDirection = Vector2.zero;
    }

    void FixedUpdate()
    {
        Vector2 movementInput = controls.Player.Movement.ReadValue<Vector2>();
        rigidbody.velocity = movementInput * MovementSpeed;
        if(movementInput.x >= 0.7)
        {
            rigidbody.MoveRotation(0);
        }
        else if (movementInput.x <= -0.7)
        {
            rigidbody.MoveRotation(-180);
        }
        if(movementInput == Vector2.zero)
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

    float GetAngleFromVectorFloat(Vector2 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
