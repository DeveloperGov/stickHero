using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start : MonoBehaviour
{

    public Text Game_name;
    public GameObject Buttons, Camera;

    private void OnMouseDown()
    {
        Game_name.text = "Have a nice play";

        StartCoroutine(Timer());       
    }

    IEnumerator Timer()
    {
        Camera.GetComponent<Animation>().Play("Camera_Animation");

        yield return new WaitForSeconds(1);

        Buttons.gameObject.SetActive(false);
        Destroy (Game_name);
        StopCoroutine("Timer");
    }
 
}
