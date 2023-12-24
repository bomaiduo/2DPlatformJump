using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    public Vector3 EulerAngleSpeed;
    public bool running = true;

    void Update()
    {
        if (running && EulerAngleSpeed != Vector3.zero)
        {
            transform.localRotation = Quaternion.Euler(EulerAngleSpeed * Time.deltaTime) * transform.localRotation;
        }
    }
}
