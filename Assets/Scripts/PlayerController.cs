using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public ControlScheme controls;

    public DialogueSystem.DialogueSceneManager manager;

    public LayerMask layerMask;

    GameObject camera;
    
    private Rigidbody2D rigidbody;

    public float MovementSpeed;

    public Text text;

    Vector2 lastDirection;

    [HideInInspector]
    public bool nearNPCs;
    [HideInInspector]
    public string name;

    void Awake()
    {
        camera = GameObject.Find("Main Camera");
        controls = new ControlScheme();
        rigidbody = GetComponent<Rigidbody2D>();
        nearNPCs = false;
        text.enabled = false;
    }

    void Start()
    {
        lastDirection = Vector2.zero;
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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 3.0f, layerMask);
        int closestNPC = 0;
        if(colliders.Length == 0)
        {
            nearNPCs = false;
            text.enabled = false;
        }
        else if (colliders.Length > 1)
        {
            nearNPCs = true;
            for (int i = 1; i < colliders.Length; i++)
            {
                if(Vector2.Distance(transform.position, colliders[i].transform.position) < Vector2.Distance(transform.position, colliders[closestNPC].transform.position))
                {
                    closestNPC = i;
                }
            }
        }
        if(nearNPCs == true)
        {
            name = colliders[closestNPC].gameObject.name;
            manager.name = name;
            text.enabled = true;
            text.text = "Press E to talk to " + name;
            
        }
        else
        {
            name = "";
            manager.name = name;
            text.enabled = false;
        }
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
