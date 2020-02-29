using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float playerSpeed = 2f;
    Rigidbody rb;
    public bool anchorDown = false;

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
            anchorDown = true;
        }

        else if (Input.GetKeyDown(KeyCode.F) && anchorDown == true)
        {
            anchorDown = false;
        }
    }

    private void Turning()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -1, 0);
        }
    }
}
