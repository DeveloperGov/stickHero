using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGroup : MonoBehaviour
{
    public bool moved = true;
    private Vector3 target;

    void Start()
    {
        target = new Vector3 (-1.06f, -1.17f, 2.41f); //set targer point at present one to keep the moving group on its position

    }

    void FixedUpdate()
    {
        if (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 7);
        }
        else if (transform.position == target && !moved)
        {   //set new target position in case if it's nessasery
            GamePlay.arrivedForMoving = false;
            target = new Vector3(transform.position.x - 6f, transform.position.y, transform.position.z);
            moved = true;
        }
        if (GamePlay.arrivedForMoving)
        {
            moved = false;
        }
    }
}
