using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToLoad : MonoBehaviour
{
    public GameObject white;
    public GameObject black;

    public List<string> encouragment;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = encouragment[Random.Range(0, encouragment.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if(LightOrDark.caught == false)
        {
            white.gameObject.SetActive(true);
            black.gameObject.SetActive(false);
        }
        else
        {
            white.gameObject.SetActive(false);
            black.gameObject.SetActive(true);
        }
    }
}
