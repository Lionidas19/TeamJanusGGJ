using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
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
            LightOrDark.light = false;

            if(gameObject.name == "Light")
            {
                LightOrDark.Position = (Vector2)collision.gameObject.transform.position + new Vector2(2, 0);
            }

            SceneManager.LoadScene(gameObject.name);
        }
    }
}
