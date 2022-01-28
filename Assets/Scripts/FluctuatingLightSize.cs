using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluctuatingLightSize : MonoBehaviour
{
    public float minSize;
    public float maxSize;
    bool minOrMax;
    // Start is called before the first frame update
    void Start()
    {
        minOrMax = false;
    }

    // Update is called once per frame
    void Update()
    {
        Fluctuate();
               
    }

    void Fluctuate()
    {
        if (minOrMax == false)
        {
            if (transform.localScale.x <= maxSize)
            {
                transform.localScale += new Vector3(0.01f, 0.01f);
            }
            else
            {
                minOrMax = true;
            }
        }
        else if (minOrMax == true)
        {
            if (transform.localScale.x >= minSize)
            {
                transform.localScale -= new Vector3(0.01f, 0.01f);
            }
            else
            {
                minOrMax = false;
            }
        }
    }
}
