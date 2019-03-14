using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public GameObject player;


    void Start()
    {
        player = GameObject.Find("Player");
        GetComponent<Text>().text = ("Best ") + PlayerPrefs.GetInt("High Score").ToString();
    }

    private void OnMouseDown()
    {
        player.transform.position = new Vector3(Towers.CurrentTower.transform.position.x + Towers.CurrentTower.transform.localScale.x / 2.3f, 0, 0);
        player.GetComponent<GamePlay>().ResetStick();
    }

}


