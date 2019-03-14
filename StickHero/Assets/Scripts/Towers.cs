using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{

    static public GameObject newTower;
    public GameObject groupToMove;
    public GameObject tower;    
    private Vector3 towerPos;
    private bool TowerCame = false;
    public static float newTowerW;
    public static GameObject CurrentTower;
    public float CurrentTowerW;

    public void Start()
    {

        CurrentTower = GameObject.Find("StartTower");
        CurrentTowerW = CurrentTower.GetComponent<MeshRenderer>().bounds.size.x;

        towerPos = new Vector3(Random.Range (0f, 6.5f), -6.451f, 0); //set quasi random position for new tower, create new tower
        newTower = Instantiate(tower, new Vector3(9f, -6.451f, 0f), Quaternion.identity); 
        newTower.transform.localScale = new Vector3(Random.Range(1f, 3f), 11.76f, 0.1f);

        newTower.transform.parent = groupToMove.transform;
        newTowerW = newTower.GetComponent<Renderer>().bounds.size.x;
    }

    public void Update()
    {
        if (newTower.transform.position != towerPos && !TowerCame)
        {//if tower has not come to target  position - move it to target position
            newTower.transform.position = Vector3.MoveTowards(newTower.transform.position, towerPos, 3 * Time.deltaTime);
        }
        else if (newTower.transform.position == towerPos)
        {
            TowerCame = true;
        }


        if (GamePlay.arrived) 
        {// when Player has come to direct tower new one is being created

            Destroy(CurrentTower);
            CurrentTower = newTower;
            CurrentTowerW = CurrentTower.GetComponent<MeshRenderer>().bounds.size.x;

            towerPos = new Vector3(Random.Range(0f, 5f) + newTower.transform.localScale.x, -6.451f, 0);
            newTower = Instantiate(tower, new Vector3(9f, -6.451f, 0f), Quaternion.identity);
            newTower.transform.localScale = new Vector3(Random.Range(1f, 3f), 11.76f, 0.1f);


            newTower.transform.parent = groupToMove.transform;
            newTowerW = newTower.GetComponent<Renderer>().bounds.size.x;

            newTower.transform.parent = groupToMove.transform;

            

            TowerCame = false;
            GamePlay.arrived = false;
        }
    }
}
