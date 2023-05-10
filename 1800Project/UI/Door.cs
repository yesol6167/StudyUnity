using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool OpenCheck = false;

    // Update is called once per frame
    void Update()
    { 
        if (OpenCheck)
        {
            GetComponent<Animator>().SetBool("IsOpen", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("IsOpen", false);
        }
    }
}
