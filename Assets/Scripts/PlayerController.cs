using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    GameObject[] Keys;

    int KeysPickedUp;

    public Animator transition;

    public float transitionTime = 1f;

    public ControlScheme controls;

    public AudioSource Pop;

    GameObject camera;

    public bool is_in_hiding = false;
    
    private Rigidbody2D rigidbody;

    public float MovementSpeed;

    void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        controls = new ControlScheme();
        rigidbody = GetComponent<Rigidbody2D>();
        KeysPickedUp = 0;
    }

    private void Start()
    {
        if(LightOrDark.light == true)
        {
            transform.position = LightOrDark.MCPositions[LightOrDark.numberOfDark];
        }
        Keys = GameObject.FindGameObjectsWithTag("Key");
    }

    void FixedUpdate()
    {
        if (LightOrDark.stop == false && (Keys.Length == 0 || (Keys.Length > 0 && KeysPickedUp < Keys.Length)))
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
            if(is_in_hiding) return;
            LightOrDark.stop = true;
            Pop.Play();
            StartCoroutine("YoureDead");
        }
        if(collision.tag == "Key")
        {
            collision.gameObject.SetActive(false);
            KeysPickedUp++;
            if(KeysPickedUp >= Keys.Length)
            {
                StartCoroutine("MoveOn");
            }
        }
    }

    IEnumerator MoveOn()
    {
        transition.SetTrigger("Start");
        LightOrDark.light = true;
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Light");
    }

    IEnumerator YoureDead()
    {
        //Pop.PlayOneShot(clip);
        yield return new WaitForSeconds(Pop.clip.length);
        StartCoroutine(DoLast());
    }

    IEnumerator DoLast()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Hide()
    {
        Debug.Log("Player hiding");
        is_in_hiding = true;
    }

    public void Unhide()
    {
        Debug.Log("Player unhiding");
        is_in_hiding = false;
    }
}
