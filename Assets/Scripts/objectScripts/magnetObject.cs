using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnetObject : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    private void FixedUpdate()
    {
        transform.localEulerAngles += transform.forward * rotationSpeed * Time.fixedDeltaTime;
    }
}
