using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static Action ObjectDroppedEvent;
    // sound manager
    // board controller 
    // level manager
    public static Action ObstacleDroppedEvent;
    public static Action MagnetDroppedEvent;

    private void Start()
    {
        ObjectDroppedEvent += () => Debug.Log("object dropped   1");
        ObjectDroppedEvent += fun;
    }
    public void fun()
    {
        Debug.Log("object dropped");
    }
}
