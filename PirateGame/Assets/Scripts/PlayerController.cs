using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float playerSpeed = 2f;
    Rigidbody rb;
    [SerializeField]
    float turnSpeed = 0.1f;
    public bool anchorDown = false;
    public GameObject mainCamera;
    public GameObject sideCamera;
    public GameObject cannon;
    private bool cooldownDone = true;
    ArduinoInput arduinoInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        arduinoInput = GetComponent<ArduinoInput>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (anchorDown == false)
        {
            rb.velocity = transform.forward * playerSpeed;
        }

        Anchor();
        TurnWithPotentiometer();
        //Turning();
    }

    private void Anchor()
    {

        if (arduinoInput.anchorButtonBool && anchorDown == false && cooldownDone)
        {
            mainCamera.SetActive(false);
            sideCamera.SetActive(true);
            anchorDown = true;
            cooldownDone = false;

            StartCoroutine("CooldownCoroutine");

        }

        else if (arduinoInput.anchorButtonBool && anchorDown == true && cooldownDone)
        {
            sideCamera.SetActive(false);
            mainCamera.SetActive(true);
            anchorDown = false;
            cannon.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cooldownDone = false;

            StartCoroutine("CooldownCoroutine");
        }

    }

    private void TurnWithPotentiometer()
    {

        if (arduinoInput.steerValue > 1)
        {
            transform.Rotate(0, -arduinoInput.steerValue, 0);
        }
        else if (arduinoInput.steerValue < -1)
        {
            transform.Rotate(0, -arduinoInput.steerValue, 0);
        }
    }

    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(1);
        cooldownDone = true;
    }

    //private void Turning()
    //{
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.Rotate(0, turnSpeed, 0);
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.Rotate(0, -turnSpeed, 0);
    //    }
    //}
}
