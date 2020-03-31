using UnityEngine;

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
    ArduinoInput arduinoInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        arduinoInput = GetComponent<ArduinoInput>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Anchor();

        if (anchorDown == false)
        {
            rb.velocity = transform.forward * playerSpeed;
        }

        TurnWithPotentiometer();
        //Turning();
    }

    private void Anchor()
    {
        if (arduinoInput.anchorButtonBool && anchorDown == false)
        {
            mainCamera.SetActive(false);
            sideCamera.SetActive(true);
            anchorDown = true;
        }

        else if (!arduinoInput.anchorButtonBool && anchorDown == true)
        {
            sideCamera.SetActive(false);
            mainCamera.SetActive(true);
            anchorDown = false;
            cannon.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
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
