using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float playerSpeed = 2f;
    Rigidbody rb;
    [SerializeField]
    float turnSpeed = 0.5f;
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
        if (Input.GetKeyDown(KeyCode.F) && anchorDown == false)
        {
            mainCamera.SetActive(false);
            sideCamera.SetActive(true);
            anchorDown = true;
        }

        else if (Input.GetKeyDown(KeyCode.F) && anchorDown == true)
        {
            sideCamera.SetActive(false);
            mainCamera.SetActive(true);
            anchorDown = false;
            cannon.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
        }
    }

    private void TurnWithPotentiometer()
    {
        float yPos = arduinoInput.Remap(int.Parse(arduinoInput.value), 0, 1, 0, 10);

        transform.Rotate(0, arduinoInput.steerValue * Time.deltaTime, 0);
    }

    private void Turning()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, turnSpeed, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -turnSpeed, 0);
        }
    }
}
