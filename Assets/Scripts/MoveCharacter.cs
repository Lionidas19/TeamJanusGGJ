using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    private bool lookingDirection;

    public float MovementSpeed;

    void Start()
    {
        lookingDirection = false;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 move = (Vector2)transform.position;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            move += (Vector2)transform.up * Time.deltaTime * MovementSpeed;
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            lookingDirection = false;
            move += (Vector2)transform.right * Time.deltaTime * MovementSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            move += -(Vector2)transform.up * Time.deltaTime * MovementSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            lookingDirection = true;
            move += -(Vector2)transform.right * Time.deltaTime * MovementSpeed;
        }
        if (lookingDirection)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
        }
        transform.position = move;
    }
}
