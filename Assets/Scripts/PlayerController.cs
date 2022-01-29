using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public ControlScheme controls;

    GameObject camera;
    
    private Rigidbody2D rigidbody;

    public float MovementSpeed;

    void Awake()
    {
        camera = GameObject.Find("Main Camera");
        controls = new ControlScheme();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movementInput = controls.Player.Movement.ReadValue<Vector2>();
        rigidbody.velocity = movementInput * MovementSpeed;
        if (movementInput.x >= 0.7)
        {
            rigidbody.MoveRotation(0);
        }
        else if (movementInput.x <= -0.7)
        {
            rigidbody.MoveRotation(-180);
        }
        
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
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
