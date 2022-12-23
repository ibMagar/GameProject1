using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class magnet : MonoBehaviour
{
    [Header("objects")]
    public  List<Transform> affectedObjects;

    [Header("variables")]
    public float magnetDuration = 10f;
    [SerializeField] float rotationSpeed;
    public float magnetScale = 1f;
    private Vector3 initialMagnetScale;
    [SerializeField] float magnetForce;
    
    private void Start()
    {
        affectedObjects = new List<Transform>();
        setMagnetSize();
        affectedObjects.Clear();
    }
    public void setMagnetSize()
    {
        initialMagnetScale = transform.localScale;           //getting the parent local Scale
        transform.localScale = initialMagnetScale * magnetScale;
    }
    private void FixedUpdate()
    {
        if(!GameData.isGameOver)
        {
            foreach(Transform t in affectedObjects)
            {
                if(t!=null)
                { 
                Vector3 dir = (transform.position - t.position).normalized;
                t.GetComponent<Rigidbody>().AddForce(dir * magnetForce);
                }
            }
        }
        //transform.localEulerAngles += new Vector3(0, 0, 1) * rotationSpeed * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (tag.Equals("object") || tag.Equals("obstacle") || tag.Equals("camo")||tag.Equals("chargeObject"))
        {
            affectedObjects.Add(other.transform);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        string tag = other.tag;
        if (tag.Equals("object") || tag.Equals("obstacle") || tag.Equals("camo") || tag.Equals("chargeObject"))
        {
            affectedObjects.Remove(other.transform);
        }
    }

}
