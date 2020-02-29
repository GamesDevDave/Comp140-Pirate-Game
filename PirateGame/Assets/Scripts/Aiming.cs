using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    PlayerMovement anchor;

    public Camera mainCamera;
    public Camera cannonCamera;

    Vector3 offset;
    private void CannonCamera()
    {
        if (anchor.anchorDown)
        {
            mainCamera.enabled = false;
            cannonCamera.enabled = true;
        }
    }
}
