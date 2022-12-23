using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeCenterTrigger : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        obstaclesScript sc = other.GetComponent<obstaclesScript>();
        if (other.CompareTag("obstacle"))
        {
            if (sc!=null && sc.type == ObstacleType.movingObj)
                sc.stopMoving();
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        obstaclesScript sc = other.GetComponent<obstaclesScript>();
        if (other.CompareTag("obstacle"))
        {
            if (sc.type == ObstacleType.movingObj)
                sc.moveCoroutine = StartCoroutine(sc.move());
        }
    }*/
}
