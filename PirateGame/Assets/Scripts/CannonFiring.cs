﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFiring : MonoBehaviour
{
    [SerializeField]
    float rotation;
    [SerializeField]
    float currentMouseRotation;
    [SerializeField]
    float rotationSpeed = 50f;
    [SerializeField]
    float firingSpeed = 30f;

    bool hasFired = false;

    PlayerController pc;

    public GameObject cannon;
    public GameObject cannonball;
    public GameObject firingPoint;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (pc.anchorDown == true)
        {
            DetectInput();
            DetectShot();
        }
    }

    private void DetectInput()
    {

        currentMouseRotation = Input.GetAxis("Mouse X");
        rotation = Mathf.Clamp(rotation, -120, -60);

        if (currentMouseRotation > 0f)
        {
            rotation += 1f * rotationSpeed * Time.deltaTime;

            cannon.transform.localRotation = Quaternion.Euler(0f, rotation, 0f);
        }

        if (currentMouseRotation < 0f)
        {
            rotation -= 1f * rotationSpeed * Time.deltaTime;

            cannon.transform.localRotation = Quaternion.Euler(0f, rotation, 0f);
        }
    }

    private void DetectShot()
    {
        if (Input.GetMouseButtonDown(0) && hasFired == false)
        {
            GameObject cannonballClone;
            cannonballClone = Instantiate(cannonball, firingPoint.transform.position, transform.rotation);
            cannonballClone.GetComponent<Rigidbody>().AddForce(firingPoint.transform.right * firingSpeed);
            hasFired = true;
            StartCoroutine(cannonCooldown());
            Destroy(cannonballClone, 5f);
        }
    }

    IEnumerator cannonCooldown()
    {
        yield return new WaitForSeconds(2);
        hasFired = false;
    }

}
