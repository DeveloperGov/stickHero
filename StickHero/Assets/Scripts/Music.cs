using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject stick;
    public GameObject background;

    private void OnMouseUp()
    {
        if (background.GetComponent<AudioSource>().volume > 0)
        {
            stick.GetComponent<AudioSource>().volume = 0;
            background.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            stick.GetComponent<AudioSource>().volume = 1;
            background.GetComponent<AudioSource>().volume = 0.05f;
        }
    }
}
