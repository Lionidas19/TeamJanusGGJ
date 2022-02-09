using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManagement : MonoBehaviour
{
    public List<GameObject> npcs;

    // Start is called before the first frame update
    void Start()
    {
        if(npcs.Count == 0)
        {
            print("Add NPCS");
        }
        else
        {
            if(LightOrDark.numberOfDark == 0)
            {
                npcs[0].gameObject.SetActive(true);
                npcs[2].gameObject.SetActive(true);
                npcs[4].gameObject.SetActive(true);
                npcs[1].gameObject.SetActive(false);
                npcs[3].gameObject.SetActive(false);
                npcs[5].gameObject.SetActive(false);
            }
            else if (LightOrDark.numberOfDark == 1)
            {
                npcs[0].gameObject.transform.position = new Vector2(100, 100);
                npcs[0].gameObject.SetActive(false);
                npcs[2].gameObject.SetActive(true);
                npcs[4].gameObject.SetActive(true);
                npcs[1].gameObject.SetActive(true);
                npcs[3].gameObject.SetActive(false);
                npcs[5].gameObject.SetActive(false);
            }
            else if (LightOrDark.numberOfDark == 2)
            {
                npcs[0].gameObject.SetActive(false);
                npcs[2].gameObject.SetActive(false);
                npcs[4].gameObject.SetActive(true);
                npcs[1].gameObject.SetActive(true);
                npcs[3].gameObject.SetActive(true);
                npcs[5].gameObject.SetActive(false);
            }
            else if (LightOrDark.numberOfDark == 3)
            {
                npcs[0].gameObject.SetActive(false);
                npcs[2].gameObject.SetActive(false);
                npcs[4].gameObject.SetActive(false);
                npcs[1].gameObject.SetActive(true);
                npcs[3].gameObject.SetActive(true);
                npcs[5].gameObject.SetActive(true);
            }
        }
    }
}
