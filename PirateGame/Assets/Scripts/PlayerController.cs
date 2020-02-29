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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Anchor();

        if (anchorDown == false)
        {
            rb.velocity = transform.forward * playerSpeed;
        }

        Turning();
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
        }
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
