using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float playerSpeed = 2f;
    Rigidbody rb;
    bool anchorDown = false;

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
}
