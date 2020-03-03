using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFiring : MonoBehaviour
{
    [SerializeField]
    float maxRotation = -120f;
    [SerializeField]
    float minRotation = -60f;
    [SerializeField]
    float rotationSpeed;

    bool hasFired = true;

    public GameObject cannon;

    private void Update()
    {
        DetectInput();
    }

    private void DetectInput()
    {
        cannon.transform.Rotate(0, Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
