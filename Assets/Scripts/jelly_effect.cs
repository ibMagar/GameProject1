using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelly_effect : MonoBehaviour
{

    public Mesh OriginalMesh;          //original Mesh
    public Mesh CloneMesh;             // clone mesh inorder to not directly deal with original mesh

    private Renderer _Renderer;             // Mesh renderer

    public jelly_vertex[] vertices;

    public Vector3[] vertices_array;

     public float intensity=1f;
    public float mass=1f;
     public float stiffness=1f;
    [Range(0,1)] public  float damping=.75f;

    private void Start()
    {
        OriginalMesh = GetComponent<MeshFilter>().sharedMesh;
        CloneMesh = Instantiate(OriginalMesh);

        _Renderer = GetComponent<Renderer>();

        vertices = new jelly_vertex[CloneMesh.vertices.Length];
        for(int i=0;i<CloneMesh.vertices.Length;i++)
        {
            vertices[i] = new jelly_vertex(i, transform.TransformPoint(CloneMesh.vertices[i]));

        }
    }
    private void FixedUpdate()
    {
        vertices_array = OriginalMesh.vertices;
        for(int i=0;i<vertices.Length;i++)
        {
            Vector3 target = transform.TransformPoint(vertices_array[vertices[i].id]);
            float _intensity = (1 - (_Renderer.bounds.max.y - target.y) / _Renderer.bounds.size.y)*intensity;
            vertices[i].shake(target, mass, stiffness, damping);
            target = transform.InverseTransformPoint(vertices[i].position);
            vertices_array[vertices[i].id] = Vector3.Lerp(vertices_array[vertices[i].id], target, _intensity);

        }
        CloneMesh.vertices = vertices_array;
    }
    [System.Serializable]
    public class jelly_vertex                           // class for each vertex
    {
        public int id;
        public Vector3 position;
        public Vector3 velocity, force;
        public jelly_vertex(int _id,Vector3 _position)
        {
            id = _id;
            position = _position;
        }
        public void shake(Vector3 target, float _mass, float _stiffness, float _damping)
        {
            force = (target - position) * _stiffness;
            velocity = (velocity + force / _mass) * _damping;
            position += velocity;
            if((velocity+force+force/_mass).magnitude<0.001f)
            {
                position = target; 
            }
        }
    }
}
