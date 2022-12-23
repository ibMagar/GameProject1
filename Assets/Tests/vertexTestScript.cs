using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vertexTestScript : MonoBehaviour
{

    /*public MeshFilter cubeMeshFilter;
    public Mesh cubeMesh;
    public List<Vector3> vertexList;
    public Transform center;
    public float radius;
    private void Start()
    {
        cubeMeshFilter = GetComponent<MeshFilter>();
        cubeMesh = cubeMeshFilter.mesh;
        getVertex();
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            getVertex();
    }
    void getVertex()
    {
        for(int i=0;i<cubeMesh.vertices.Length;i++)
        {
            //print(cubeMesh.vertices[i]);
            //vertexList.Add(cubeMesh.vertices[i]);
            if(Vector3.Distance(center.position,cubeMesh.vertices[i])<radius)
            {
                print(cubeMesh.vertices[i]);
            }
        }
    }
    private void OnDrawGizmos()
    {
        *//*Gizmos.color = Color.red;
        for (int i = 0; i < cubeMeshFilter.mesh.vertices.Length; i++)
        {
            Gizmos.DrawLine(center.position, cubeMeshFilter.mesh.vertices[i]);
        }*//*
    }*/
    public Vector3 pos;
    public int touchCount;
    private void Update()
    {
        touchCount = Input.touchCount;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase==TouchPhase.Moved)
        {

             pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            print(pos);
        }
    }
}