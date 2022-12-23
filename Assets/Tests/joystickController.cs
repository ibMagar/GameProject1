using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick;
using DG.Tweening;
public class joystickController : MonoBehaviour
{

    [SerializeField] Joystick joystick;
    [SerializeField] float x;
    [SerializeField] float z;
    [SerializeField] float speed;
    private void Start()
    {
        
    }
    private void Update()
    {
        x = joystick.Horizontal();
        z = joystick.Vertical();
        transform.position += new Vector3(x, 0f, z)*speed*Time.deltaTime;
         //transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(x, 0f, z),speed*Time.deltaTime);
        //transform.DOMove(transform.position +new Vector3(x, 0f, z), speed * Time.deltaTime);
    }

}
