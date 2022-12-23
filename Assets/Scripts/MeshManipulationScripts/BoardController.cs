using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick;
using DG.Tweening;

public class BoardController : MonoBehaviour
{
    #region variable region
    //[SerializeField] Joystick joystick;
    [Header("Hole manipulation")]
    public float holeScale;                     // manipulating the hole size 
    [Header("hole visuals")]
    [SerializeField] Transform holeBorder;
    private Vector3 CurrentHoleScale;
    [Header("meshes")]
    [SerializeField] MeshFilter Filter;
    [SerializeField] MeshCollider Collider;
    [SerializeField] Mesh mesh;
    [SerializeField] Transform holeCenter;
    [SerializeField] float radius;
    [SerializeField] float moveSpeed;
    

    [Header("datas")]
    [SerializeField] List<int> vertices;
    [SerializeField] List<Vector3> offsets;
    [SerializeField] int verticesCount;

    float x, y;
    Vector3 targetPosition;
    Vector3 currentPos;
    public Vector2 holePositionLimit;
    [Space]
    [SerializeField] MeshFilter poolMesh;
    [Space]                                 //magnet
    [Header("magnet effect")]
    [SerializeField] GameObject magnetObject;
    [SerializeField] ParticleSystem magnetDropEffect;
    [SerializeField] bool isMagnetEnabled;
    [SerializeField] int MagnetFadeRate;
    [SerializeField] float timeToFade;
    [Header("drop effect")]
    [SerializeField] ParticleSystem obstacledropEffect;
    [SerializeField] ParticleSystem objectdropEffect;
    [SerializeField] ParticleSystem chargeObjectDropEffect;
    //Android
    Vector3 initialTouchPosition;
    [SerializeField] LayerMask board;
    Vector3 moveposition;
    Vector3 initialPosition;
    Ray ray;
    RaycastHit hit;
    #endregion
    private void Start()
    {

        magnetObject.SetActive(false);
        Filter = GetComponent<MeshFilter>();
        Collider = GetComponent<MeshCollider>();
        Filter.mesh = poolMesh.mesh;
        mesh = Filter.sharedMesh;
        getVertices();
        CurrentHoleScale = holeBorder.localScale;
    }
    private void Update()
    {
#if UNITY_EDITOR
        //Mouse move
        GameData.isMoving = Input.GetMouseButton(0);
        if (!GameData.isGameOver && GameData.isMoving)
        {
            moveSpeed = 10f;
            MoveHole();
            UpdateMesh();
        }
#else

        // mobile touch
       
        GameData.isMoving = Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved);
        if(!GameData.isGameOver && GameData.isMoving)
        {
                moveSpeed=1.5f;
                MoveHole();
                UpdateMesh();
        }
        
#endif
    }

    void MoveHole()
    {
       
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        moveposition = holeCenter.position + new Vector3(x, 0f, y);
        currentPos = Vector3.Lerp(holeCenter.position, moveposition,moveSpeed*Time.deltaTime);
        
        targetPosition = new Vector3(Mathf.Clamp(currentPos.x, -holePositionLimit.x,holePositionLimit.x), currentPos.y, Mathf.Clamp(currentPos.z, -holePositionLimit.y,holePositionLimit.y));
        holeCenter.position = targetPosition;

    }
    void UpdateMesh()
    {
        Vector3[] currentVertices=mesh.vertices;
        for(int i=0;i<verticesCount;i++)
        {
            currentVertices[vertices[i]] = holeCenter.position + (offsets[i]*holeScale);
        }
        mesh.vertices = currentVertices;
        Filter.mesh = mesh;
        Collider.sharedMesh = mesh;

        holeBorder.localScale = CurrentHoleScale * holeScale;
     }
    void getVertices()
    {
        if (vertices.Count > 0)
        {
            vertices.Clear();
            offsets.Clear();

        }
        for (int i=0;i<mesh.vertices.Length;i++)
        {
            Vector3 vertex = mesh.vertices[i];
            //print(mesh.vertices[i]);
            float distance = Vector3.Distance(holeCenter.position, vertex);
            if(distance<radius)
            {
                vertices.Add(i);
                offsets.Add(vertex - holeCenter.position);
            }
                //print(i+ "  "+mesh.vertices[i]);
             
        }
        verticesCount = vertices.Count;
       // print(vertices.Count);
    }
    public void enableMagnet(float magnetTimer)
    {
            magnetDropEffect.Play();                //magnet drop effect
        if (!isMagnetEnabled)
        {
            SoundManager.instance.PlayMagnetDropSound();
            isMagnetEnabled = true;
            StartCoroutine(handleMagnetObject(magnetTimer));
        }
    }
    IEnumerator handleMagnetObject(float magnetTimer)           // Heavy code.
    {
        {
           /* Color currentColor = magnetObject.GetComponent<SpriteRenderer>().color;
            Color targetColor = currentColor;
            targetColor.a = 0f;
            magnetObject.SetActive(true);
            yield return new WaitForSeconds(magnetTimer - timeToFade > 0 ? magnetTimer : timeToFade);
            float percent = 0f;
            while (percent < timeToFade)
            {
                magnetObject.GetComponent<SpriteRenderer>().color = Color.Lerp(currentColor, targetColor, Mathf.PingPong(percent * MagnetFadeRate, 1));
                percent += Time.deltaTime;
                yield return null;
            }
            magnetObject.SetActive(false);
            isMagnetEnabled = false;*/
        }
        magnetObject.SetActive(true);
        yield return new WaitForSeconds(magnetTimer);
        SoundManager.instance.PlayMagnetoutSound();
        magnetObject.SetActive(false);
        isMagnetEnabled = false;

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(holeCenter.position, radius);
        /*for (int i = 0; i < Filter.mesh.vertices.Length; i++)
        {
            Gizmos.DrawLine(holeCenter.position, new Vector3(Filter.mesh.vertices[i].x, Filter.mesh.vertices[i].y, Filter.mesh.vertices[i].z));
        }*/
        //print(Filter.mesh.vertices.Length);
        
    }
    public void ObstacleDropEffect()
    {
        //if (!dropEffect.isPlaying)
            obstacledropEffect.Play();
    }
    public void ObjectDropEffect()
    {
        objectdropEffect.Play();
        //shake camera
    }
    public void ChargeObjectDropEffect()
    {
        chargeObjectDropEffect.Play();
    }
    
}
