using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePlay : MonoBehaviour
{
    public AudioSource stick_sound;

    public GameObject stick;    
    public GameObject Buttons;
    public GameObject player;
    
    public GameObject End_menu;

    GameObject Tower;


    public Text LiveScore;

    public static bool arrived = false;
    public static bool arrivedForMoving = false;

    public int Score = 0;
    float playerSpeed = 10f;
    private float yScale = 0f;
    
    bool canGrow;
    bool canRun = false;

    Vector3 stickPos;
    Vector3 grow_stick;

    public void Start()
    {
        stickPos = new Vector3 (0f, 0f, -90f);

        canGrow = true;

        stick.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.3f, player.transform.position.z);
        stick.transform.localScale = new Vector3(stick.transform.localScale.x, yScale, 0);
        stick.transform.rotation = Quaternion.identity;
 
        Tower = GetComponent<Towers>().tower;
    }

    void Update()
    {
        if (!Buttons.activeSelf)
        {
            //growing the stick
            if (Input.GetMouseButton(0) && grow_stick.y < 15 && canGrow)
            {
                float groingSpeed = 0.05f;
                grow_stick = stick.transform.localScale;    //growing the stick
                grow_stick.y += groingSpeed;
                stick.transform.localScale = grow_stick;

                Vector3 up_stick = stick.transform.position;  //moving the stick up twice slower than growing to keep the bottom point at its position
                up_stick.y += groingSpeed / 2;
                stick.transform.position = up_stick;

                stick_sound.Play();
            }
            //stop growing and start falling the stick
            if (Input.GetMouseButtonUp(0) && canGrow)
            {
                StartCoroutine(Fall());
                stick_sound.Stop();
                canGrow = false;
                grow_stick.y = 0;
            }
            //moving the player
            if (canRun && !PlayerStickEnd() && (stick.transform.position.x + stick.transform.localScale.y / 2 <= Towers.newTower.transform.position.x + Towers.newTower.transform.localScale.x/2))
            {
                player.transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0f, 0f));
            }

            else if (canRun && !PlayerStickEnd() && (stick.transform.position.x + stick.transform.localScale.y / 2 > Towers.newTower.transform.position.x + Towers.newTower.transform.localScale.x / 2))
            {
                PlayerLose();
            }

            if (canRun && PlayerStickEnd() && PlayerOnTower() && !PlayerTowerEnd())
            {
                player.transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0f, 0f));

            }

            else if (canRun && PlayerStickEnd() && !PlayerOnTower ())
            {
                PlayerLose();
            }

                if (PlayerTowerEnd())
            {
                canRun = false;
                arrived = true;
                arrivedForMoving = true;
                canGrow = true;
                Score++;
                ResetStick();
            }

            IEnumerator Fall()
            {
                for (int i = 0; i < 90; i++)
                {
                    canGrow = false;
                    yield return new WaitForEndOfFrame();
                    
                    stick.transform.RotateAround(new Vector3 (player.transform.position.x - 0.5f, player.transform.position.y - 0.1f, 0f), new Vector3(0, 0, -1), 1f);
                    //90 times rotates the stick for -1 degree around the point (x-0.5, y-0.1f, 0f)
                    
                }
                
                canRun = true;


                StopCoroutine(Fall());                
            }

            LiveScore.text = "" + Score;

            player.transform.rotation = Quaternion.identity;
        }
       
    }

    float ObjPosition(GameObject obj)
    {
        return obj.transform.position.x + obj.GetComponent<Renderer>().bounds.size.x / 2;
    }

    bool PlayerTowerEnd()
    {
        if (ObjPosition(player) < ObjPosition(Towers.newTower))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool PlayerStickEnd()
    {
        if (ObjPosition(player) >= ObjPosition(stick))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool PlayerOnTower ()
    {

        if (ObjPosition(player) < ObjPosition(Towers.newTower) + Towers.newTower.transform.localScale.x/2 && stick.transform.localScale.y - 0.2f + stick.transform.position.x < Towers.newTower.transform.position.x + Towers.newTower.transform.localScale.x/2)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    public void ResetStick()
    {
        StartCoroutine(Timer());
        stick.transform.position = player.transform.position;
        stick.transform.localScale = new Vector3(0.1f, 0.5f, 0);
        stick.transform.rotation = Quaternion.identity;
        
        IEnumerator Timer()
        {
            End_menu.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            canGrow = true;
            StopCoroutine("Timer");
        }

    }

    void PlayerLose () 
    {
        canRun = false;
        canGrow = false;

        End_menu.SetActive(true); //player sees end menu 
        Debug.Log("Lose");        
        // record saves if direct result is better than best one
        if (PlayerPrefs.GetInt ("High Score") < Score); 
        PlayerPrefs.SetInt("High Score", Score);

        Score = 0;
    }
}


