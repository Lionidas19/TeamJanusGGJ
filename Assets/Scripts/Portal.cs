using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            LightOrDark.light = !LightOrDark.light;

            if (LightOrDark.light == true)
            {
                LightOrDark.Position = (Vector2)collision.gameObject.transform.position + new Vector2(2, 0);
            }

            StartCoroutine(LoadLevel(gameObject.name));
        }
    }

    IEnumerator LoadLevel(string light)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(light);
    }
}
