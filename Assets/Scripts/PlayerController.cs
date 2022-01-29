using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ControlScheme controls;

    public AudioSource Pop;

    GameObject camera;
    
    private Rigidbody2D rigidbody;

    public float MovementSpeed;

    void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        controls = new ControlScheme();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (LightOrDark.stop == false)
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
        else
        {
            rigidbody.velocity = Vector2.zero;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FOV")
        {
            LightOrDark.stop = true;
            Pop.Play();
            StartCoroutine("YoureDead");
        }
    }

    IEnumerator YoureDead()
    {
        //Pop.PlayOneShot(clip);
        yield return new WaitForSeconds(3);
        DoLast();
    }

    void DoLast()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
